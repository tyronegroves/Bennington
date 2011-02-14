using AutoMapperAssist;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Mappers
{
	public interface IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper
	{
		ContentNodeProviderPublishedVersion CreateInstance(ContentNodeProviderDraft source);
	}

	public class ContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper : Mapper<ContentNodeProviderDraft, ContentNodeProviderPublishedVersion>, IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper
	{
	}
}
