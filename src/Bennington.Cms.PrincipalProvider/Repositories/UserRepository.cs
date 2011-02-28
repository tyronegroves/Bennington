using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Core.Helpers;
using Bennington.Repository;
using Bennington.Repository.Helpers;

namespace Bennington.Cms.PrincipalProvider.Repositories
{
	public interface IUserRepository
	{
		IEnumerable<User> GetAll();
		string SaveAndReturnId(User instance);
		void Delete(string id);
	}

	public class UserRepository : ObjectStore<User>, IUserRepository
	{
		public UserRepository(IXmlFileSerializationHelper xmlFileSerializationHelper, IGetDataPathForType getDataPathForType, IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance, IGuidGetter guidGetter, IFileSystem fileSystem) : base(xmlFileSerializationHelper, getDataPathForType, getValueOfIdPropertyForInstance, guidGetter, fileSystem)
		{
		}
	}
}