using Bennington.ContentTree.Providers.SectionNodeProvider.Models;
using Deg.Alt.FluentValidation;
using FluentValidation;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Validation
{
	public class ContentTreeSectionInputModelValidator : AbstractValidator<ContentTreeSectionInputModel>
	{
		public ContentTreeSectionInputModelValidator()
		{
			RuleFor(x => x.Name).IsRequired();
			RuleFor(x => x.UrlSegment).IsRequired();
		}
	}
}
