using System;
using Bennington.Core.Helpers;
using Bennington.Repository.Helpers;

namespace Bennington.Repository
{
	public class ObjectStore<T>
	{
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;
		private readonly IGetRootPathToSerializeObjectTo<T> getRootPathToSerializeObjectTo;
		private readonly IGetDataPathForType getDataPathForType;
		private readonly IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance;
		private readonly IGuidGetter guidGetter;

		public ObjectStore(IXmlFileSerializationHelper xmlFileSerializationHelper,
							IGetDataPathForType getDataPathForType,
							IGetRootPathToSerializeObjectTo<T> getRootPathToSerializeObjectTo,
							IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance,
							IGuidGetter guidGetter)
		{
			this.guidGetter = guidGetter;
			this.getValueOfIdPropertyForInstance = getValueOfIdPropertyForInstance;
			this.getDataPathForType = getDataPathForType;
			this.getRootPathToSerializeObjectTo = getRootPathToSerializeObjectTo;
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
		}

		public string SaveAndReturnId(T instance)
		{
			var idValue = getValueOfIdPropertyForInstance.GetId(instance) ?? guidGetter.GetGuid().ToString();
			var path = string.Format("{0}{1}.xml", getRootPathToSerializeObjectTo.GetPathFromObjectUsingIdProperty(instance), idValue);
			xmlFileSerializationHelper.SerializeToPath(instance, path);
			return idValue;
		}

		public T GetById(string id)
		{
			var path = string.Format("{0}{1}.xml", getDataPathForType.GetPathForDataByType(typeof(T)) , id);
			return xmlFileSerializationHelper.DeserializeFromPath<T>(path);
		}
	}
}
