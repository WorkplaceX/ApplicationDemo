namespace Server
{
    using Application;
    using Framework.Server;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System.Threading.Tasks;

    public class WebController : WebControllerBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WebController(IMemoryCache memoryCache)
            : base(memoryCache)
        {

        }

        /// <summary>
        /// Every WebRequest comes here.
        /// </summary>
        [Route(Startup.ControllerPath + "{*uri}")]
        public async Task<IActionResult> Web()
        {
            return await UtilServer.ControllerWebRequest(this, Startup.ControllerPath, new AppSelectorDemo());
        }
    }
}
