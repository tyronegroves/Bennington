using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperAssist;
using Bennington.Cms.PrincipalProvider.Models;

namespace Bennington.Cms.PrincipalProvider.Mappers
{
	public interface IUserToUserInputModelMapper
	{
		UserInputModel CreateInstance(User source);
	}

	public class UserToUserInputModelMapper : Mapper<Models.User, Models.UserInputModel>, IUserToUserInputModelMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<Models.User, Models.UserInputModel>()
					.ForMember(a => a.Password, b => b.Ignore())
					.ForMember(a => a.ConfirmPassword, b => b.Ignore())
				;
		}
	}
}