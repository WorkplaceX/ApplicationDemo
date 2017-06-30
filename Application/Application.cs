namespace Application
{
    using Framework;
    using Framework.Application;
    using Framework.DataAccessLayer;
    using System;

    public class AppDemo : App
    {
        protected override void ProcessInit(ProcessList processList)
        {
            base.ProcessInit(processList);
            processList.AddBefore<ProcessGridMasterIsClick, ProcessGridIsClickFalse>();
        }

        protected override void CellValueToText(string gridName, string index, Cell cell, ref string text)
        {
            if (UtilApplication.IndexToIndexEnum(index) == IndexEnum.Index)
            {
                if (UtilFramework.TypeUnderlying(cell.TypeField) == typeof(bool))
                {
                    if ((bool?)cell.Value == false)
                    {
                        text = "No";
                    }
                    if ((bool?)cell.Value == true)
                    {
                        text = "Yes";
                    }
                }
            }
        }

        protected override void CellValueFromText(string gridName, string index, Cell cell, ref string text)
        {
            if (UtilApplication.IndexToIndexEnum(index) == IndexEnum.Index)
            {
                if (UtilFramework.TypeUnderlying(cell.TypeField) == typeof(bool))
                {
                    if (text != null)
                    {
                        if (text.ToUpper() == "YES")
                        {
                            text = "True";
                        }
                        if (text.ToUpper() == "NO")
                        {
                            text = "False";
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
