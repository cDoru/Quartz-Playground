using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QuartzWebTemplate.Migrations.Embedded
{
    class EmbeddedUtil
    {

        private static EmbeddedUtil _executingResources;

        private readonly Assembly _assembly;

        private readonly string[] _resources;


        public static EmbeddedUtil ThisResources
        {
            get
            {
                return _executingResources ??
                       (_executingResources = new EmbeddedUtil(Assembly.GetAssembly(typeof(ThisAssembly))));
            }
        }

        private EmbeddedUtil(Assembly assembly)
        {
            _assembly = assembly;
            _resources = assembly.GetManifestResourceNames();
        }

        public string GetContent(string filename)
        {
            return new StreamReader(GetStream(filename)).ReadToEnd();
        }

        private Stream GetStream(string resName)
        {
            var possibleCandidates = _resources.Where(s => s.Contains(resName)).ToArray();

            switch (possibleCandidates.Length)
            {
                case 0:
                    return null;
                case 1:
                    return _assembly.GetManifestResourceStream(possibleCandidates[0]);
            }

            throw new ArgumentException(@"Ambiguous name, cannot identify resource", "resName");
        }
    }
}