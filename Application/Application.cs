namespace Application
{
    using Framework.Server.Application;
    using System;

    public class Application : ApplicationBase
    {
        protected override Type TypePageMain()
        {
            return typeof(PageMain);
        }
    }
}
