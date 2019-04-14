using System;
using System.Threading.Tasks;
using AwesomeShop.AzureFunctions.Infrastructure;
using AwesomeShop.AzureQueueLibrary.Infrastructure;
using AwesomeShop.AzureQueueLibrary.QueueConnection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AwesomeShop.AzureQueueLibrary.Messages;

namespace AwesomeShop.AzureFunctions.Email
{
    public static class EmailQueueTrigger
    {
        [FunctionName("EmailQueueTrigger")]
        public static async Task Run(
			[QueueTrigger(RouteNames.EmailBox, Connection = "AzureWebJobsStorage")]
			string message,
			ILogger log)
        {
			try
			{
				var queueCommunicator = DIContainer.Instance.GetService<IQueueCommunicator>();
				var command = queueCommunicator.Read<SendEmailCommand>(message);

				var handler = DIContainer.Instance.GetService<ISendEmailCommandHandler>();
				await handler.Handle(command);
			}
			catch (Exception ex)
			{
				log.LogError(ex, $"Something went wrong with the EmailQueueTrigger {message}");
				throw;
			}
        }
    }
}
