using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Core.Helpers;
using Bennington.Repository.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bennington.Repository.Tests
{
	[TestClass]
	public class ObjectStoreTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Serializes_object_to_path_provided_by_IGetPathToSerializeObjectTo()
		{
			mocker.GetMock<IGetRootPathToSerializeObjectTo<DataClass>>()
				.Setup(a => a.GetPathFromObjectUsingIdProperty(It.Is<DataClass>(b => b.Id == "id")))
				.Returns("/path/");
			mocker.GetMock<IGetValueOfIdPropertyForInstance>()
				.Setup(a => a.GetId(It.Is<DataClass>(b => b.Id == "id")))
				.Returns("id");

			mocker.Resolve<ObjectStore<DataClass>>().SaveAndReturnId(new DataClass()
																		{
																			Id = "id",
																		});

			mocker.GetMock<IXmlFileSerializationHelper>()
				.Verify(a => a.SerializeToPath<DataClass>(It.Is<DataClass>(b => b.Id == "id"),  "/path/id.xml"));
		}

		[TestMethod]
		public void SaveAndReturnId_method_returns_value_returned_by_IGetValueOfIdPropertyForInstance()
		{
			mocker.GetMock<IGetValueOfIdPropertyForInstance>()
				.Setup(a => a.GetId(It.Is<DataClass>(b => b.Id == "id")))
				.Returns("id");

			var result = mocker.Resolve<ObjectStore<DataClass>>().SaveAndReturnId(new DataClass()
			                                                                      	{
			                                                                      		Id = "id"
			                                                                      	});

			Assert.AreEqual("id", result);
		}

		[TestMethod]
		public void Uses_a_new_guid_if_IGetValueOfIdPropertyForInstance_returns_null()
		{
			var id = Guid.NewGuid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid())
				.Returns(id);
			mocker.GetMock<IGetValueOfIdPropertyForInstance>()
				.Setup(a => a.GetId(It.IsAny<DataClass>()))
				.Returns((string)null);

			var result = mocker.Resolve<ObjectStore<DataClass>>().SaveAndReturnId(new DataClass());

			Assert.AreEqual(id.ToString(), result);
		}

		[TestMethod]
		public void Returns_deserialized_object_from_path_derived_from_Id_property()
		{
			mocker.GetMock<IGetDataPathForType>().Setup(a => a.GetPathForDataByType(typeof(DataClass)))
				.Returns("/path/");
			mocker.GetMock<IXmlFileSerializationHelper>()
				.Setup(a => a.DeserializeFromPath<DataClass>("/path/1.xml"))
				.Returns(new DataClass()
				         	{
				         		Id = "1",
								Name = "test"
				         	});

			var result = mocker.Resolve<ObjectStore<DataClass>>().GetById("1");

			Assert.AreEqual("test", result.Name);
		}
	}

	public class DataClass
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}
