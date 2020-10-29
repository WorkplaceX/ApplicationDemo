namespace Application
{
    using Database.Demo;
    using DatabaseIntegrate.Demo;
    using Framework.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using System.Web;

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
            var componentType = CmsComponentTypeIntegrateApp.IdName(component.ComponentTypeIdName);

            // Ul close
            if (componentType != CmsComponentTypeIntegrateApp.IdNameEnum.Bullet && isUl)
            {
                result.Append("</ul>");
                isUl = false;
            }

            // Render
            switch (componentType)
            {
                // Page
                case CmsComponentTypeIntegrateApp.IdNameEnum.Page:
                    foreach (var item in componentList.Where(item => item.ParentId == component.Id).OrderBy(item => item.Sort))
                    {
                        if (item.ComponentTypeIdName == CmsComponentTypeIntegrateApp.IdNameEnum.Page.IdName())
                        {
                            // Render sub page as card.
                            result.Append($"<a class='navigatePost' href='{ UtilCms.PathCmsPage(item.PagePath) }'>");
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
                case CmsComponentTypeIntegrateApp.IdNameEnum.Paragraph:
                    if (component.ParagraphTitle != null)
                    {
                        result.Append($"<h1>{ HtmlText(component.ParagraphTitle) }</h1>");
                    }
                    if (component.ParagraphText != null)
                    {
                        result.Append($"<p>{ HtmlText(component.ParagraphText) }</p>");
                    }
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Bullet:
                    if (!isUl)
                    {
                        result.Append("<ul>");
                        isUl = true;
                    }
                    result.Append($"<li>{ HtmlText(component.BulletText) }</li>");
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Image:
                     result.Append($"<img src='{ UtilCms.PathCmsFile(component.ImageFileName) }'>");
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Youtube:
                    result.Append(string.Format("<iframe width='560' height='315' src='{0}' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>", component.YoutubeLink));
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.CodeBlock:
                    result.Append(string.Format("<pre><code class='{0}'>{1}</code></pre>", component.CodeBlockTypeText, component.CodeBlockText));
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Glossary:
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

        private static void TextMdParameter(TextMdArgs args, string parameterName, string parameterValue)
        {
            if (parameterValue != null)
            {
                args.Result.Append($" { parameterName }={ parameterValue }");
            }
        }

        private static void TextMdNewLine(TextMdArgs args)
        {
            if (args.IsFirst)
            {
                args.IsFirst = false;
            }
            else
            {
                args.Result.AppendLine();
            }
        }

        private class TextMdArgs
        {
            public CmsComponentDisplay Component;

            public List<CmsComponentDisplay> ComponentList;

            public CmsComponentDisplay ComponentPrevious;

            public bool IsFirst;

            public StringBuilder Result;
        }

        private static void TextMd(TextMdArgs args)
        {
            var componentType = CmsComponentTypeIntegrateApp.IdName(args.Component.ComponentTypeIdName);
            var componentPreviousType = CmsComponentTypeIntegrateApp.IdName(args.ComponentPrevious?.ComponentTypeIdName);
            switch (componentType)
            {
                case CmsComponentTypeIntegrateApp.IdNameEnum.None:
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Page:
                    TextMdNewLine(args);
                    args.Result.Append("(Page");
                    TextMdParameter(args, "Path", args.Component.PagePath);
                    TextMdParameter(args, "Date", args.Component.PageDate?.ToString("yyyy-MM-dd"));
                    TextMdParameter(args, "ImageFileName", args.Component.PageImageFileName);
                    args.Result.AppendLine(")");
                    TextMdArgs argsLocal = new TextMdArgs { ComponentList = args.ComponentList, IsFirst = args.IsFirst, Result = args.Result };
                    foreach (var item in args.ComponentList.Where(item => item.ParentId == args.Component.Id).OrderBy(item => item.Sort))
                    {
                        argsLocal.Component = item;
                        TextMd(argsLocal);
                        argsLocal.ComponentPrevious = item;
                    }
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Paragraph:
                    TextMdNewLine(args);
                    if (args.Component.ParagraphTitle != null)
                    {
                        args.Result.AppendLine("# " + args.Component.ParagraphTitle);
                    }
                    if (args.Component.ParagraphText != null)
                    {
                        args.Result.AppendLine(args.Component.ParagraphText);
                    }
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Bullet:
                    if (componentPreviousType != CmsComponentTypeIntegrateApp.IdNameEnum.Bullet)
                    {
                        TextMdNewLine(args);
                    }
                    args.Result.AppendLine("* " + args.Component.BulletText);
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Image:
                    TextMdNewLine(args);
                    args.Result.AppendLine($"![{ args.Component.ImageText }]({ args.Component.ImageFileName })");
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Youtube:
                    TextMdNewLine(args);
                    args.Result.AppendLine("(Youtube)");
                    if (args.Component.YoutubeLink != null)
                    {
                        args.Result.AppendLine(args.Component.YoutubeLink);
                    }
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.CodeBlock:
                    TextMdNewLine(args);
                    args.Result.AppendLine("```" + args.Component.CodeBlockTypeText);
                    args.Result.AppendLine(args.Component.CodeBlockText);
                    args.Result.AppendLine("```");
                    break;
                case CmsComponentTypeIntegrateApp.IdNameEnum.Glossary:
                    break;
                default:
                    break;
            }
        }

        public static string TextMd(CmsComponentDisplay component, List<CmsComponentDisplay> componentList)
        {
            TextMdArgs args = new TextMdArgs { Component = component, ComponentList = componentList, IsFirst = true, Result = new StringBuilder() };
            TextMd(args);
            return args.Result.ToString();
        }
    }
}
