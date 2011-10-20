using System.Linq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.TreeManager.Models;

namespace Bennington.ContentTree.TreeManager.ViewModelBuilders
{
    public interface ITreeManagerIndexViewModelBuilder
    {
        TreeManagerIndexViewModel BuildViewModel();
    }

    public class TreeManagerIndexViewModelBuilder : ITreeManagerIndexViewModelBuilder
    {
        private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

        public TreeManagerIndexViewModelBuilder(ITreeNodeSummaryContext treeNodeSummaryContext)
        {
            this.treeNodeSummaryContext = treeNodeSummaryContext;
        }

        public TreeManagerIndexViewModel BuildViewModel()
        {
            return new TreeManagerIndexViewModel()
                       {
                           ContentTreeHasNodes = treeNodeSummaryContext.GetChildren(Constants.RootNodeId).Any(),
                           TreeNodeCreationInputModel = new TreeNodeCreationInputModel()
                                                            {
                                                                ParentTreeNodeId = Constants.RootNodeId
                                                            }
                       };
        }
    }
}