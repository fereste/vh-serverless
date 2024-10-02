using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace VhFunction
{
    public class Order
    {
        public int? Id { get; set; }
    }

    public static class TakeOrder
    {
        [FunctionName("TakeOrder")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req, TraceWriter log)
        {
            Order data = await req.Content.ReadAsAsync<Order>();

            if (data == null || data.Id == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid order");
            }

            return req.CreateResponse(HttpStatusCode.OK, $"Orden tomada, ID: {data.Id}");
        }
    }
}
