using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

namespace QuartzWebTemplate.Quartz.Locking.Helpers
{
    public static class SqlHelpers
    {
        /// <summary>
        /// Create check application lock command
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="timeoutMillis"></param>
        /// <param name="lockName"></param>
        /// <param name="lockOwner"></param>
        /// <returns></returns>
        public static DbCommand CreateCheckApplicationLockCommand(DbConnection connection, int timeoutMillis, string lockName,
            Guid lockOwner)
        {
            var command = connection.CreateCommand();
            //command.Transaction = transaction;
            command.CommandText = "select COUNT(*) from [dbo].[ApplicationLock] where [LockName] = @lockName and [Id] = @owner";

            command.CommandType = CommandType.Text;
            command.CommandTimeout = timeoutMillis >= 0
                // command timeout is in seconds. We always wait at least the lock timeout plus a buffer 
                ? (timeoutMillis / 1000) + 30
                // otherwise timeout is infinite so we use the infinite timeout of 0
                // (see https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.commandtimeout%28v=vs.110%29.aspx)
                : 0;

            command.Parameters.Add(CreateStringParameter(command, "lockName", lockName));
            command.Parameters.Add(CreateUniqueidentityParameter(command, "owner", lockOwner));
            return command;
        }

        /// <summary>
        /// Create delete application lock command
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="timeoutMillis"></param>
        /// <param name="lockName"></param>
        /// <param name="lockOwner"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static DbCommand CreateDeleteApplicationLockCommand(DbConnection connection,
            int timeoutMillis, string lockName, Guid lockOwner, out SqlParameter returnValue)
        {

            var command = connection.CreateCommand();
            //command.Transaction = transaction;
            command.CommandText = "delete from [dbo].[ApplicationLock] where [LockName] = @lockName and [Id] = @owner";

            command.CommandType = CommandType.Text;
            command.CommandTimeout = timeoutMillis >= 0
                // command timeout is in seconds. We always wait at least the lock timeout plus a buffer 
                ? (timeoutMillis / 1000) + 30
                // otherwise timeout is infinite so we use the infinite timeout of 0
                // (see https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.commandtimeout%28v=vs.110%29.aspx)
                : 0;

            command.Parameters.Add(CreateStringParameter(command, "lockName", lockName));
            command.Parameters.Add(CreateUniqueidentityParameter(command, "owner", lockOwner));
            command.Parameters.Add(returnValue = new SqlParameter { Direction = ParameterDirection.ReturnValue });
            return command;
        }

        /// <summary>
        /// Create check lock availability command
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="timeoutMillis"></param>
        /// <param name="lockName"></param>
        /// <returns></returns>
        public static DbCommand CreateCheckLockAvailabilityCommand(DbConnection connection, int timeoutMillis,
            string lockName)
        {
            var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = "select count(*) from [dbo].[ApplicationLock] where [LockName] = @lockName";
            checkCommand.CommandType = CommandType.Text;
            checkCommand.CommandTimeout = timeoutMillis >= 0
                // command timeout is in seconds. We always wait at least the lock timeout plus a buffer 
                ? (timeoutMillis / 1000) + 30
                // otherwise timeout is infinite so we use the infinite timeout of 0
                // (see https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.commandtimeout%28v=vs.110%29.aspx)
                : 0;
            checkCommand.Parameters.Add(CreateStringParameter(checkCommand, "@lockName", lockName));
            return checkCommand;
        }

        /// <summary>
        /// Creates insert application lock command
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="timeoutMillis"></param>
        /// <param name="lockName"></param>
        /// <param name="utcTimestamp"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static DbCommand CreateInsertApplicationLockCommand(DbConnection connection, Guid id, int timeoutMillis,
            string lockName, DateTime utcTimestamp, out SqlParameter returnValue)
        {

            var insertCommand = connection.CreateCommand();
            //command.Transaction = transaction;
            insertCommand.CommandText = @"INSERT INTO [dbo].[ApplicationLock]
                                       ([Id]
                                       ,[UtcTimestamp]
                                       ,[LockName])
                                        VALUES
                                       (@Id
                                       ,@UtcTimestamp
                                       ,@LockName)";

            insertCommand.CommandType = CommandType.Text;
            insertCommand.CommandTimeout = timeoutMillis >= 0
                // command timeout is in seconds. We always wait at least the lock timeout plus a buffer 
                ? (timeoutMillis / 1000) + 30
                // otherwise timeout is infinite so we use the infinite timeout of 0
                // (see https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.commandtimeout%28v=vs.110%29.aspx)
                : 0;

            insertCommand.Parameters.Add(CreateUniqueidentityParameter(insertCommand, "Id", id));
            insertCommand.Parameters.Add(CreateDateParameter(insertCommand, "UtcTimestamp", utcTimestamp));
            insertCommand.Parameters.Add(CreateStringParameter(insertCommand, "LockName", lockName));

            insertCommand.Parameters.Add(returnValue = new SqlParameter { Direction = ParameterDirection.ReturnValue });
            return insertCommand;
        }

        /// <summary>
        /// Creates simple param
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter CreateParameter(DbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        /// <summary>
        /// Creates string param
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter CreateStringParameter(DbCommand cmd, string name, string value)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.String;
            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Creates datetime param
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DbParameter CreateDateParameter(DbCommand cmd, string name, DateTime date)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.DbType = DbType.DateTime;
            param.Direction = ParameterDirection.Input;
            param.Value = date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            return param;
        }

        /// <summary>
        /// Creates GUID param
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        /// <param name="uniqueidentifier"></param>
        /// <returns></returns>
        public static DbParameter CreateUniqueidentityParameter(DbCommand cmd, string name, Guid uniqueidentifier)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = DbType.Guid;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = uniqueidentifier;
            return parameter;
        }
    }
}