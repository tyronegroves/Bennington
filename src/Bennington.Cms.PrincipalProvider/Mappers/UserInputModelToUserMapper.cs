using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperAssist;
using Bennington.Cms.PrincipalProvider.Models;

namespace Bennington.Cms.PrincipalProvider.Mappers
{
	public interface IUserInputModelToUserMapper
	{
		User CreateInstance(UserInputModel source);
	}

	public class UserInputModelToUserMapper : Mapper<UserInputModel, User>, IUserInputModelToUserMapper
	{
		public override void DefineMap(AutoMapper.IConfiguration configuration)
		{
			configuration.CreateMap<UserInputModel, User>()
					.ForMember(a => a.Groups, b => b.Ignore())
				;
		}
	}
}