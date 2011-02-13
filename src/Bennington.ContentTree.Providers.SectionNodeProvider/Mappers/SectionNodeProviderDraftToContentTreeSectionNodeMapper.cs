using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapperAssist;
using Paragon.ContentTree.SectionNodeProvider.Data;
using Paragon.ContentTree.SectionNodeProvider.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Mappers
{
	public interface ISectionNodeProviderDraftToContentTreeSectionNodeMapper
	{
		IEnumerable<ContentTreeSectionNode> CreateSet(IEnumerable<SectionNodeProviderDraft> source);
	}

	public class SectionNodeProviderDraftToContentTreeSectionNodeMapper : Mapper<SectionNodeProviderDraft, ContentTreeSectionNode>, ISectionNodeProviderDraftToContentTreeSectionNodeMapper
	{
	}
}
