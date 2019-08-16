namespace SignalRConsoleClient
{
    using System;
    using System.Net.Http;
    using System.Text;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public static class Program
    {
        private const string BaseUrl = "https://localhost:44317";

        public static void Main()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl($"{BaseUrl}/coffeehub")
                .AddMessagePackProtocol()
                .Build();

            connection.On<Order>(
                "NewOrder",
                (order) => Console.WriteLine($"Somebody ordered an {order.Product} ({order.Size})"));

            connection.On<string>("ReceiveOrderUpdate", Console.WriteLine);

            connection.StartAsync().GetAwaiter().GetResult();

            Console.WriteLine("Listening. Press enter to exit...");

            Console.WriteLine("Eneter 'order {ProductName} {PreductSize}' if you want to order coffee.");
            string command =  Console.ReadLine();
            if (command.StartsWith("order"))
            {
                CreateOrder(connection, command);
            }

            Console.ReadLine();
        }

        private static void CreateOrder(HubConnection connection, string command)
        {
            string[] data = command.Split();
            string prodcutName = data[1];
            string size = data[2];
            string json = JsonConvert.SerializeObject(new Order() { Product = prodcutName, Size = size });
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsync($"{BaseUrl}/coffee/",
                new StringContent(json, Encoding.UTF8, "application/json"))
                .GetAwaiter().GetResult();

            Console.WriteLine(response.StatusCode);
            int id = int.Parse(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            Console.WriteLine($"Order id: {id}");

            connection.InvokeAsync("GetUpdateForOrder", id).GetAwaiter().GetResult();
        }
    }
}
