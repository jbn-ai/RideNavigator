using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RideNavigator.Models.API
{

    public class Candidate
    {       
        public string Get()
        {

            var data = new
            {
                name = "test",
                phone = "test"
            };

            return JsonSerializer.Serialize(data);

        }

    }

}
