
using System.Net.Http.Headers;
using System.Text.Json;

namespace RideNavigator.Models.API
{
    public class Listing
    {
        public class JsonResponse
        {
            public List<Listings>? listings { get; set; }

        }
        public class Listings
        {
            public string? name { get; set; }
            public float? pricePerPassenger { get; set; }
            public float? totalCost { get; set; }
            public VehicleType? vehicleType { get; set; }
        }
        public class VehicleType
        {
            public string? name { get; set; }
            public float? maxPassengers { get; set; }

        }

        public async Task<string>? Get(int passengers)
        {

            var url = $"https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                JsonResponse? jsonList = JsonSerializer.Deserialize<JsonResponse>(jsonString)!;

                foreach (var item in jsonList.listings!)
                {
                    item.totalCost = (float?)Math.Round((decimal)(item.pricePerPassenger * passengers)!, 2);
                }

                var sortedJsonList =
                    from item in jsonList.listings
                    where item.vehicleType!.maxPassengers >= passengers
                    orderby item.totalCost descending
                    select item;

                return JsonSerializer.Serialize(sortedJsonList);
            }
            else
            {
                return null!;
            }

        }

    }
}
