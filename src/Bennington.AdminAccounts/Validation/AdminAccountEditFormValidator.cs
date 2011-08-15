using System;
using System.Collections.Generic;
using Bennington.AdminAccounts.Data;
using Bennington.AdminAccounts.Models;
using FluentValidation;

namespace Bennington.AdminAccounts.Validation
{
    public class AdminAccountEditFormValidator : AbstractValidator<AdminAccountEditForm>
    {
        public AdminAccountEditFormValidator(IDatabaseRetriever databaseRetriever)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Username)
                .Must(x => false)
                .When(x =>
                          {
                              IEnumerable<dynamic> accounts = databaseRetriever.GetTheDatabase().AdminAccounts.FindAllByUsername(x.Username).Cast<dynamic>();
                              foreach (var account in accounts)
                                  if (account.Id.ToString().ToUpper() != x.Id.ToString().ToUpper())
                                      return true;
                              return false;
                          })
                .WithMessage("This username has already been assigned.");
        }
    }
}