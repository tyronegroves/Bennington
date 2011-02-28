using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Cms.PrincipalProvider.Models;

namespace Bennington.Cms.PrincipalProvider.ViewModelBuilders
{
	public interface IModifyViewModelBuilder
	{
		ModifyViewModel BuildViewModel(UserInputModel userInputModel);
	}

	public class ModifyViewModelBuilder : IModifyViewModelBuilder
	{
		public ModifyViewModel BuildViewModel(UserInputModel userInputModel)
		{
			return new ModifyViewModel()
			       	{
			       		UserInputModel = userInputModel ?? new UserInputModel()
			       	};
		}
	}
}