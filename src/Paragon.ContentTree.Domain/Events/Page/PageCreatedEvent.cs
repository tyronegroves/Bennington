﻿using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageCreatedEvent : DomainEvent
	{
		public Type ProviderType { get; set; }
	}
}