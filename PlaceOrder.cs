using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace vh_serverless
{
    public class Order
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
    }

    public class PlaceOrder
    {
        private readonly ILogger<PlaceOrder> _logger;

        public PlaceOrder(ILogger<PlaceOrder> logger)
        {
            _logger = logger;
        }

        [Function("PlaceOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            Order? order = await req.ReadFromJsonAsync<Order>();
            if (order == null)
            {
                return new BadRequestObjectResult("Invalid request");
            }

            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync("https://unlam-vh-api.azurewebsites.net/orders", order);

            return new OkObjectResult($"¡Pedido registrado! ID={order.Id} ProductName={order.ProductName}");
        }
    }
}
