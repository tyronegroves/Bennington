using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace MvcTurbine.EmbeddedResources
{
	public class EmbeddedResourceVirtualPathProvider : VirtualPathProvider
	{
		private readonly EmbeddedResourceTable embeddedResources;

		public EmbeddedResourceVirtualPathProvider(EmbeddedResourceTable table)
		{
			if (table == null)
				throw new ArgumentNullException("table", "EmbeddedResourceTable cannot be null.");

			embeddedResources = table;
		}

		public override bool FileExists(string virtualPath)
		{
			return base.FileExists(virtualPath) || IsEmbeddedResource(virtualPath);
		}

		private bool IsEmbeddedResource(string virtualPath)
		{
			virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);

			var isContent = virtualPath.Contains("/Content/");
			var isScript = virtualPath.Contains("/Scripts/");
			if (!isContent && !isScript)
				return false;
			return embeddedResources.ContainsEmbeddedResource(virtualPath);
		}

		public override VirtualFile GetFile(string virtualPath)
		{
		    virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);

			if (base.FileExists(virtualPath))
				return base.GetFile(virtualPath);

			if (IsEmbeddedResource(virtualPath))
			{
				var embeddedResource = embeddedResources.FindEmbeddedResource(virtualPath);
				return new AssemblyResourceFile(embeddedResource, virtualPath);
			}

			return null;
		}

		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return IsEmbeddedResource(virtualPath) ? null : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
		}
	}
}