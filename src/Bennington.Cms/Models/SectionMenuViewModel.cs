using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Core.MenuSystem;

namespace Paragon.Cms.Models
{
	public class SectionMenuViewModel
	{
		public IEnumerable<IAmASectionMenuItem> MenuItems { get; set; }
	}
}