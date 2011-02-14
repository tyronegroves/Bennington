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

namespace Bennington.ContentTree.Tests.TreeNodeExtensionProvider
{
	[TestClass]
	public class GetAllContentTreeNodeProvidersTests_GetAll
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void INit()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_service_that_implements_IAmATreeNodeExtensionProvider_and_matches_specified_type()
		{
			mocker.GetMock<IServiceLocatorWrapper>().Setup(a => a.ResolveServices<IAmATreeNodeExtensionProvider>())
				.Returns(new List<IAmATreeNodeExtensionProvider>()
				         	{
								new IamATreeNodeExtensionProvider1(),
								new IamATreeNodeExtensionProvider2(),
				         	});

			var getAllContentTreeNodeProviders = mocker.Resolve<TreeNodeProviderContext>();
			var result = getAllContentTreeNodeProviders.GetProviderByTypeName(typeof (IamATreeNodeExtensionProvider2).AssemblyQualifiedName);

			Assert.AreEqual("IamATreeNodeExtensionProvider2", result.Name);
		}

		private class IamATreeNodeExtensionProvider2 : IAmATreeNodeExtensionProvider
		{
			public IQueryable<IAmATreeNodeExtension> GetAll()
			{
				throw new NotImplementedException();
			}

			public string Name
			{
				get { return "IamATreeNodeExtensionProvider2"; }
			}

			public string ControllerToUseForProcessing
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public string Type
			{
				get { throw new NotImplementedException(); }
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

		private class IamATreeNodeExtensionProvider1 : IAmATreeNodeExtensionProvider
		{
			public IQueryable<IAmATreeNodeExtension> GetAll()
			{
				throw new NotImplementedException();
			}

			public string Name
			{
				get { return "IamATreeNodeExtensionProvider1"; }
			}

			public string ControllerToUseForProcessing
			{
				get { throw new NotImplementedException(); }
				set { throw new NotImplementedException(); }
			}

			public string Type
			{
				get { throw new NotImplementedException(); }
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
}
