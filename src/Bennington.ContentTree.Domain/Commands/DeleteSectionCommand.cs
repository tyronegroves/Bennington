using System;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.Commands
{
	public class DeleteSectionCommand : CommandWithAggregateRootId
	{
	    public string LastModifyBy { get; set; }
	}
}
