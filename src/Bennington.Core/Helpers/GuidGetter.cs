using System;

namespace Bennington.Core.Helpers
{
	public interface IGuidGetter
	{
		Guid GetGuid();
	}

	public class GuidGetter : IGuidGetter
	{
		public Guid GetGuid()
		{
			return Guid.NewGuid();
		}
	}
}
