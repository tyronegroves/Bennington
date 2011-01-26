using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapperAssist;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Mappers
{
	public interface IContentNodeProviderPublishedVersionToContentTreeNodeMapper
	{
		IEnumerable<ContentTreeNode> CreateSet(IEnumerable<ContentNodeProviderPublishedVersion> source);
	}

	public class ContentNodeProviderPublishedVersionToContentTreeNodeMapper : Mapper<ContentNodeProviderPublishedVersion, ContentTreeNode>, IContentNodeProviderPublishedVersionToContentTreeNodeMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<ContentNodeProviderDraft, ContentTreeNode>();
		}

		public override IEnumerable<ContentTreeNode> CreateSet(IEnumerable<ContentNodeProviderPublishedVersion> source)
		{
			return base.CreateSet(source);
		}
	}
}
