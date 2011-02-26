using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bennington.Core.Helpers;

namespace Bennington.Repository.Helpers
{
	public interface IGetDataPathForType
	{
		string GetPathForDataByType(Type t);
	}

	public class GetDataPathForType : IGetDataPathForType
	{
		private readonly IGetPathToDataDirectoryService getPathToDataDirectoryService;

		public GetDataPathForType(IGetPathToDataDirectoryService getPathToDataDirectoryService)
		{
			this.getPathToDataDirectoryService = getPathToDataDirectoryService;
		}

		public string GetPathForDataByType(Type t)
		{
			return getPathToDataDirectoryService.GetPathToDirectory() + t + "/";
		}
	}
}
