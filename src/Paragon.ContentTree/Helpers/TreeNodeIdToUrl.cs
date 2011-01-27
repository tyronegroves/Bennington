using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paragon.ContentTree.Helpers
{
	public interface ITreeNodeIdToUrl
	{
		string GetUrlByTreeNodeId(string treeNodeId);
	}

	public class TreeNodeIdToUrl : ITreeNodeIdToUrl
	{
		public string GetUrlByTreeNodeId(string treeNodeId)
		{
			throw new NotImplementedException();
		}
	}
}