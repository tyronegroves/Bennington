using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.Data;

namespace Paragon.Pages.Adapters
{
	//public interface INodeSegmentEnumerableToTreeNodeAdapter
	//{
	//    TreeNode GetTreeNodeFromUrl(IEnumerable<string> treeNodeSegments);
	//}

	//public class NodeSegmentEnumerableToTreeNodeAdapter : INodeSegmentEnumerableToTreeNodeAdapter
	//{
	//    private readonly IContentTreeRepository contentTreeRepository;
	//    private readonly IContentTreeNodeToTreeNodeMapper contentTreeNodeToTreeNodeMapper;

	//    public NodeSegmentEnumerableToTreeNodeAdapter(IContentTreeRepository contentTreeRepository, IContentTreeNodeToTreeNodeMapper contentTreeNodeToTreeNodeMapper)
	//    {
	//        this.contentTreeNodeToTreeNodeMapper = contentTreeNodeToTreeNodeMapper;
	//        this.contentTreeRepository = contentTreeRepository;
	//    }

	//    public TreeNode GetTreeNodeFromUrl(IEnumerable<string> treeNodeSegments)
	//    {
	//        var rootNode = contentTreeRepository.GetRootNode();

	//        var treeNodeToReturn = rootNode;

	//        foreach(var treeNodeSegment in treeNodeSegments)
	//        {
	//            if (treeNodeToReturn.ChildNodes == null) break;

	//            var potentialTreeNodeToReturn = treeNodeToReturn.ChildNodes.Where(a => a.UrlSegment == treeNodeSegment).FirstOrDefault();
	//            if (potentialTreeNodeToReturn == null) break;
				
	//            treeNodeToReturn = potentialTreeNodeToReturn;
	//        }

	//        return contentTreeNodeToTreeNodeMapper.CreateInstance(treeNodeToReturn);
	//    }
	//}
}