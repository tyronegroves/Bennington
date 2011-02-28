using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Cms.PrincipalProvider.Mappers;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;

namespace Bennington.Cms.PrincipalProvider.Services
{
	public interface IProcessUserInputModelService
	{
		string ProcessAndReturnId(UserInputModel userInputModel);
	}

	public class ProcessUserInputModelService : IProcessUserInputModelService
	{
		private readonly IUserInputModelToUserMapper userInputModelToUserMapper;
		private readonly IUserRepository userRepository;

		public ProcessUserInputModelService(IUserInputModelToUserMapper userInputModelToUserMapper,
										IUserRepository userRepository)
		{
			this.userRepository = userRepository;
			this.userInputModelToUserMapper = userInputModelToUserMapper;
		}

		public string ProcessAndReturnId(UserInputModel userInputModel)
		{
			return userRepository.SaveAndReturnId(userInputModelToUserMapper.CreateInstance(userInputModel));
		}
	}
}