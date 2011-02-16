using System.Collections.Generic;
using System.Linq;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.TreeNodeExtensionProvider
{
	public interface IAmATreeNodeExtensionProvider
	{
		IQueryable<IAmATreeNodeExtension> GetAll();
		string Name { get; }
		string ControllerToUseForModification { get; set; }
		string ActionToUseForModification { get; set; }
		string ControllerToUseForCreation { get; set; }
		string ActionToUseForCreation { get; set; }
		IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems { get; set; }
		bool MayHaveChildNodes { get; set; }
		void RegisterRouteForTreeNodeId(string treeNodeId);
	}
}
