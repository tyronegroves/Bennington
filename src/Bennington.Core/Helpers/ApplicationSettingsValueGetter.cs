using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Paragon.Core.Helpers
{
	public interface IApplicationSettingsValueGetter
	{
		string GetValue(string key);
	}

	public class ApplicationSettingsValueGetter : IApplicationSettingsValueGetter
	{
		public string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}
