﻿namespace Application
{
    using Database.Demo;
    using DatabaseIntegrate.Demo;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class UtilCms
    {
        private static string Path(string pathPrefix, string path)
        {
            if (path?.StartsWith("/") == true)
            {
                path = path.Substring("/".Length);
            }
            return pathPrefix + path;
        }

        public static string PathCmsPage(string path)
        {
            return Path("/cms/", path);
        }

        public static string PathCmsPage()
        {
            return PathCmsPage(null);
        }

        public static string PathCmsFile(string path)
        {
            return Path("/cmsfile/", path);
        }

        public static string PathCmsFile()
        {
            return PathCmsFile(null);
        }

        private static string HtmlText(string text)
        {
            if (text == null)
            {
                return null;
            }

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
                else
                {
                    if (indexBracketStart == null)
                    {
                        result.Append(c);
                    }
                }
            }
            var resultText = result.ToString();
            return resultText;
        }

        private static void HtmlText(CmsComponentDisplay component, List<CmsComponentDisplay> componentList, ref bool isUl, StringBuilder result)
        {
            var componentType = CmsComponentTypeIntegrateApplication.IdName(component.ComponentTypeIdName);

            // Ul close
            if (componentType != CmsComponentTypeIntegrateApplication.IdNameEnum.Bullet && isUl)
            {
                result.Append("</ul>");
                isUl = false;
            }

            // Render
            switch (componentType)
            {
                // Page
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Page:
                    foreach (var item in componentList.Where(item => item.ParentId == component.Id).OrderBy(item => item.Sort))
                    {
                        if (item.ComponentTypeIdName == CmsComponentTypeIntegrateApplication.IdNameEnum.Page.IdName())
                        {
                            // Render sub page as card.
                            result.Append($"<a class='linkPost' href='{ UtilCms.PathCmsPage(item.PagePath) }'>");
                            result.Append("<div class='card'>");
                            if (item.PageImageFileName != null)
                            {
                                result.Append($"<img src='{ UtilCms.PathCmsFile(item.PageImageFileName) }' class='card-img-top'>");
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
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Paragraph:
                    if (component.ParagraphTitle != null)
                    {
                        result.Append($"<h1>{ HtmlText(component.ParagraphTitle) }</h1>");
                    }
                    if (component.ParagraphText != null)
                    {
                        result.Append($"<p>{ HtmlText(component.ParagraphText) }</p>");
                    }
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Bullet:
                    if (!isUl)
                    {
                        result.Append("<ul>");
                        isUl = true;
                    }
                    result.Append($"<li>{ HtmlText(component.BulletText) }</li>");
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Image:
                     result.Append($"<img src='{ UtilCms.PathCmsFile(component.ImageFileName) }'>");
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Youtube:
                    result.Append(string.Format("Youtube<iframe width='560' height='315' src='{0}' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>", component.YoutubeLink));
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.CodeBlock:
                    result.Append(string.Format("<pre><code class='{0}'>{1}</code></pre>", component.CodeBlockTypeText, component.CodeBlockText));
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Glossary:
                    break;
                default:
                    break;
            }
        }

        public static string TextHtml(CmsComponentDisplay component, List<CmsComponentDisplay> componentList)
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
