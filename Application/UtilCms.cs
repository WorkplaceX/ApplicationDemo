namespace Application
{
    using Database.Demo;
    using DatabaseIntegrate.Demo;
    using Framework.Util;
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

        private static string ComponentToHtmlText(string text)
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

        private static void ComponentToHtmlText(CmsComponentDisplay component, List<CmsComponentDisplay> componentList, ref bool isUl, StringBuilder result)
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
                            result.Append($"<p class='card-text'>{ ComponentToHtmlText(item.PageTitle) }</p>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append($"</a>");
                        }
                        else
                        {
                            ComponentToHtmlText(item, componentList, ref isUl, result);
                        }
                    }
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Paragraph:
                    if (component.ParagraphTitle != null)
                    {
                        result.Append($"<h1>{ ComponentToHtmlText(component.ParagraphTitle) }</h1>");
                    }
                    if (component.ParagraphText != null)
                    {
                        result.Append($"<p>{ ComponentToHtmlText(component.ParagraphText) }</p>");
                    }
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Bullet:
                    if (!isUl)
                    {
                        result.Append("<ul>");
                        isUl = true;
                    }
                    result.Append($"<li>{ ComponentToHtmlText(component.BulletText) }</li>");
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Image:
                     result.Append($"<img src='{ UtilCms.PathCmsFile(component.ImageFileName) }'>");
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Youtube:
                    result.Append(string.Format("<iframe width='560' height='315' src='{0}' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>", component.YoutubeLink));
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

        public static string ComponentToTextHtml(CmsComponentDisplay component, List<CmsComponentDisplay> componentList)
        {
            StringBuilder result = new StringBuilder();
            bool isUl = false;
            ComponentToHtmlText(component, componentList, ref isUl, result);
            
            // Ul close
            if (isUl)
            {
                result.Append("</ul>");
                isUl = false;
            }

            return result.ToString();
        }

        private static void ComponentToTextMdParameter(TextMdArgs args, string parameterName, string parameterValue)
        {
            if (parameterValue != null)
            {
                args.Result.Append($" { parameterName }={ parameterValue }");
            }
        }

        private static void ComponentToTextMdNewLine(TextMdArgs args)
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

        private static void ComponentToTextMd(TextMdArgs args)
        {
            var componentType = CmsComponentTypeIntegrateApplication.IdName(args.Component.ComponentTypeIdName);
            var componentPreviousType = CmsComponentTypeIntegrateApplication.IdName(args.ComponentPrevious?.ComponentTypeIdName);
            switch (componentType)
            {
                case CmsComponentTypeIntegrateApplication.IdNameEnum.None:
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Page:
                    ComponentToTextMdNewLine(args);
                    args.Result.Append("(Page");
                    ComponentToTextMdParameter(args, "Path", args.Component.PagePath);
                    ComponentToTextMdParameter(args, "Date", args.Component.PageDate?.ToString("yyyy-MM-dd"));
                    ComponentToTextMdParameter(args, "ImageFileName", args.Component.PageImageFileName);
                    args.Result.AppendLine(")");
                    TextMdArgs argsLocal = new TextMdArgs { ComponentList = args.ComponentList, IsFirst = args.IsFirst, Result = args.Result };
                    foreach (var item in args.ComponentList.Where(item => item.ParentId == args.Component.Id).OrderBy(item => item.Sort))
                    {
                        argsLocal.Component = item;
                        ComponentToTextMd(argsLocal);
                        argsLocal.ComponentPrevious = item;
                    }
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Paragraph:
                    ComponentToTextMdNewLine(args);
                    if (args.Component.ParagraphTitle != null)
                    {
                        args.Result.AppendLine("# " + args.Component.ParagraphTitle);
                    }
                    if (args.Component.ParagraphText != null)
                    {
                        args.Result.AppendLine(args.Component.ParagraphText);
                    }
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Bullet:
                    if (componentPreviousType != CmsComponentTypeIntegrateApplication.IdNameEnum.Bullet)
                    {
                        ComponentToTextMdNewLine(args);
                    }
                    args.Result.AppendLine("* " + args.Component.BulletText);
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Image:
                    ComponentToTextMdNewLine(args);
                    args.Result.AppendLine($"![{ args.Component.ImageText }]({ args.Component.ImageFileName })");
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Youtube:
                    ComponentToTextMdNewLine(args);
                    args.Result.AppendLine("(Youtube)");
                    if (args.Component.YoutubeLink != null)
                    {
                        args.Result.AppendLine(args.Component.YoutubeLink);
                    }
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.CodeBlock:
                    ComponentToTextMdNewLine(args);
                    args.Result.AppendLine("```" + args.Component.CodeBlockTypeText);
                    args.Result.AppendLine(args.Component.CodeBlockText);
                    args.Result.AppendLine("```");
                    break;
                case CmsComponentTypeIntegrateApplication.IdNameEnum.Glossary:
                    break;
                default:
                    break;
            }
        }

        public static string ComponentToTextMd(CmsComponentDisplay component, List<CmsComponentDisplay> componentList)
        {
            TextMdArgs args = new TextMdArgs { Component = component, ComponentList = componentList, IsFirst = true, Result = new StringBuilder() };
            ComponentToTextMd(args);
            return args.Result.ToString();
        }

        public static List<CmsComponentDisplay> ComponentFromTextMd(string textMd)
        {
            new ComponentFromTextMdDocument(textMd);
            return null;

        }

        private class ComponentFromTextMdDocument : TextParseDocument
        {
            public ComponentFromTextMdDocument(string text) 
                : base(text)
            {

            }
        }

        private class ComponentFromTextMdParagraph : TextParseComponent
        {
            public ComponentFromTextMdParagraph(TextParseComponent owner) 
                : base(owner)
            {

            }

            protected override bool Parse()
            {
                return base.Parse();
            }
        }

        private class ComponentFromTextMdBullet : TextParseComponent
        {
            public ComponentFromTextMdBullet(TextParseComponent owner) 
                : base(owner)
            {

            }
        }

        private class ComponentFromTextMdCodeBlock : TextParseComponent
        {
            public ComponentFromTextMdCodeBlock(TextParseComponent owner)
                : base(owner)
            {

            }
        }

        private class ComponentFromTextMdLink : TextParseComponent
        {
            public ComponentFromTextMdLink(TextParseComponent owner)
                : base(owner)
            {

            }
        }

        private class ComponentFromTextMdBold : TextParseComponent
        {
            public ComponentFromTextMdBold(TextParseComponent owner)
                : base(owner)
            {

            }
        }

    }
}
