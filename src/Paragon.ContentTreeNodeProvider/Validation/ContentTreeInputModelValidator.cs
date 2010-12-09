using Deg.Alt.FluentValidation;
using FluentValidation;
using Paragon.ContentTreeNodeProvider.Models;

namespace Paragon.ContentTreeNodeProvider.Validation
{
	public class ContentTreeInputModelValidator : AbstractValidator<ContentTreeNodeInputModel>
	{
		public ContentTreeInputModelValidator()
		{
			RuleFor(x => x.Name).IsRequired();
			RuleFor(x => x.UrlSegment).IsRequired();
		}
	}
}
