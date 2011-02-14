using System.Collections.Generic;
using AutoMapperAssist;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Mappers
{
	public interface ISectionNodeProviderDraftToContentTreeSectionNodeMapper
	{
		IEnumerable<ContentTreeSectionNode> CreateSet(IEnumerable<SectionNodeProviderDraft> source);
	}

	public class SectionNodeProviderDraftToContentTreeSectionNodeMapper : Mapper<SectionNodeProviderDraft, ContentTreeSectionNode>, ISectionNodeProviderDraftToContentTreeSectionNodeMapper
	{
	}
}
