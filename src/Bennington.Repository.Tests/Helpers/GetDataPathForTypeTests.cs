using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Core.Helpers;
using Bennington.Repository.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Repository.Tests.Helpers
{
	[TestClass]
	public class GetDataPathForTypeTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_correct_path()
		{
			mocker.GetMock<IGetPathToDataDirectoryService>()
				.Setup(a => a.GetPathToDirectory())
				.Returns("/path/");

			var result = mocker.Resolve<GetDataPathForType>().GetPathForDataByType(typeof(string));

			Assert.AreEqual("/path/" + typeof(string).FullName + "/", result);
		}
	}
}
