using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bennington.Repository.Helpers
{
	public interface IGetValueOfIdPropertyForInstance
	{
		string GetId(object o);
	}

	public class GetValueOfIdPropertyForInstance : IGetValueOfIdPropertyForInstance
	{
		private readonly IGetNameOfIdPropertyForType getNameOfIdPropertyForType;

		public GetValueOfIdPropertyForInstance(IGetNameOfIdPropertyForType getNameOfIdPropertyForType)
		{
			this.getNameOfIdPropertyForType = getNameOfIdPropertyForType;
		}

		public string GetId(object o)
		{
			string returnValue = null;

			var nameOfIdPropertyFieldOrProperty = getNameOfIdPropertyForType.GetNameOfIdProperty(o.GetType());
			var idPropertyInformation = o.GetType().GetProperty(nameOfIdPropertyFieldOrProperty, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			
			if (idPropertyInformation == null)
			{
			    var fieldInfo = o.GetType().GetField(nameOfIdPropertyFieldOrProperty,
			                                 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static |
			                                 BindingFlags.Public);

                if (nameOfIdPropertyFieldOrProperty == null)
                    throw new Exception("Could not find id property for type " + o.GetType().FullName);

				return fieldInfo.GetValue(o).ToString();
			}

			return idPropertyInformation.GetValue(o, null).ToString();
		}
	}
}