using QuartzWebTemplate.Migrations.Utils;

namespace QuartzWebTemplate.Quartz
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuartzIndexes : DbMigration
    {
        public override void Up()
        {
            QuartzUtils.AddQuartzIndexes(x => Sql(x));
        }
        
        public override void Down()
        {
            QuartzUtils.RollbackQuartzIndexes(x => Sql(x));
        }
    }
}
