using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;
using Bennington.ContentTree.Providers.SectionNodeProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Tests
{
	[TestClass]
	public class SectionNodeProviderTests_GetAll
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_sections_from_repository_which_are_not_marked_as_inactive_when_IVersionContext_does_not_return_Manage()
		{
			mocker.GetMock<IContentTreeSectionNodeRepository>().Setup(a => a.GetAllContentTreeSectionNodes())
				.Returns(new ContentTreeSectionNode[]
				         	{
				         		new ContentTreeSectionNode(), 
								new ContentTreeSectionNode()
									{
										Inactive = true
									}, 
							}.AsQueryable());

			var result = mocker.Resolve<SectionNodeProvider>().GetAll();

			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public void Returns_sections_from_repository_which_are_not_marked_as_inactive_when_IVersionContext_returns_Manage()
		{
			mocker.GetMock<IVersionContext>().Setup(a => a.GetCurrentVersionId()).Returns(VersionContext.Manage);
			mocker.GetMock<IContentTreeSectionNodeRepository>().Setup(a => a.GetAllContentTreeSectionNodes())
				.Returns(new ContentTreeSectionNode[]
				         	{
				         		new ContentTreeSectionNode(), 
								new ContentTreeSectionNode()
									{
										Inactive = true
									}, 
							}.AsQueryable());

			var result = mocker.Resolve<SectionNodeProvider>().GetAll();

			Assert.AreEqual(2, result.Count());
		}
	}
}
