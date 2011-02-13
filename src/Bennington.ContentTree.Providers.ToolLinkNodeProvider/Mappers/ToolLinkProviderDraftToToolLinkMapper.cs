using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperAssist;
using Paragon.ContentTree.ToolLinkNodeProvider.Data;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Mappers
{
	public interface IToolLinkProviderDraftToToolLinkMapper
	{
		IEnumerable<ToolLink> CreateSet(IEnumerable<ToolLinkProviderDraft> source);
	}

	public class ToolLinkProviderDraftToToolLinkMapper : Mapper<ToolLinkProviderDraft, ToolLink>, IToolLinkProviderDraftToToolLinkMapper
	{
	}
}