namespace Application
{
    using Framework;
    using Framework.Application;
    using Framework.DataAccessLayer;
    using System;
    using Database.dbo;
    using System.Collections.Generic;
    using Framework.Application.Setup;

    public class AppSelectorDemo : AppSelector
    {
        public override IEnumerable<FrameworkApplication> ApplicationList()
        {
            List<FrameworkApplication> result = new List<FrameworkApplication>();
            result.Add(new FrameworkApplication() { Name = "Setup", Path = "setup", Type = UtilFramework.TypeToName(typeof(AppSetup)) });
            result.Add(new FrameworkApplication() { Name = "Demo", Path = null, Type = UtilFramework.TypeToName(typeof(AppDemo)) });
            return result;
        }
    }

    public class AppDemo : App
    {
        protected override void ProcessInit(ProcessList processList)
        {
            base.ProcessInit(processList);
            processList.AddBefore<ProcessGridMasterIsClick, ProcessGridIsClickFalse>();
        }

        protected override void CellValueToText(string gridName, string index, Cell cell, ref string result)
        {
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Index)
            {
                if (UtilFramework.TypeUnderlying(cell.TypeField) == typeof(bool))
                {
                    if ((bool?)cell.Value == false)
                    {
                        result = "No";
                    }
                    if ((bool?)cell.Value == true)
                    {
                        result = "Yes";
                    }
                }
            }
        }

        protected override void CellValueFromText(string gridName, string index, Cell cell, ref string result)
        {
            if (UtilApplication.IndexEnumFromText(index) == IndexEnum.Index)
            {
                if (UtilFramework.TypeUnderlying(cell.TypeField) == typeof(bool))
                {
                    if (result != null)
                    {
                        if (result.ToUpper() == "YES")
                        {
                            result = "True";
                        }
                        if (result.ToUpper() == "NO")
                        {
                            result = "False";
                        }
                    }
                }
            }
        }

        protected override Type TypePageMain()
        {
            return typeof(PageMain);
        }
    }
}
