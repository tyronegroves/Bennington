using System.Collections.Generic;
using Bennington.Core.MenuSystem;

namespace Bennington.Cms.Models
{
	public class SectionMenuViewModel
	{
		public IEnumerable<IAmASectionMenuItem> MenuItems { get; set; }
	}
}