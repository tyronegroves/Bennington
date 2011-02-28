using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Bennington.Cms.PrincipalProvider.Models
{
	public class UserInputModel
	{
		[DisplayName("Username")]
		public string Id { get; set; }

		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string LastName { get; set; }

		public string Password { get; set; }

		[DisplayName("Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}