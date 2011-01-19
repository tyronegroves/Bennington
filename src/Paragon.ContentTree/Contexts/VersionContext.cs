using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paragon.ContentTree.Contexts
{
	public interface IVersionContext
	{
		string GetCurrentVersionId();
	}

	public class VersionContext : IVersionContext
	{
		public string GetCurrentVersionId()
		{
			return "Draft";
		}
	}
}