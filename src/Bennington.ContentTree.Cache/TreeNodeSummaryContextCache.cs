using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.Cache
{
    public class TreeNodeSummaryContextCache : ITreeNodeSummaryContext
    {
        private readonly TreeNodeSummaryContext treeNodeSummaryContextImplementation;
        private readonly ConcurrentDictionary<string, TreeNodeSummary[]> treeNodeSummaryCacheByParentId = new ConcurrentDictionary<string, TreeNodeSummary[]>(); 
        private readonly ConcurrentDictionary<string, TreeNodeSummary> treeNodeSummaryCacheById = new ConcurrentDictionary<string, TreeNodeSummary>(); 

        public TreeNodeSummaryContextCache(TreeNodeSummaryContext treeNodeSummaryContextImplementation)
        {
            this.treeNodeSummaryContextImplementation = treeNodeSummaryContextImplementation;
        }

        public TreeNodeSummary GetTreeNodeSummaryByTreeNodeId(string nodeId)
        {
            TreeNodeSummary treeNodeSummary;
            if (!treeNodeSummaryCacheById.TryGetValue(nodeId, out treeNodeSummary))
            {
                treeNodeSummary = treeNodeSummaryContextImplementation.GetTreeNodeSummaryByTreeNodeId(nodeId);
                treeNodeSummaryCacheById.TryAdd(nodeId, treeNodeSummary);
            }
            
            return treeNodeSummary;
        }

        public IEnumerable<TreeNodeSummary> GetChildren(string parentNodeId)
        {
            TreeNodeSummary[] treeNodeSummaries;
            if (!treeNodeSummaryCacheByParentId.TryGetValue(parentNodeId, out treeNodeSummaries))
            {
                treeNodeSummaries = treeNodeSummaryContextImplementation.GetChildren(parentNodeId).ToArray();
                treeNodeSummaryCacheByParentId.TryAdd(parentNodeId, treeNodeSummaries);
            }
            return treeNodeSummaries;
        }

        public string Create(string parentNodeId, string providerTypeAssemblyQualifiedName)
        {
            return treeNodeSummaryContextImplementation.Create(parentNodeId, providerTypeAssemblyQualifiedName);
        }
    }
}
