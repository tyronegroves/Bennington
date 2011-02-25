using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bennington.Repository.Helpers
{
	public interface IGetValueOfIdPropertyForInstance
	{
		string GetId(object o);
	}

	public class GetValueOfIdPropertyForInstance : IGetValueOfIdPropertyForInstance
	{
		public string GetId(object o)
		{
			throw new NotImplementedException();
		}
	}
}
