using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using API.Controllers;

namespace Desktop.Services
{
    public static class HttpService
    {
        public static HttpClient client = new HttpClient();
        public static string Token = null!;

        public static async Task<ApiResponse<D>> Request<D>(HttpMethod method, string endpoint, object? Data = null)
        {
            if (Token != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var message = new HttpRequestMessage(method, $"http://localhost:5261/api/{endpoint}");
            if (Data != null) message.Content = JsonContent.Create(Data);

            var response = await client.SendAsync(message);

            return await response.Content.ReadFromJsonAsync<ApiResponse<D>>();
        }
    }
}
