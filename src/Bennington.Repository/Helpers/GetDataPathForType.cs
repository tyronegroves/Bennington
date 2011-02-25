using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bennington.Repository.Helpers
{
	public interface IGetDataPathForType
	{
		string GetPathForDataByType(Type t);
	}

	public class GetDataPathForType : IGetDataPathForType
	{
		public string GetPathForDataByType(Type t)
		{
			throw new NotImplementedException();
		}
	}
}
