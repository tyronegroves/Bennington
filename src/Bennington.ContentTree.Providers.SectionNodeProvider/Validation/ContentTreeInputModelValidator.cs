using Bennington.ContentTree.Providers.SectionNodeProvider.Models;
using FluentValidation;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Validation
{
	public class ContentTreeSectionInputModelValidator : FluentValidation.AbstractValidator<ContentTreeSectionInputModel>
	{
		public ContentTreeSectionInputModelValidator()
		{
			RuleFor(x => x.Name).Must(b => !string.IsNullOrEmpty(b));
			RuleFor(x => x.UrlSegment).Must(b => !string.IsNullOrEmpty(b));
		}
	}
}
