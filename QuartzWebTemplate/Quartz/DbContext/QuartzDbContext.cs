using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using QuartzWebTemplate.Quartz.Configurations;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.DbContext
{
    public class QuartzDbContext : System.Data.Entity.DbContext, IQuartzDbContext
    {
        public DbSet<QrtzBlobTrigger> QrtzBlobTriggers { get; set; } // QRTZ_BLOB_TRIGGERS
        public DbSet<QrtzCalendar> QrtzCalendars { get; set; } // QRTZ_CALENDARS
        public DbSet<QrtzCronTrigger> QrtzCronTriggers { get; set; } // QRTZ_CRON_TRIGGERS
        public DbSet<QrtzFiredTrigger> QrtzFiredTriggers { get; set; } // QRTZ_FIRED_TRIGGERS
        public DbSet<QrtzJobDetail> QrtzJobDetails { get; set; } // QRTZ_JOB_DETAILS
        public DbSet<QrtzLock> QrtzLocks { get; set; } // QRTZ_LOCKS
        public DbSet<QrtzPausedTriggerGrp> QrtzPausedTriggerGrps { get; set; } // QRTZ_PAUSED_TRIGGER_GRPS
        public DbSet<QrtzSchedulerState> QrtzSchedulerStates { get; set; } // QRTZ_SCHEDULER_STATE
        public DbSet<QrtzSimpleTrigger> QrtzSimpleTriggers { get; set; } // QRTZ_SIMPLE_TRIGGERS
        public DbSet<QrtzSimpropTrigger> QrtzSimpropTriggers { get; set; } // QRTZ_SIMPROP_TRIGGERS
        public DbSet<QrtzTrigger> QrtzTriggers { get; set; } // QRTZ_TRIGGERS

        //static QuartzDbContext()
        //{
        //    Database.SetInitializer<QuartzDbContext>(null);
        //}

        //public QuartzDbContext()
        //    : base("Name=MyDbContext")
        //{
        //}

        public QuartzDbContext()
        {
            
        }

        public QuartzDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public QuartzDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        public QuartzDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public QuartzDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new QrtzBlobTriggerConfiguration());
            modelBuilder.Configurations.Add(new QrtzCalendarConfiguration());
            modelBuilder.Configurations.Add(new QrtzCronTriggerConfiguration());
            modelBuilder.Configurations.Add(new QrtzFiredTriggerConfiguration());
            modelBuilder.Configurations.Add(new QrtzJobDetailConfiguration());
            modelBuilder.Configurations.Add(new QrtzLockConfiguration());
            modelBuilder.Configurations.Add(new QrtzPausedTriggerGrpConfiguration());
            modelBuilder.Configurations.Add(new QrtzSchedulerStateConfiguration());
            modelBuilder.Configurations.Add(new QrtzSimpleTriggerConfiguration());
            modelBuilder.Configurations.Add(new QrtzSimpropTriggerConfiguration());
            modelBuilder.Configurations.Add(new QrtzTriggerConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new QrtzBlobTriggerConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzCalendarConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzCronTriggerConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzFiredTriggerConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzJobDetailConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzLockConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzPausedTriggerGrpConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzSchedulerStateConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzSimpleTriggerConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzSimpropTriggerConfiguration(schema));
            modelBuilder.Configurations.Add(new QrtzTriggerConfiguration(schema));
            return modelBuilder;
        }
    }

}