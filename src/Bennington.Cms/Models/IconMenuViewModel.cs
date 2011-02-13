using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Core.MenuSystem;

namespace Paragon.Cms.Models
{
	public class IconMenuViewModel
	{
		public IEnumerable<IAmAnIconMenuItem> IconMenuItems { get; set; }
	}
}