using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bennington.Cms.PrincipalProvider.Models
{
	public class IndexViewModel
	{
		public IEnumerable<User> Users { get; set; }
	}
}