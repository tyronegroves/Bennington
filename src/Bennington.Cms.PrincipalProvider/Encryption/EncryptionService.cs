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
			return HashString(s);
		}

		private string HashString(string Value)
		{
			var md5CryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
			var data = System.Text.Encoding.ASCII.GetBytes(Value);
			data = md5CryptoServiceProvider.ComputeHash(data);
			var ret = "";
			for (var i = 0; i < data.Length; i++)
				ret += data[i].ToString("x2").ToLower();
			return ret;
		}
	}
}