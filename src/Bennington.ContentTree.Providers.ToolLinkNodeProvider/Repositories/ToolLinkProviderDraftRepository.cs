using System;
using System.Linq;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Data;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using Bennington.Core.EntityFramework;
using Bennington.Core.Helpers;
using Bennington.Repository;
using Bennington.Repository.Helpers;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Repositories
{
	public interface IToolLinkProviderDraftRepository
	{
		IQueryable<Data.ToolLinkProviderDraft> GetAll();
	    string SaveAndReturnId(ToolLinkProviderDraft toolLinkProviderDraft);
	}

    public class ToolLinkProviderDraftRepository : ObjectStore<ToolLinkProviderDraft>, IToolLinkProviderDraftRepository
	{
	    public ToolLinkProviderDraftRepository(IXmlFileSerializationHelper xmlFileSerializationHelper, IGetDataPathForType getDataPathForType, IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance, IGuidGetter guidGetter, IFileSystem fileSystem) : base(xmlFileSerializationHelper, getDataPathForType, getValueOfIdPropertyForInstance, guidGetter, fileSystem)
	    {
	    }

	    public IQueryable<ToolLinkProviderDraft> GetAll()
	    {
	        return base.GetAll().AsQueryable();
	    }
	}
}