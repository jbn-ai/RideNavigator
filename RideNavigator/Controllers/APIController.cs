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


        [HttpGet("/location")]
        public async Task<ActionResult<string>> Location()
        {
            var task = await new Location().Get(Request.Headers["Host"].ToString())!;

            if (task == null)
            {
                return NoContent();
            }

            return Ok(task);
        }

    }
}
