using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Paragon.ContentTree.Routing.Content
{
    public class ContentTreeNode : IEnumerable<ContentTreeNode>
    {
		public ContentTreeNode()
        {
            ChildNodes = new ContentTreeNodeCollection(this);
        }

        public string NodeId { get; set; }
        public string UrlSegment { get; set; }
        public ContentTreeNode Parent { get; internal set; }
        public ContentTreeNodeCollection ChildNodes { get; set; }

        public int Depth
        {
            get
            {
                var parent = Parent;
                var depth = 0;
                while(parent != null)
                {
                    parent = parent.Parent;
                    depth++;
                }

                return depth;
            }
        }

        public IEnumerator<ContentTreeNode> GetEnumerator()
        {
            return ChildNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ContentTreeNodeCollection : Collection<ContentTreeNode>
    {
        private readonly ContentTreeNode parentNode;

        public ContentTreeNodeCollection(ContentTreeNode parentNode)
        {
            this.parentNode = parentNode;
        }

        protected override void InsertItem(int index, ContentTreeNode item)
        {
            item.Parent = parentNode;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, ContentTreeNode item)
        {
            item.Parent = parentNode;
            base.SetItem(index, item);
        }

        public ContentTreeNode FindByUrlSegment(string segment)
        {
            return this.Where(node => node.UrlSegment.Equals(segment, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
    }
}