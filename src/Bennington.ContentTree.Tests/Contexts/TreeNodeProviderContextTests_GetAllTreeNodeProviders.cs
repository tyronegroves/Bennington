using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using AutoMoq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Models;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using Bennington.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Tests.Contexts
{
	[TestClass]
	public class TreeNodeProviderContextTests_GetAllTreeNodeProviders
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_instances_of_all_providers()
		{
			var fake1IAmATreeNodeExtensionProvider = new Fake2IAmATreeNodeExtensionProvider();
			var fake2IAmATreeNodeExtensionProvider = new Fake1IAmATreeNodeExtensionProvider();
			mocker.GetMock<IServiceLocatorWrapper>().Setup(a => a.ResolveServices<IAmATreeNodeExtensionProvider>())
				.Returns(new IAmATreeNodeExtensionProvider[]
				         	{
				         		fake1IAmATreeNodeExtensionProvider,
								fake2IAmATreeNodeExtensionProvider
							});

			var treeNodeProviderContext = mocker.Resolve<TreeNodeProviderContext>();
			var providers = treeNodeProviderContext.GetAllTreeNodeProviders();

			Assert.AreEqual(2, providers.Count());
			Assert.AreEqual(true, providers.Any(a => a == fake1IAmATreeNodeExtensionProvider));
			Assert.AreEqual(true, providers.Any(a => a == fake2IAmATreeNodeExtensionProvider));
		}
	}

	public class Fake2IAmATreeNodeExtensionProvider : IAmATreeNodeExtensionProvider
	{
		public IQueryable<IAmATreeNodeExtension> GetAll()
		{
			throw new NotImplementedException();
		}

		public string Name
		{
			get { throw new NotImplementedException(); }
		}

		public string ControllerToUseForProcessing
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ControllerToUseForModification
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForModification
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ControllerToUseForCreation
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForCreation
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public IRouteConstraint IgnoreConstraint
		{
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public bool MayHaveChildNodes
		{
			get { return false; }
			set { throw new NotImplementedException(); }
		}
	}

	public class Fake1IAmATreeNodeExtensionProvider : IAmATreeNodeExtensionProvider
	{
		public IQueryable<IAmATreeNodeExtension> GetAll()
		{
			throw new NotImplementedException();
		}

		public string Name
		{
			get { throw new NotImplementedException(); }
		}

		public string ControllerToUseForProcessing
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ControllerToUseForModification
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForModification
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ControllerToUseForCreation
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string ActionToUseForCreation
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public IRouteConstraint IgnoreConstraint
		{
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public bool MayHaveChildNodes
		{
			get { return false; }
			set { throw new NotImplementedException(); }
		}
	}
}
