//namespace Application
//{
//    using Database.Demo;
//    using DatabaseBuiltIn.Demo;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;

//    public static class UtilCms
//    {
//        private static void HtmlText(CmsComponentBuiltIn component, List<CmsComponentBuiltIn> componentList, StringBuilder result)
//        {
//            var componentType = CmsComponentTypeBuiltInApplication.IdName(component.ComponentTypeIdName);
//            switch (componentType)
//            {
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.Page:
//                    foreach (var item in componentList.Where(item => item.ParentId == component.Id).OrderBy(item => item.Sort))
//                    {
//                        HtmlText(item, componentList, result);
//                    }
//                    break;
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.Paragraph:
//                    result.Append(string.Format("<h1>{0}<h1>", component.ParagraphTitle));
//                    result.Append(string.Format("<p>{0}</p>", component.ParagraphText));
//                    break;
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.Bullet:
//                    result.Append($"<li>{component.BulletText}</li>");
                    
//                    for (int i = componentList.IndexOf(component); i < componentList.Count; i++)
//                    {
//                        var componentNext = componentList[i];
//                        if (componentNext.CodeBlockTypeIdName ==  CmsComponentTypeBuiltInApplication.IdNameEnum.Bullet.IdName())
//                        {

//                        }
//                    }
//                    componentList.Where(item => item.ParentId == component.ParentId);
//                    break;
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.Image:
//                    break;
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.Youtube:
//                    break;
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.CodeBlock:
//                    break;
//                case CmsComponentTypeBuiltInApplication.IdNameEnum.Glossary:
//                    break;
//                default:
//                    break;
//            }

//        }

//        public static string HtmlText(CmsComponentBuiltIn component, List<CmsComponent> componentList)
//        {
//            StringBuilder result = new StringBuilder();

//            return result.ToString();
//        }
//    }
//}
