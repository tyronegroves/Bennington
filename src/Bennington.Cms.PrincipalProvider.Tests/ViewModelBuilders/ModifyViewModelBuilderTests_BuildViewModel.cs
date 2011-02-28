using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.PrincipalProvider.Tests.ViewModelBuilders
{
	[TestClass]
	public class ModifyViewModelBuilderTests_BuildViewModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_non_null_input_model_when_passed_null()
		{
			var result = mocker.Resolve<ModifyViewModelBuilder>()
											.BuildViewModel(null);

			Assert.IsNotNull(result.UserInputModel);
		}

		[TestMethod]
		public void Returns_input_model_passed_in()
		{
			var result = mocker.Resolve<ModifyViewModelBuilder>()
											.BuildViewModel(new UserInputModel()
											                	{
											                		Id = "id",
											                	});

			Assert.AreEqual("id", result.UserInputModel.Id);
		}
	}
}
