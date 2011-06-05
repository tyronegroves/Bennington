using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;

namespace MvcTurbine.EmbeddedResources
{
	public class AssemblyResourceFile : VirtualFile
	{
		private readonly EmbeddedResource embeddedResource;

		public AssemblyResourceFile(EmbeddedResource resource, string virtualPath) :
			base(virtualPath)
		{
			if (resource == null) throw new ArgumentNullException("resource", "EmbeddedResource cannot be null.");

			embeddedResource = resource;
		}

		public override Stream Open()
		{
			var assembly = GetResourceAssembly();
			return assembly == null ? null : assembly.GetManifestResourceStream(embeddedResource.Name);
		}

		protected virtual Assembly GetResourceAssembly()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return assemblies
				.Where(assembly => string.Equals(assembly.FullName, embeddedResource.AssemblyFullName, StringComparison.InvariantCultureIgnoreCase))
				.SingleOrDefault();
		}
	}
}