using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Cms.PrincipalProvider.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Bennington.Cms.PrincipalProvider.Validators
{
	public class UserInputModelValidator : AbstractValidator<UserInputModel>
	{
		public UserInputModelValidator()
		{
			RuleFor(a => a.Id).Must(b => !string.IsNullOrEmpty(b));
			RuleFor(a => a.Password).Must(b => !string.IsNullOrEmpty(b));
		}

		public override FluentValidation.Results.ValidationResult Validate(UserInputModel instance)
		{
			var results = base.Validate(instance);
			if ((instance.Password != instance.ConfirmPassword) && (!string.IsNullOrEmpty(instance.Password)))
			{
				results.Errors.Add(new ValidationFailure("Password", "Passwords must match"));
				results.Errors.Add(new ValidationFailure("ConfirmPassword", "Passwords must match"));
			}

			return results;
		}
	}
}