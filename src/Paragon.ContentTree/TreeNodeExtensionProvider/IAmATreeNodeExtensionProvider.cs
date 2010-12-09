using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.TreeNodeExtensionProvider
{
	public interface IAmATreeNodeExtensionProvider
	{
		IQueryable<IAmATreeNodeExtension> GetAll();
		string Name { get; }
		string ControllerToUseForModification { get; set; }
		string ActionToUseForModification { get; set; }
		string ControllerToUseForCreation { get; set; }
		string ActionToUseForCreation { get; set; }
		IRouteConstraint IgnoreConstraint { get; }
		IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems { get; set; }
	}
}
