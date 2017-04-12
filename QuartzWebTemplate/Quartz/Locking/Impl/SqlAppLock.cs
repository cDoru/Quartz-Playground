using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using QuartzWebTemplate.Configuration;
using QuartzWebTemplate.Quartz.Locking.Contracts;
using QuartzWebTemplate.Quartz.Locking.Helpers;

namespace QuartzWebTemplate.Quartz.Locking.Impl
{
    public sealed class SqlAppLock : IAppLock
    {
        private readonly IEncryptor _encryptor;
        private readonly string _connectionString;

        public SqlAppLock(IConfigurationProvider configurationProvider, IEncryptor encryptor)
        {
            _connectionString = configurationProvider.Get(ConfigurationKeys.QuartzSqlConnection);
            _encryptor = encryptor;
        }

        /// <summary>
        /// The maximum allowed length for lock names. See https://msdn.microsoft.com/en-us/library/ms189823.aspx
        /// </summary>
        private static int MaxLockNameLength
        {
            get { return 255; }
        }

        private static DateTime Utc
        {
            get { return ConvertToUtc(DateTime.Now); }
        }

        private static DateTime ConvertToUtc(DateTime dateTime)
        {
            switch (dateTime.Kind)
            {
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                case DateTimeKind.Local:
                    return dateTime.ToUniversalTime();
                default:
                    return dateTime;
            }
        }

        /// <summary>
        /// Attempts to acquire the lock synchronously. Usage:
        /// <code>
        ///     using (var handle = myLock.TryAcquire(...))
        ///     {
        ///         if (handle != null) { /* we have the lock! */ }
        ///     }
        ///     // dispose releases the lock if we took it
        /// </code>
        /// </summary>
        /// <param name="lock"></param>
        /// <param name="timeout">How long to wait before giving up on acquiring the lock. Defaults to 0</param>
        /// <returns>An <see cref="IDisposable"/> "handle" which can be used to release the lock, or null if the lock was not taken</returns>
        public LockAcquisitionResult TryAcquire(string @lock, TimeSpan timeout = default(TimeSpan))
        {
            // synchronous mode
            var timeoutMillis = timeout.ToInt32Timeout();

            // calculate safe lock name
            var lockName = ToSafeLockName(@lock, MaxLockNameLength, s => s);

            DbConnection acquireConnection = null;
            var cleanup = true;
            try
            {
                acquireConnection = GetConnection();

                if (_connectionString != null)
                {
                    acquireConnection.Open();
                }
                else if (acquireConnection == null)
                {
                    throw new InvalidOperationException("The transaction had been disposed");
                }
                else if (acquireConnection.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("The connection is not open");
                }

                var checkCommand = SqlHelpers.CreateCheckLockAvailabilityCommand(acquireConnection, timeoutMillis, lockName);
                var exists = (int)checkCommand.ExecuteScalar() > 0;
                if (exists) return LockAcquisitionResult.Fail;

                var id = Guid.NewGuid();

                SqlParameter insertReturnValue;
                using (var insertCommand = SqlHelpers.CreateInsertApplicationLockCommand(acquireConnection, id,
                    timeoutMillis, lockName, Utc, out insertReturnValue))
                {
                    insertCommand.ExecuteNonQuery();
                }

                var ret = (int)insertReturnValue.Value;
                cleanup = ret == 0;
                var success = ret == 0;
                var owner = string.Empty;

                if (success)
                {
                    // hash owner
                    owner = _encryptor.Encrypt(id.ToString());

                    // check no duplicates.
                    var checkDuplicateCommand = SqlHelpers.CreateCheckLockAvailabilityCommand(acquireConnection, timeoutMillis, lockName);
                    var duplicatesExist = (int)checkDuplicateCommand.ExecuteScalar() > 1;

                    if (duplicatesExist)
                    {
                        // delete current lock
                        ReleaseLock(lockName, owner);
                        return LockAcquisitionResult.Fail;
                    }
                }

                return new LockAcquisitionResult
                {
                    Success = success,
                    LockOwner = owner
                };

            }
            catch
            {
                // in case we fail to create lock scope or something
                cleanup = true;
                throw;
            }
            finally
            {
                if (cleanup)
                {
                    Cleanup(acquireConnection);
                }
            }
        }

        private void Cleanup(IDisposable connection)
        {
            // dispose connection and transaction unless they are externally owned
            if (_connectionString == null) return;

            if (connection != null)
            {
                connection.Dispose();
            }
        }

        public LockReleaseResult ReleaseLock(string @lock, string lockOwner)
        {
            var lockName = ToSafeLockName(@lock, MaxLockNameLength, s => s);

            // otherwise issue the release command
            var connection = GetConnection();
            if (_connectionString != null)
            {
                connection.Open();
            }
            else if (connection == null)
            {
                throw new InvalidOperationException("The transaction had been disposed");
            }
            else if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("The connection is not open");
            }

            var id = Guid.Parse(_encryptor.Decrypt(lockOwner));
            using (var checkCommand = SqlHelpers.CreateCheckApplicationLockCommand(connection, 1000, lockName, id))
            {
                var exists = (int)checkCommand.ExecuteScalar() > 0;

                if (!exists)
                {
                    return new LockReleaseResult
                    {
                        Success = false,
                        Reason = ReleaseLockFailure.OwnerNotMatching
                    };
                }

                SqlParameter deleteReturnValue;

                using (
                    var releaseCommand = SqlHelpers.CreateDeleteApplicationLockCommand(connection,
                        1000, lockName, id, out deleteReturnValue))
                {
                    releaseCommand.ExecuteNonQuery();
                }

                var success = (int)deleteReturnValue.Value == 0;

                return success
                    ? new LockReleaseResult
                    {
                        Success = true,
                        Reason = ReleaseLockFailure.Undefined
                    }
                    : new LockReleaseResult
                    {
                        Success = false,
                        Reason = ReleaseLockFailure.ReleaseError
                    };
            }
        }

        public bool VerifyLockOwnership(string lockName, string lockOwner)
        {
            // otherwise issue the release command
            var connection = GetConnection();
            if (_connectionString != null)
            {
                connection.Open();
            }
            else if (connection == null)
            {
                throw new InvalidOperationException("The transaction had been disposed");
            }
            else if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("The connection is not open");
            }

            var id = Guid.Parse(_encryptor.Decrypt(lockOwner));
            using (var checkCommand = SqlHelpers.CreateCheckApplicationLockCommand(connection, 1000, lockName, id))
            {
                var exists = (int)checkCommand.ExecuteScalar() > 0;
                return exists;
            }
        }

        private DbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }



        /// <summary>
        /// Creates a safe lock name
        /// </summary>
        /// <param name="baseLockName"></param>
        /// <param name="maxNameLength"></param>
        /// <param name="convertToValidName"></param>
        /// <returns></returns>
        private static string ToSafeLockName(string baseLockName, int maxNameLength, Func<string, string> convertToValidName)
        {
            if (baseLockName == null)
                throw new ArgumentNullException("baseLockName");

            var validBaseLockName = convertToValidName(baseLockName);
            if (validBaseLockName == baseLockName && validBaseLockName.Length <= maxNameLength)
            {
                return baseLockName;
            }

            using (var sha = new SHA512Managed())
            {
                var hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(baseLockName)));

                if (hash.Length >= maxNameLength)
                {
                    return hash.Substring(0, maxNameLength);
                }

                var prefix = validBaseLockName.Substring(0, Math.Min(validBaseLockName.Length, maxNameLength - hash.Length));
                return prefix + hash;
            }
        }

    }
}