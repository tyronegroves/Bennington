using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paragon.Core.Helpers
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
