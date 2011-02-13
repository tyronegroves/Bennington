using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Paragon.Core.EntityFramework
{
	public interface IEntityConnectionInformation
    {
        string EntityConnectionString { get; }
    	string GetEntityConnectionString(string name);
    }

	public class EntityConnectionInformation : IEntityConnectionInformation
	{
		public string EntityConnectionString
		{
			get
			{
				return ConfigurationManager
					.ConnectionStrings["Entities"].ConnectionString;
			}
		}

		public string GetEntityConnectionString(string name)
		{
			var s = EntityConnectionString.Replace("Models.ObjectModel", name);
			return s;
		}
	}
}
