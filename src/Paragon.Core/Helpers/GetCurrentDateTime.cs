using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paragon.Core.Helpers
{
	public interface IGetCurrentDateTime
	{
		DateTime Now();
	}

	public class GetCurrentDateTime : IGetCurrentDateTime
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}
