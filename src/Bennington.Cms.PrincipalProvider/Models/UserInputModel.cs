using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bennington.Cms.PrincipalProvider.Models
{
	public class UserInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		[DisplayName("Username")]
		public string Username { get; set; }

		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string LastName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		[DisplayName("Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}