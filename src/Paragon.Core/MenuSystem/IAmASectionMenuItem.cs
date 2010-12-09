namespace Paragon.Core.MenuSystem
{
	public interface IAmASectionMenuItem
	{
		string Name { get; }
		string Controller { get; }
		string Action { get; }
		object RouteValues { get; }
	}
}
