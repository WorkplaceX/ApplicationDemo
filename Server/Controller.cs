namespace Server
{
    using Application;
    using Database.dbo;
    using Framework;
    using Framework.Application.Config;
    using Framework.DataAccessLayer;
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
            UtilFramework.UnitTest(typeof(AppDemo), () => {
                UtilDataAccessLayer.Insert(new FrameworkApplicationView() { Type = UtilFramework.TypeToName(typeof(AppDemo)), IsActive = true, IsExist = true });
                UtilDataAccessLayer.Insert(new FrameworkApplicationView() { Type = UtilFramework.TypeToName(typeof(AppConfig)), IsActive = true, IsExist = true, Path = "config" });
            });
            return await UtilServer.ControllerWebRequest(this, Startup.ControllerPath, new AppSelectorDemo());
        }
    }
}
