using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Bennington.Core.Helpers
{
	public interface IXmlFileSerializationHelper
	{
		void SerializeListToPath<T>(List<T> data, string path);
		List<T> DeserializeListFromPath<T>(string path);
	}

	public class XmlFileSerializationHelper : IXmlFileSerializationHelper
	{
		public void SerializeListToPath<T>(List<T> data, string path)
		{
			CreateEmptyFileIfItDoesntExist<T>(path);

			var serializer = new XmlSerializer(typeof(List<T>));
			TextWriter textWriter = new StreamWriter(path);
			serializer.Serialize(textWriter, data);
			textWriter.Close();
		}

		public List<T> DeserializeListFromPath<T>(string path)
		{
			CreateEmptyFileIfItDoesntExist<T>(path);

			var deserializer = new XmlSerializer(typeof(List<T>));
			TextReader textReader = new StreamReader(path);
			var data = (List<T>)deserializer.Deserialize(textReader);
			textReader.Close();

			return data;
		}

		private void CreateEmptyFileIfItDoesntExist<T>(string path)
		{
			if (!File.Exists(path))
			{
				using (System.IO.File.Create(path)){}

				var serializer = new XmlSerializer(typeof(List<T>));
				TextWriter textWriter = new StreamWriter(path);
				serializer.Serialize(textWriter, new List<T>());
				textWriter.Close();	
			}
		}
	}
}
