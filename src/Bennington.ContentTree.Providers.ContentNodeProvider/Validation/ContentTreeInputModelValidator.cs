using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using FluentValidation;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Validation
{
	public class ContentTreeInputModelValidator : FluentValidation.AbstractValidator<ContentTreeNodeInputModel>
	{
		public ContentTreeInputModelValidator()
		{
			RuleFor(x => x.Name).Must(b => !string.IsNullOrEmpty(b)).When(a => a.Action == "Index" || a.Action == null);
			RuleFor(x => x.UrlSegment).Must(b => !string.IsNullOrEmpty(b)).When(a => a.Action == "Index" || a.Action == null);
		}
	}
}
