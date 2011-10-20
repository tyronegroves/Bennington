using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMoq;
using Bennington.ContentTree.TreeManager.Controllers;
using Bennington.ContentTree.TreeManager.Models;
using Bennington.ContentTree.TreeManager.ViewModelBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.TreeManager.Tests.Controllers
{
    [TestClass]
    public class TreeManagerControllerTests_Index
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Init()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Returns_correct_view_name()
        {
            var result = mocker.Resolve<TreeManagerController>().Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Returns_view_model_from_view_model_builder()
        {
            mocker.GetMock<ITreeManagerIndexViewModelBuilder>().Setup(a => a.BuildViewModel())
                .Returns(new TreeManagerIndexViewModel()
                             {
                                 ContentTreeHasNodes = true
                             });


            var result = mocker.Resolve<TreeManagerController>().Index() as ViewResult;

            Assert.IsTrue((result.ViewData.Model as TreeManagerIndexViewModel).ContentTreeHasNodes);
        }
    }
}
