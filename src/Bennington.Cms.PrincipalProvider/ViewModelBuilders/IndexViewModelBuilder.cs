using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;

namespace Bennington.Cms.PrincipalProvider.ViewModelBuilders
{
	public interface IIndexViewModelBuilder
	{
		IndexViewModel BuildViewModel();
	}

	public class IndexViewModelBuilder : IIndexViewModelBuilder
	{
		private readonly IUserRepository userRepository;

		public IndexViewModelBuilder(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public IndexViewModel BuildViewModel()
		{
			return new IndexViewModel()
			       	{
						Users = userRepository.GetAll(),
			       	};
		}
	}
}