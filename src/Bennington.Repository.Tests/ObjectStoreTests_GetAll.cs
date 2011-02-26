using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Core.Helpers;
using Bennington.Repository.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Repository.Tests
{
	[TestClass]
	public class ObjectStoreTests_GetAll
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_deserialized_instances_from_file_system()
		{
			mocker.GetMock<IGetDataPathForType>()
				.Setup(a => a.GetPathForDataByType(typeof(DataClass)))
				.Returns("/path/");
			mocker.GetMock<IFileSystem>()
				.Setup(a => a.GetFiles("/path/", "*.xml"))
				.Returns(new string[]
				         	{
				         		"/path/1.xml",
								"/path/2.xml",
							});
			mocker.GetMock<IXmlFileSerializationHelper>()
				.Setup(a => a.DeserializeFromPath<DataClass>("/path/1.xml"))
				.Returns(new DataClass()
				         	{
				         		Id = "1",
				         	});
			mocker.GetMock<IXmlFileSerializationHelper>()
				.Setup(a => a.DeserializeFromPath<DataClass>("/path/2.xml"))
				.Returns(new DataClass()
				{
					Id = "2",
				});

			var results = mocker.Resolve<ObjectStore<DataClass>>().GetAll();

			Assert.AreEqual(2, results.Count());
			Assert.AreEqual(1, results.Where(a => a.Id == "1").Count());
			Assert.AreEqual(1, results.Where(a => a.Id == "2").Count());
		}
	}
}
