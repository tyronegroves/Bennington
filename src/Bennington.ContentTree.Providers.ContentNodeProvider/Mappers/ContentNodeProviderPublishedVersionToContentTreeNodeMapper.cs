using System.Collections.Generic;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Mappers
{
	public interface IContentNodeProviderPublishedVersionToContentTreeNodeMapper
	{
		IEnumerable<ContentTreeNode> CreateSet(IEnumerable<ContentNodeProviderPublishedVersion> source);
	}

	public class ContentNodeProviderPublishedVersionToContentTreeNodeMapper : Mapper<ContentNodeProviderPublishedVersion, ContentTreeNode>, IContentNodeProviderPublishedVersionToContentTreeNodeMapper
	{
	}
}
