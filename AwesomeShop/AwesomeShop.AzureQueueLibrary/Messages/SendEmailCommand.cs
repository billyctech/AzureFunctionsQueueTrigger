using AwesomeShop.AzureQueueLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.Messages
{
	public class SendEmailCommand : BaseQueueMessage
	{
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

		public SendEmailCommand() 
			: base(RouteNames.EmailBox)
		{
		}
	}
}
