namespace Application
{
    using Database.Demo;
    using DatabaseBuiltIn.Demo;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class UtilCms
    {
        public static string ToHtmlText(List<CmsComponent> cmsComponentList)
        {
            StringBuilder result = new StringBuilder();

            var cmsComponentTypeId = CmsComponentTypeBuiltInApplication.IdNameEnum.Page.Row().Id;
            foreach (var item in CmsComponentTypeBuiltInApplication.RowList.Where(item => item.Id == cmsComponentTypeId))
            {

            }

            return result.ToString();
        }
    }
}
