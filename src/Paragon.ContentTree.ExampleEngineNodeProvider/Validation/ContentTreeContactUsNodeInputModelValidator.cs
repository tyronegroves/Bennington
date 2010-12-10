using Deg.Alt.FluentValidation;
using FluentValidation;
using Paragon.ContentTree.ExampleEngineNodeProvider.Models;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Validation
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
