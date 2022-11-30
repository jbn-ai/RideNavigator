using Microsoft.AspNetCore.Mvc;
using RideNavigator.Models.API;

namespace RideNavigator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {

        [HttpGet("/candidate")]
        public ActionResult<string> Candidate()
        {
            return Ok(new Candidate().Get());
        }

    }
}
