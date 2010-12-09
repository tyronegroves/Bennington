using Deg.Alt.FluentValidation;
using FluentValidation;
using Paragon.ContentTree.SectionNodeProvider.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Validation
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
