using Deg.Alt.FluentValidation;
using FluentValidation;
using Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Models;

namespace Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Validation
{
	public class ContentTreeSectionInputModelValidator : AbstractValidator<ContentTreeContactUsNodeInputModel>
	{
		public ContentTreeSectionInputModelValidator()
		{
			RuleFor(x => x.Name).IsRequired();
			RuleFor(x => x.UrlSegment).IsRequired();
		}
	}
}
