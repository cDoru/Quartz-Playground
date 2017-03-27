using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzWebTemplate.KeepAlive
{
    public class BasePathHolder
    {
        private static readonly object _lock = new object();

        private static string _basePath;
        public static string BasePath
        {
            get { return _basePath; }
            set
            {
                lock (_lock)
                {
                    _basePath = value;
                }
            }
        }

        public static bool NeedsBasePath
        {
            get { return _basePath == null; }
        }
    }
}