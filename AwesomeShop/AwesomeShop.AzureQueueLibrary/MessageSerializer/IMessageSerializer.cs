using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.MessageSerializer
{
	public interface IMessageSerializer
	{
		T Deserialize<T>(string message);
		string Serialize(object obj);
	}
}
