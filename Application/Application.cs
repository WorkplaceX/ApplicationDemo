namespace Application
{
    using Framework.Application;
    using System;

    public class AppDemo : App
    {
        protected override void ProcessInit(ProcessList processList)
        {
            base.ProcessInit(processList);
            processList.AddBefore<ProcessGridMasterIsClick, ProcessGridIsClickFalse>();
        }

        protected override Type TypePageMain()
        {
            return typeof(PageMain);
        }
    }
}
