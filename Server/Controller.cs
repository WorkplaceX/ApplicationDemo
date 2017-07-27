namespace Server
{
    using Application;
    using Framework.Server;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class WebController : Controller
    {
        [Route(Startup.ControllerPath + "{*uri}")]
        public async Task<IActionResult> Web()
        {
            return await UtilServer.ControllerWebRequest(this, Startup.ControllerPath, new AppSelectorDemo());
        }
    }
}
