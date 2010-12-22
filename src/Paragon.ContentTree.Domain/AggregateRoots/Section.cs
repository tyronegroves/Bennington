namespace Paragon.ContentTree.Domain.AggregateRoots
{
	public class Section : SimpleCqrs.Domain.AggregateRoot
	{
		public void SetUrlSegment(string urlSegment) { }

		public void SetDefaultPage(string pageId) { }

		public void SetParentId(string id) { }

		public void SetSequence(int? sequence) { }
	}
}