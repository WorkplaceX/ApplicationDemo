namespace Server
{
    using Application;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class WebController : Controller
    {
        [Route(Startup.RoutePath + "{*uri}")]
        public async Task<IActionResult> Web()
        {
            return await Framework.Server.Util.ControllerWebRequest(this, Startup.RoutePath, new Application());
        }
    }
}
