using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.PrincipalProvider.Mappers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;
using Bennington.Cms.PrincipalProvider.Services;
using Bennington.Cms.PrincipalProvider.ViewModelBuilders;
using Bennington.Core.Helpers;
using Bennington.Repository;

namespace Bennington.Cms.PrincipalProvider.Controllers
{
    public class UserController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
    	private readonly IProcessUserInputModelService processUserInputModelService;
    	private readonly IUserRepository userRepository;
    	private readonly IUserToUserInputModelMapper userToUserInputModelMapper;
    	private readonly IGuidGetter guidGetter;

    	public UserController(IIndexViewModelBuilder indexViewModelBuilder,
								IModifyViewModelBuilder modifyViewModelBuilder,
								IProcessUserInputModelService processUserInputModelService,
								IUserRepository userRepository,
								IUserToUserInputModelMapper userToUserInputModelMapper,
								IGuidGetter guidGetter)
    	{
    		this.guidGetter = guidGetter;
    		this.userToUserInputModelMapper = userToUserInputModelMapper;
    		this.userRepository = userRepository;
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
		public ActionResult Create()
		{
			return View("Modify", new ModifyViewModel()
										{
											UserInputModel = new UserInputModel()
											{
												Id = guidGetter.GetGuid().ToString()
											}
										});
		}

		[Authorize]
		[HttpPost]
		public ActionResult Create(UserInputModel userInputModel)
		{
			return Modify(userInputModel);
		}

		[Authorize]
		public ActionResult Modify(string id)
		{
			var user = userRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

			return View("Modify", modifyViewModelBuilder.BuildViewModel(userToUserInputModelMapper.CreateInstance(user)));
		}

		[Authorize]
		[HttpPost]
		public ActionResult Modify(UserInputModel userInputModel)
		{
			if (ModelState.IsValid)
			{
				processUserInputModelService.ProcessAndReturnId(userInputModel);
				var routeValues = new RouteValueDictionary();
				routeValues.Add("Controller", "User");
				routeValues.Add("Action", "Modify");
				routeValues.Add("id", userInputModel.Id);

				return new RedirectToRouteResult(routeValues);
			}

			return View("Modify", modifyViewModelBuilder.BuildViewModel(userInputModel));
		}

		[Authorize]
		public ActionResult Delete(string id)
		{
			var routeValues = new RouteValueDictionary();
			routeValues.Add("Controller", "User");
			routeValues.Add("Action", "Index");
			
			userRepository.Delete(id);

			return new RedirectToRouteResult(routeValues);
		}
    }
}
