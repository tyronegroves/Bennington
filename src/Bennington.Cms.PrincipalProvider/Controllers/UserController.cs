using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Services;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Bennington.Repository;

namespace Bennington.Cms.PrincipalProvider.Controllers
{
    public class UserController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
    	private readonly IProcessUserInputModelService processUserInputModelService;

    	public UserController(IIndexViewModelBuilder indexViewModelBuilder,
								IModifyViewModelBuilder modifyViewModelBuilder,
								IProcessUserInputModelService processUserInputModelService)
    	{
    		this.processUserInputModelService = processUserInputModelService;
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    		this.indexViewModelBuilder = indexViewModelBuilder;
    	}

		[Authorize]
    	public ActionResult Index()
        {
			return View("Index", indexViewModelBuilder.BuildViewModel());
        }

		[Authorize]
		public ActionResult Modify(UserInputModel userInputModel)
		{
			if (ModelState.IsValid)
				processUserInputModelService.ProcessAndReturnId(userInputModel);

			return View("Modify", modifyViewModelBuilder.BuildViewModel(userInputModel));
		}
    }
}
