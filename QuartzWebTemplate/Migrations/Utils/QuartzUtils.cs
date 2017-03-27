using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuartzWebTemplate.Migrations.Embedded;

namespace QuartzWebTemplate.Migrations.Utils
{
    public class QuartzUtils
    {
        public static void AddQuartzIndexes(Action<string> sql)
        {
            var indexesSql = EmbeddedUtil.ThisResources.GetContent(EmbeddedKeys.CreateQuartzIndexes);
            sql(indexesSql);
        }

        public static void RollbackQuartzIndexes(Action<string> sql)
        {
            var indexesSql = EmbeddedUtil.ThisResources.GetContent(EmbeddedKeys.DropQuartzIndexes);
        }
    }
}