namespace Application
{
    using Framework;
    using Framework.Application;
    using Framework.DataAccessLayer;
    using System;

    public class AppSelectorDemo : AppSelector
    {

    }

    public class AppDemo : App
    {
        protected override void CellValueToText(string gridName, Index index, Cell cell, ref string result)
        {
            if (index.Enum == IndexEnum.Index)
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

        protected override void CellValueFromText(string gridName, Index index, Cell cell, ref string result)
        {
            if (index.Enum == IndexEnum.Index)
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
            return typeof(PageGridDatabaseBrowse);
        }
    }
}
