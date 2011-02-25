using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bennington.Repository.Helpers
{
	public interface IGetRootPathToSerializeObjectTo<T>
	{
		string GetPathFromObjectUsingIdProperty(T o);
	}

	public class GetRootPathToSerializeObjectTo<T> : IGetRootPathToSerializeObjectTo<T>
	{
		public string GetPathFromObjectUsingIdProperty(T o)
		{
			throw new NotImplementedException();
		}
	}
}
