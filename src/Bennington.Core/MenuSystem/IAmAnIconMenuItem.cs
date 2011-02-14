namespace Bennington.Core.MenuSystem
{
	public interface IAmAnIconMenuItem
	{
		string Name { get; }
		string IconUrl { get; }
		string Controller { get; }
		string Action { get; }
		object RouteValues { get; }
	}
}
