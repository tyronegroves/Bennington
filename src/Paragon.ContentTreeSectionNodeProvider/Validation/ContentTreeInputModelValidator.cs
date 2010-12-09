using Deg.Alt.FluentValidation;
using FluentValidation;
using Paragon.ContentTreeSectionNodeProvider.Models;

namespace Paragon.ContentTreeSectionNodeProvider.Validation
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
