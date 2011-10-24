using System.Linq;
using Bennington.ContentTree.Data;
using Bennington.ContentTree.Domain.Events.TreeNode;
using Bennington.Core.SisoDb;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Denormalizers
{
    public class TreeNodeDenormalizer : DatabaseFactory, IHandleDomainEvents<TreeNodeDeletedEvent>,
										IHandleDomainEvents<TreeNodeCreatedEvent>,
										IHandleDomainEvents<TreeNodeTypeSetEvent>,
										IHandleDomainEvents<TreeNodeParentTreeNodeIdSetEvent>
	{
		private TreeNode GetTreeNodeFromDomainEvent(DomainEvent domainEvent)
		{
            using (var queryEngine = database.CreateQueryEngine())
            {
                return queryEngine.Where<TreeNode>(x => x.Id == domainEvent.AggregateRootId.ToString()).First();
            }
        }

		public void Handle(TreeNodeCreatedEvent domainEvent)
		{
            var treeNode = new TreeNode
            {
                Id = domainEvent.AggregateRootId.ToString(),
            };

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                unitOfWork.Insert(treeNode);
                unitOfWork.Commit();
            }
		}

        public void Handle(TreeNodeDeletedEvent treeNodeDeletedEvent)
        {
            TreeNode treeNode;

            using (var queryEngine = database.CreateQueryEngine())
            {
                treeNode = queryEngine.Where<TreeNode>(x => x.Id == treeNodeDeletedEvent.AggregateRootId.ToString()).First();
            }

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                unitOfWork.DeleteById<TreeNode>(treeNode.SisoId);
                unitOfWork.Commit();
            }
        }

		public void Handle(TreeNodeTypeSetEvent domainEvent)
		{
			var treeNode = GetTreeNodeFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                treeNode.Type = domainEvent.Type.AssemblyQualifiedName;
                unitOfWork.Update(treeNode);
                unitOfWork.Commit();
            }
		}

		public void Handle(TreeNodeParentTreeNodeIdSetEvent domainEvent)
		{
			var treeNode = GetTreeNodeFromDomainEvent(domainEvent);

            using (var unitOfWork = database.CreateUnitOfWork())
            {
                treeNode.ParentTreeNodeId = domainEvent.ParentTreeNodeId.ToString();
                unitOfWork.Update(treeNode);
                unitOfWork.Commit();
            }
		}
	}
}