namespace Application
{
    using Framework.Application;
    using Framework.DataAccessLayer;
    using System;

    /// <summary>
    /// AppSelector has to be in same assembly like App classes.
    /// </summary>
    public class AppSelectorDemo : AppSelector
    {

    }

    public class AppDemo : App
    {
        protected override void CellRowValueToText(GridName gridName, Index index, Cell cell, ref string result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                if (cell.TypeColumn == typeof(bool) || cell.TypeColumn == typeof(bool?))
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

        protected override void CellRowValueFromText(GridName gridName, Index index, Cell cell, ref string result)
        {
            if (index.Enum == IndexEnum.Index)
            {
                if (cell.TypeColumn == typeof(bool) || cell.TypeColumn == typeof(bool?))
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
