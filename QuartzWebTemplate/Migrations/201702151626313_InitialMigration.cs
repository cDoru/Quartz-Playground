namespace QuartzWebTemplate.Quartz
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QRTZ_BLOB_TRIGGERS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        TRIGGER_NAME = c.String(nullable: false, maxLength: 150),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                        BLOB_DATA = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP });
            
            CreateTable(
                "dbo.QRTZ_CALENDARS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        CALENDAR_NAME = c.String(nullable: false, maxLength: 200),
                        CALENDAR = c.Binary(nullable: false, storeType: "image"),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.CALENDAR_NAME });
            
            CreateTable(
                "dbo.QRTZ_CRON_TRIGGERS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        TRIGGER_NAME = c.String(nullable: false, maxLength: 150),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                        CRON_EXPRESSION = c.String(nullable: false, maxLength: 120),
                        TIME_ZONE_ID = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .ForeignKey("dbo.QRTZ_TRIGGERS", t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .Index(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP });
            
            CreateTable(
                "dbo.QRTZ_TRIGGERS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        TRIGGER_NAME = c.String(nullable: false, maxLength: 150),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                        JOB_NAME = c.String(nullable: false, maxLength: 150),
                        JOB_GROUP = c.String(nullable: false, maxLength: 150),
                        DESCRIPTION = c.String(maxLength: 250),
                        NEXT_FIRE_TIME = c.Long(),
                        PREV_FIRE_TIME = c.Long(),
                        PRIORITY = c.Int(),
                        TRIGGER_STATE = c.String(nullable: false, maxLength: 16),
                        TRIGGER_TYPE = c.String(nullable: false, maxLength: 8),
                        START_TIME = c.Long(nullable: false),
                        END_TIME = c.Long(),
                        CALENDAR_NAME = c.String(maxLength: 200),
                        MISFIRE_INSTR = c.Int(),
                        JOB_DATA = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .ForeignKey("dbo.QRTZ_JOB_DETAILS", t => new { t.SCHED_NAME, t.JOB_NAME, t.JOB_GROUP })
                .Index(t => new { t.SCHED_NAME, t.JOB_NAME, t.JOB_GROUP });
            
            CreateTable(
                "dbo.QRTZ_JOB_DETAILS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        JOB_NAME = c.String(nullable: false, maxLength: 150),
                        JOB_GROUP = c.String(nullable: false, maxLength: 150),
                        DESCRIPTION = c.String(maxLength: 250),
                        JOB_CLASS_NAME = c.String(nullable: false, maxLength: 250),
                        IS_DURABLE = c.Boolean(nullable: false),
                        IS_NONCONCURRENT = c.Boolean(nullable: false),
                        IS_UPDATE_DATA = c.Boolean(nullable: false),
                        REQUESTS_RECOVERY = c.Boolean(nullable: false),
                        JOB_DATA = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.JOB_NAME, t.JOB_GROUP });
            
            CreateTable(
                "dbo.QRTZ_SIMPLE_TRIGGERS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        TRIGGER_NAME = c.String(nullable: false, maxLength: 150),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                        REPEAT_COUNT = c.Int(nullable: false),
                        REPEAT_INTERVAL = c.Long(nullable: false),
                        TIMES_TRIGGERED = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .ForeignKey("dbo.QRTZ_TRIGGERS", t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .Index(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP });
            
            CreateTable(
                "dbo.QRTZ_SIMPROP_TRIGGERS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        TRIGGER_NAME = c.String(nullable: false, maxLength: 150),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                        STR_PROP_1 = c.String(maxLength: 512),
                        STR_PROP_2 = c.String(maxLength: 512),
                        STR_PROP_3 = c.String(maxLength: 512),
                        INT_PROP_1 = c.Int(),
                        INT_PROP_2 = c.Int(),
                        LONG_PROP_1 = c.Long(),
                        LONG_PROP_2 = c.Long(),
                        DEC_PROP_1 = c.Decimal(precision: 13, scale: 4, storeType: "numeric"),
                        DEC_PROP_2 = c.Decimal(precision: 13, scale: 4, storeType: "numeric"),
                        BOOL_PROP_1 = c.Boolean(),
                        BOOL_PROP_2 = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .ForeignKey("dbo.QRTZ_TRIGGERS", t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP })
                .Index(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP });
            
            CreateTable(
                "dbo.QRTZ_FIRED_TRIGGERS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        ENTRY_ID = c.String(nullable: false, maxLength: 95),
                        TRIGGER_NAME = c.String(nullable: false, maxLength: 150),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                        INSTANCE_NAME = c.String(nullable: false, maxLength: 200),
                        FIRED_TIME = c.Long(nullable: false),
                        SCHED_TIME = c.Long(nullable: false),
                        PRIORITY = c.Int(nullable: false),
                        STATE = c.String(nullable: false, maxLength: 16),
                        JOB_NAME = c.String(maxLength: 150),
                        JOB_GROUP = c.String(maxLength: 150),
                        IS_NONCONCURRENT = c.Boolean(),
                        REQUESTS_RECOVERY = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.ENTRY_ID });
            
            CreateTable(
                "dbo.QRTZ_LOCKS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        LOCK_NAME = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.LOCK_NAME });
            
            CreateTable(
                "dbo.QRTZ_PAUSED_TRIGGER_GRPS",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        TRIGGER_GROUP = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.TRIGGER_GROUP });
            
            CreateTable(
                "dbo.QRTZ_SCHEDULER_STATE",
                c => new
                    {
                        SCHED_NAME = c.String(nullable: false, maxLength: 100),
                        INSTANCE_NAME = c.String(nullable: false, maxLength: 200),
                        LAST_CHECKIN_TIME = c.Long(nullable: false),
                        CHECKIN_INTERVAL = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.SCHED_NAME, t.INSTANCE_NAME });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QRTZ_CRON_TRIGGERS", new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" }, "dbo.QRTZ_TRIGGERS");
            DropForeignKey("dbo.QRTZ_SIMPROP_TRIGGERS", new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" }, "dbo.QRTZ_TRIGGERS");
            DropForeignKey("dbo.QRTZ_SIMPLE_TRIGGERS", new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" }, "dbo.QRTZ_TRIGGERS");
            DropForeignKey("dbo.QRTZ_TRIGGERS", new[] { "SCHED_NAME", "JOB_NAME", "JOB_GROUP" }, "dbo.QRTZ_JOB_DETAILS");
            DropIndex("dbo.QRTZ_SIMPROP_TRIGGERS", new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" });
            DropIndex("dbo.QRTZ_SIMPLE_TRIGGERS", new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" });
            DropIndex("dbo.QRTZ_TRIGGERS", new[] { "SCHED_NAME", "JOB_NAME", "JOB_GROUP" });
            DropIndex("dbo.QRTZ_CRON_TRIGGERS", new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" });
            DropTable("dbo.QRTZ_SCHEDULER_STATE");
            DropTable("dbo.QRTZ_PAUSED_TRIGGER_GRPS");
            DropTable("dbo.QRTZ_LOCKS");
            DropTable("dbo.QRTZ_FIRED_TRIGGERS");
            DropTable("dbo.QRTZ_SIMPROP_TRIGGERS");
            DropTable("dbo.QRTZ_SIMPLE_TRIGGERS");
            DropTable("dbo.QRTZ_JOB_DETAILS");
            DropTable("dbo.QRTZ_TRIGGERS");
            DropTable("dbo.QRTZ_CRON_TRIGGERS");
            DropTable("dbo.QRTZ_CALENDARS");
            DropTable("dbo.QRTZ_BLOB_TRIGGERS");
        }
    }
}
