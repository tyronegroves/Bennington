using Deg.Alt.FluentValidation;
using FluentValidation;
using Paragon.ContentTree.ContentNodeProvider.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Validation
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
