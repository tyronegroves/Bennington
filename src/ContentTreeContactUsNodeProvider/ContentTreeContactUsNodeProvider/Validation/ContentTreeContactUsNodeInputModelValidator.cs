using ContentTreeContactUsNodeProvider.Models;
using Deg.Alt.FluentValidation;
using FluentValidation;

namespace ContentTreeContactUsNodeProvider.Validation
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
