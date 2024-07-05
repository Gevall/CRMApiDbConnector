using System.Net;
using System.Text.Json;
using System.Text;
using WebApiDbConnector.Models;

namespace WebApiDbConnector.Services
{
    public class SendTrip
    {
        private static IHttpClientFactory _httpClientFactory;

        public SendTrip(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public static async Task<HttpStatusCode> SendTripForEmploye(int employeId, SentTrip trip)
        {
            var httpClient = _httpClientFactory.CreateClient("SendTripToEmploye");
            var responseJson = httpClient.PostAsync(
                                    requestUri: @"http://localhost:5000/asigntrip",
                                    content: new StringContent(JsonSerializer.Serialize(trip),
                                    Encoding.UTF8,
                                    mediaType: "application/json"))
                                    .Result;
            return HttpStatusCode.OK;
        }

    }
}
