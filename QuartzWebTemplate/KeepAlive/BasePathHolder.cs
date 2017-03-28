namespace QuartzWebTemplate.KeepAlive
{
    public class BasePathHolder
    {
        private static readonly object Lock = new object();

        private static string _basePath;
        public static string BasePath
        {
            get { return _basePath; }
            set
            {
                using (new Lock(Lock))
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