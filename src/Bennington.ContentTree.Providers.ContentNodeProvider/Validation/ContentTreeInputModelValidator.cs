using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Deg.Alt.FluentValidation;
using FluentValidation;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Validation
{
	public class ContentTreeInputModelValidator : AbstractValidator<ContentTreeNodeInputModel>
	{
		public ContentTreeInputModelValidator()
		{
			RuleFor(x => x.Name).IsRequired().When(a => a.Action == "Index" || a.Action == null);
			RuleFor(x => x.UrlSegment).IsRequired().When(a => a.Action == "Index" || a.Action == null);
		}
	}
}
