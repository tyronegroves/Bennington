using System;
using System.Linq;
using System.Reflection;

namespace MvcTurbine.EmbeddedResources
{
	public interface IEmbeddedResourceResolver
	{
		EmbeddedResourceTable GetEmbeddedResources();
	}

	public class EmbeddedResourceResolver : IEmbeddedResourceResolver
	{
		public EmbeddedResourceTable GetEmbeddedResources()
		{
			var assemblies = GetAssemblies();
			if (assemblies == null || assemblies.Length == 0) return null;

			var table = new EmbeddedResourceTable();

			foreach (var assembly in assemblies)
			{
				var names = GetNamesOfAssemblyResources(assembly);
				if (names == null || names.Length == 0) continue;

				var validNames = from name in names
								 let key = name.ToLowerInvariant()
								 where key.Contains(".content.") || key.Contains(".scripts.")
								 select name;

				foreach (var name in validNames)
					table.AddResource(name, assembly.FullName);
			}

			return table;
		}

		protected virtual Assembly[] GetAssemblies()
		{
			try
			{
				return AppDomain.CurrentDomain.GetAssemblies();
			}
			catch
			{
				return null;
			}
		}

		private static string[] GetNamesOfAssemblyResources(Assembly assembly)
		{
			// GetManifestResourceNames will throw a NotSupportedException when run on a dynamic assembly
			try
			{
				return assembly.GetManifestResourceNames();
			}
			catch
			{
				return new string[] { };
			}
		}
	}
}