using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.MessageSerializer
{
	public class JsonMessageSerializer : IMessageSerializer
	{
		public T Deserialize<T>(string message)
		{
			var obj = JsonConvert.DeserializeObject<T>(message);
			return obj;
		}

		public string Serialize(object obj)
		{
			var message = JsonConvert.SerializeObject(obj);
			return message;
		}
	}
}
