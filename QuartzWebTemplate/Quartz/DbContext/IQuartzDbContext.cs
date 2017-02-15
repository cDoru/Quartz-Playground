using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.DbContext
{
    public interface IQuartzDbContext : IDisposable
    {
        DbSet<QrtzBlobTrigger> QrtzBlobTriggers { get; set; } // QRTZ_BLOB_TRIGGERS
        DbSet<QrtzCalendar> QrtzCalendars { get; set; } // QRTZ_CALENDARS
        DbSet<QrtzCronTrigger> QrtzCronTriggers { get; set; } // QRTZ_CRON_TRIGGERS
        DbSet<QrtzFiredTrigger> QrtzFiredTriggers { get; set; } // QRTZ_FIRED_TRIGGERS
        DbSet<QrtzJobDetail> QrtzJobDetails { get; set; } // QRTZ_JOB_DETAILS
        DbSet<QrtzLock> QrtzLocks { get; set; } // QRTZ_LOCKS
        DbSet<QrtzPausedTriggerGrp> QrtzPausedTriggerGrps { get; set; } // QRTZ_PAUSED_TRIGGER_GRPS
        DbSet<QrtzSchedulerState> QrtzSchedulerStates { get; set; } // QRTZ_SCHEDULER_STATE
        DbSet<QrtzSimpleTrigger> QrtzSimpleTriggers { get; set; } // QRTZ_SIMPLE_TRIGGERS
        DbSet<QrtzSimpropTrigger> QrtzSimpropTriggers { get; set; } // QRTZ_SIMPROP_TRIGGERS
        DbSet<QrtzTrigger> QrtzTriggers { get; set; } // QRTZ_TRIGGERS

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbEntityEntry Entry(object entity);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }
}