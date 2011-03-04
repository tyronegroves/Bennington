using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bennington.Cms.PrincipalProvider.Encryption
{
	public interface IEncryptionService
	{
		string Encrypt(string s);
	}

	public class EncryptionService : IEncryptionService
	{
		public string Encrypt(string s)
		{
			throw new NotImplementedException();
		}
	}
}