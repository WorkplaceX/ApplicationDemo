namespace Application
{
    using Framework.Application;
    using System;

    /// <summary>
    /// AppSelector has to be in same assembly like App classes.
    /// </summary>
    public class AppSelectorDemo : AppSelector
    {

    }

    public class AppDemo : App
    {
        protected override Type TypePageMain()
        {
            return typeof(PageGridDatabaseBrowse);
        }
    }
}
