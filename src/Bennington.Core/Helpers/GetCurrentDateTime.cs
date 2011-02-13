using System;

namespace Bennington.Core.Helpers
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
