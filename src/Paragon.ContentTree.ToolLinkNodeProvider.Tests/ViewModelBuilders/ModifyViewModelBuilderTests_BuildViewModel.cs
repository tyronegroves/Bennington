﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;
using Paragon.ContentTree.ToolLinkNodeProvider.ViewModelBuilders;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Tests.ViewModelBuilders
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
		public void Returns_view_model_with_input_model_set_to_instance_passed_in()
		{
			var result = mocker.Resolve<ModifyViewModelBuilder>().BuildViewModel(new ToolLinkInputModel()
			                                                                     	{
			                                                                     		Action = "test",
			                                                                     	});

			Assert.AreEqual("test", result.ToolLinkInputModel.Action);
		}
	}
}
