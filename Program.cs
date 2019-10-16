namespace VAGAppPoc
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;

    class Program
    {
        const string conString = "Endpoint=sb://vagpoc.servicebus.windows.net/;SharedAccessKeyName=VAGPoc;SharedAccessKey=E43y03L7ICg/VNMfESbhKqpXT4rFVICr9MbnrHz4aew=";
        const string queueName = "vagqueue";
        static IQueueClient qClient;

        static void Main(string[] args)
        {
            asyncMain().GetAwaiter().GetResult();
        }

        static async Task asyncMain()
        {
            const int numberOfMessages = 10;
            qClient = new QueueClient(conString, queueName);
            await SendMessagesAsync(numberOfMessages);
            Console.ReadKey();
            await qClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {                
                string messageBody = @"{""id"": ""VGDoc-12345"",""name"": ""VikramPOC"",""zipcode"": ""500021""}";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                Console.WriteLine($"Sending message: {messageBody}");
                await qClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went Wrong!!!" + ex.Message);
            }
        }
    }
}
