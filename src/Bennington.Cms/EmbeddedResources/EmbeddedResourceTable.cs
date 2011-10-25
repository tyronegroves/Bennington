using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTurbine.EmbeddedResources
{
    [Serializable]
    public class EmbeddedResourceTable
    {
        private static readonly object Lock = new object();
        private readonly Dictionary<string, EmbeddedResource> cache;

        public EmbeddedResourceTable()
        {
            cache = new Dictionary<string, EmbeddedResource>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void AddResource(string resourceName, string assemblyName)
        {
            lock(Lock)
                cache[resourceName] = new EmbeddedResource {Name = resourceName, AssemblyFullName = assemblyName};
        }

        public IList<EmbeddedResource> Resources
        {
            get { return cache.Values.ToList(); }
        }

        public bool ContainsEmbeddedResource(string virtualPath)
        {
            var foundResource = FindEmbeddedResource(virtualPath);
            return (foundResource != null);
        }

        public EmbeddedResource FindEmbeddedResource(string virtualPath)
        {
            var name = GetNameFromPath(virtualPath);
            if(string.IsNullOrEmpty(name))
                return null;

            var resources = Resources.Where(r => r.Name.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));
            if(resources.Count() > 1)
                throw new Exception(string.Format("Multiple resources found matching \"{0}\". Consider using the project naming convention \"SiteName.ProjectName\".", name));

            return resources.SingleOrDefault();
        }

        protected string GetNameFromPath(string virtualPath)
        {
            if(string.IsNullOrEmpty(virtualPath))
                return null;
            return virtualPath
                .Replace("~", "")
                .Replace("/", ".");
        }
    }
}