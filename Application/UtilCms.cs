namespace Application
{
    using Database.Demo;
    using DatabaseBuiltIn.Demo;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class UtilCms
    {
        public static readonly string FolderNameCms = "cms/";

        private static string HtmlText(string text)
        {
            var result = new StringBuilder();

            int? indexBracketStart = null;
            int? indexBracketStop = null;
            int? indexParentheseStart = null;
            int? indexParentheseStop = null;
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c == '[' && indexBracketStart == null)
                {
                    indexBracketStart = i;
                }
                if (c == ']' && indexBracketStart != null)
                {
                    indexBracketStop = i;
                }
                if (c =='(' && indexBracketStop == i - 1)
                {
                    indexParentheseStart = i;
                }
                if (c == ')' && indexParentheseStart != null)
                {
                    indexParentheseStop = i;
                    var linkText = text.Substring(indexBracketStart.Value + 1, (indexBracketStop.Value - indexBracketStart.Value) - 1);
                    var link = text.Substring(indexParentheseStart.Value + 1, (indexParentheseStop.Value - indexParentheseStart.Value) - 1);
                    result.Append($"<a href='{ link }'>{ linkText }</a>");
                    indexBracketStart = null;
                    indexBracketStop = null;
                    indexParentheseStart = null;
                    indexParentheseStop = null;
                    index = i + 1;
                }
            }
            result.Append(text.Substring(index));
            var resultText = result.ToString();
            return resultText;
        }

        private static void HtmlText(CmsComponentDisplay component, List<CmsComponentDisplay> componentList, ref bool isUl, StringBuilder result)
        {
            var componentType = CmsComponentTypeBuiltInApplication.IdName(component.ComponentTypeIdName);

            // Ul close
            if (componentType != CmsComponentTypeBuiltInApplication.IdNameEnum.Bullet && isUl)
            {
                result.Append("</ul>");
                isUl = false;
            }

            // Render
            switch (componentType)
            {
                // Page
                case CmsComponentTypeBuiltInApplication.IdNameEnum.Page:
                    foreach (var item in componentList.Where(item => item.ParentId == component.Id).OrderBy(item => item.Sort))
                    {
                        if (item.ComponentTypeIdName == CmsComponentTypeBuiltInApplication.IdNameEnum.Page.IdName())
                        {
                            // Render sub page as card.
                            result.Append($"<a class='linkPost' href='{ FolderNameCms + item.PageFileName }'>");
                            result.Append("<div class='card'>");
                            if (item.PageImageFileName != null)
                            {
                                result.Append($"<img src='{ FolderNameCms + item.PageImageFileName}' class='card-img-top'>");
                            }
                            result.Append("<div class='card-body'>");
                            result.Append($"<p class='card-text'>{ HtmlText(item.PageTitle) }</p>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append($"</a>");
                        }
                        else
                        {
                            HtmlText(item, componentList, ref isUl, result);
                        }
                    }
                    break;
                case CmsComponentTypeBuiltInApplication.IdNameEnum.Paragraph:
                    result.Append($"<h1>{HtmlText(component.ParagraphTitle)}</h1>");
                    result.Append($"<p>{HtmlText(component.ParagraphText)}</p>");
                    break;
                case CmsComponentTypeBuiltInApplication.IdNameEnum.Bullet:
                    if (!isUl)
                    {
                        result.Append("<ul>");
                        isUl = true;
                    }
                    result.Append($"<li>{ HtmlText(component.BulletText) }</li>");
                    break;
                case CmsComponentTypeBuiltInApplication.IdNameEnum.Image:
                    break;
                case CmsComponentTypeBuiltInApplication.IdNameEnum.Youtube:
                    break;
                case CmsComponentTypeBuiltInApplication.IdNameEnum.CodeBlock:
                    break;
                case CmsComponentTypeBuiltInApplication.IdNameEnum.Glossary:
                    break;
                default:
                    break;
            }
        }

        public static string HtmlText(CmsComponentDisplay component, List<CmsComponentDisplay> componentList)
        {
            StringBuilder result = new StringBuilder();
            bool isUl = false;
            HtmlText(component, componentList, ref isUl, result);
            
            // Ul close
            if (isUl)
            {
                result.Append("</ul>");
                isUl = false;
            }

            return result.ToString();
        }
    }
}
