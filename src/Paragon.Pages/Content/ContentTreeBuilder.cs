using MvcTurbine;
using Paragon.Pages.Data;

namespace Paragon.Pages.Content
{
	public interface IContentTreeBuilder
	{
		ContentTree GetContentTree();
	}

    public class ContentTreeBuilder : IContentTreeBuilder
    {
        private readonly IContentTreeRepository contentTreeRepository;

        public ContentTreeBuilder(IContentTreeRepository contentTreeRepository)
        {
            this.contentTreeRepository = contentTreeRepository;
        }

        public ContentTree GetContentTree()
        {
            var rootNode = contentTreeRepository.GetRootNode();
            FillNodeWithChildNodes(rootNode);

            return new ContentTree(rootNode);
        }

        private void FillNodeWithChildNodes(ContentTreeNode node)
        {
            var childNodes = contentTreeRepository.GetChildNodesForNode(node);
            foreach(var childNode in childNodes)
                node.ChildNodes.Add(childNode);
            
            node.ChildNodes.ForEach(FillNodeWithChildNodes);
        }
    }
}