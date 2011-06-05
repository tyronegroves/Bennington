using System;

namespace MvcTurbine.EmbeddedResources
{
	[Serializable]
	public class EmbeddedResource
	{
		public string Name { get; set; }
		public string AssemblyFullName { get; set; }
	}
}