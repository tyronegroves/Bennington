using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using FluentValidation;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Validators
{
    public class ToolLinkInputModelValidator : FluentValidation.AbstractValidator<ToolLinkInputModel>
    {
        public ToolLinkInputModelValidator()
        {
            RuleFor(x => x.Name).Must(b => !string.IsNullOrEmpty(b));
            RuleFor(x => x.UrlSegment).Must(b => !string.IsNullOrEmpty(b));
            RuleFor(x => x.Url).Must(b => !string.IsNullOrEmpty(b));
        }
    }
}