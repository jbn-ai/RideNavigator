using System.Net.Http.Headers;
using System.Text.Json;

namespace RideNavigator.Models.API
{

    public class Location
    {
        public class JsonResponse
        {
            public string? city { get; set; }

            //below can be uncommented for a more detailed response
            //public string? region { get; set; }
            //public string? country { get; set; }
            //public string? loc { get; set; }
            //public string? org { get; set; }
            //public string? timezone { get; set; }
            //public string? ip { get; set; }
        }

        public async Task<string>? Get(string ip)
        {

            if (ip.Contains("localhost")) { ip = "123.123.123.123"; }

            var url = $"https://ipinfo.io/";

            var token = Environment.GetEnvironmentVariable("IPINFO_KEY");

            var parameters = $"{ip}?token={token}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(parameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                JsonResponse? jsonList = JsonSerializer.Deserialize<JsonResponse>(jsonString);

                return JsonSerializer.Serialize(jsonList);
            }
            else
            {
                return null!;
            }            

        }

    }
}
