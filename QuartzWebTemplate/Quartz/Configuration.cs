using System.Data.Entity.Migrations;
using QuartzWebTemplate.Quartz.DbContext;

namespace QuartzWebTemplate.Quartz
{
    internal sealed class Configuration : DbMigrationsConfiguration<QuartzDbContext>
    {
        public Configuration()
        {
            // so that entityframework doesn't cry
            AutomaticMigrationsEnabled = false;
        }
    }
}