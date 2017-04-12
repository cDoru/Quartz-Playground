using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using QuartzWebTemplate.Quartz.Entities;
using QuartzWebTemplate.Quartz.Fakes;

namespace QuartzWebTemplate.Quartz.DbContext
{
    public class FakeQuartzDbContext : IQuartzDbContext
    {
        public DbSet<QrtzBlobTrigger> QrtzBlobTriggers { get; set; }
        public DbSet<QrtzCalendar> QrtzCalendars { get; set; }
        public DbSet<QrtzCronTrigger> QrtzCronTriggers { get; set; }
        public DbSet<QrtzFiredTrigger> QrtzFiredTriggers { get; set; }
        public DbSet<QrtzJobDetail> QrtzJobDetails { get; set; }
        public DbSet<QrtzLock> QrtzLocks { get; set; }
        public DbSet<QrtzPausedTriggerGrp> QrtzPausedTriggerGrps { get; set; }
        public DbSet<QrtzSchedulerState> QrtzSchedulerStates { get; set; }
        public DbSet<QrtzSimpleTrigger> QrtzSimpleTriggers { get; set; }
        public DbSet<QrtzSimpropTrigger> QrtzSimpropTriggers { get; set; }
        public DbSet<QrtzTrigger> QrtzTriggers { get; set; }
        public DbSet<ApplicationLock> Locks { get; set; }

        public FakeQuartzDbContext()
        {
            QrtzBlobTriggers = new FakeDbSet<QrtzBlobTrigger>("SchedName", "TriggerName", "TriggerGroup");
            QrtzCalendars = new FakeDbSet<QrtzCalendar>("SchedName", "CalendarName");
            QrtzCronTriggers = new FakeDbSet<QrtzCronTrigger>("SchedName", "TriggerName", "TriggerGroup");
            QrtzFiredTriggers = new FakeDbSet<QrtzFiredTrigger>("SchedName", "EntryId");
            QrtzJobDetails = new FakeDbSet<QrtzJobDetail>("SchedName", "JobName", "JobGroup");
            QrtzLocks = new FakeDbSet<QrtzLock>("SchedName", "LockName");
            QrtzPausedTriggerGrps = new FakeDbSet<QrtzPausedTriggerGrp>("SchedName", "TriggerGroup");
            QrtzSchedulerStates = new FakeDbSet<QrtzSchedulerState>("SchedName", "InstanceName");
            QrtzSimpleTriggers = new FakeDbSet<QrtzSimpleTrigger>("SchedName", "TriggerName", "TriggerGroup");
            QrtzSimpropTriggers = new FakeDbSet<QrtzSimpropTrigger>("SchedName", "TriggerName", "TriggerGroup");
            QrtzTriggers = new FakeDbSet<QrtzTrigger>("SchedName", "TriggerName", "TriggerGroup");
            Locks = new FakeDbSet<ApplicationLock>("Id");
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public Task<int> SaveChangesAsync()
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public DbChangeTracker _changeTracker;
        public DbChangeTracker ChangeTracker { get { return _changeTracker; } }
        public DbContextConfiguration _configuration;
        public DbContextConfiguration Configuration { get { return _configuration; } }
        public Database _database;
        public Database Database { get { return _database; } }
        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }
        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            throw new NotImplementedException();
        }
        public DbSet Set(Type entityType)
        {
            throw new NotImplementedException();
        }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}