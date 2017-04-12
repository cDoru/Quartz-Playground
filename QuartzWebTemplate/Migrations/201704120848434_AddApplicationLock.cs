namespace QuartzWebTemplate.Quartz
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationLock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationLock",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UtcTimestamp = c.DateTime(nullable: false),
                        LockName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationLock");
        }
    }
}
