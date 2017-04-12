using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    public class ApplicationLockConfiguration : EntityTypeConfiguration<ApplicationLock>
    {
        private const string TableName = "ApplicationLock";

        public ApplicationLockConfiguration()
            : this("dbo")
        {

        }

        private ApplicationLockConfiguration(string schema)
        {
            ToTable(TableName, schema);

            HasKey(x => x.Id);

            Property(x => x.UtcTimestamp).HasColumnName("UtcTimestamp").HasColumnType("datetime").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LockName).HasColumnName("LockName").HasColumnType("nvarchar").HasMaxLength(256).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}