using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.Core.MenuSystem;

namespace Paragon.Cms.Models
{
	public class IconMenuViewModel
	{
		public IEnumerable<IAmAnIconMenuItem> IconMenuItems { get; set; }
	}
}