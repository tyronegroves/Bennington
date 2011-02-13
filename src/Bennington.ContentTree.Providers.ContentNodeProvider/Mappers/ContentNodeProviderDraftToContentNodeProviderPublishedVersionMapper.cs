using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapperAssist;
using Paragon.ContentTree.ContentNodeProvider.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Mappers
{
	public interface IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper
	{
		ContentNodeProviderPublishedVersion CreateInstance(ContentNodeProviderDraft source);
	}

	public class ContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper : Mapper<ContentNodeProviderDraft, ContentNodeProviderPublishedVersion>, IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper
	{
	}
}
