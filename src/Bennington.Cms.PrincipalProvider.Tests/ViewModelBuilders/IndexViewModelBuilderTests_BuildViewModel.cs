using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Bennington.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.Cms.PrincipalProvider.Tests.ViewModelBuilders
{
	[TestClass]
	public class IndexViewModelBuilderTests_BuildViewModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_users_from_repository()
		{
			mocker.GetMock<IUserRepository>()
				.Setup(a => a.GetAll())
				.Returns(new User[]
				         	{
				         		new User(), 
							});

			var result = mocker.Resolve<IndexViewModelBuilder>().BuildViewModel();

			Assert.AreEqual(1, result.Users.Count());
		}
	}
}
