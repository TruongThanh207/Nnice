using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NNice.Web.Models;
using NNice.Web.Helpers;
namespace TestConnectApi
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            var client = new HttpClient();
            // Update port # in the following line.
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTA1MjEyMzAsImV4cCI6MTU5MTEyNjAzMCwiaWF0IjoxNTkwNTIxMjMwfQ.naevWmP9faA-J_uuwcrb9ud1vj8KBxMBFz0FEOHp1lc");
            client.BaseAddress = new Uri("https://localhost:4000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Connector connector = new Connector(client);

            try
            {
                // Create a new product
                var room = new RoomViewModel
                {
                    Name = "Room3",
                    Capacity = 12,
                    IsAvailable = true
                };

                //var url = await connector.CreateAsync<RoomViewModel>(Room);
                //Console.WriteLine($"Created at {url}");

                // Get the product
                //var room = await connector.GetAsync<RoomViewModel>("api/Accounts");
                //ShowProduct(product);

                //// Update the product
                //Console.WriteLine("Updating price...");
                room.Name = "Room5";
                room.ID = 4;
                var response = await connector.UpdateAsync<RoomViewModel>(room, $"api/room/{room.ID}");

                //// Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Delete the product
                //var statusCode = await connector.DeleteAsync($"api/room/{room.ID}");
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

                var model = new AccountViewModel()
                {
                    Username = "accountant",
                    Password = "accountant",
                };
                var account = await connector.Authencate(model);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
