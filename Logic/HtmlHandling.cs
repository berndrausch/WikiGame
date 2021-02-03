using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace WikiGame.Logic
{    
    
    internal class HtmlHandling
    {
        private const string xpathForContent = "//body//div[@id=\"content\"]";
        private const string xpathForHeading = xpathForContent + "//h1[@id=\"firstHeading\"]";
        private const string xpathForLinks = xpathForContent + "//div[@id=\"bodyContent\"]//a";

        internal string GetFullUrl(string url)
        {
            if (url.StartsWith("http"))
            {
                return url;
            }

            if (url.StartsWith("/wiki/"))
            {
                return $"https://de.wikipedia.org{url}";
            }

            if (url.StartsWith("/"))
            {
                return $"https://de.wikipedia.org{url}";
            }

            return $"https://de.wikipedia.org/wiki/{url}";
        }

        internal string GetTitle(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode(xpathForHeading).InnerText;
        }

        internal static bool IsInternalWikiLink(string url)
        {
            if (url.StartsWith(@"/wiki/Datei:"))
            {
                return false;
            }

            return url.StartsWith(@"/");
        }

        internal IEnumerable<string> GetAllLinks(HtmlDocument doc)
        {
            foreach (var node in doc.DocumentNode.SelectNodes(xpathForLinks))
            {
                var href = node.Attributes["href"];
                if (href != null && IsInternalWikiLink(href.Value))
                {
                    yield return href.Value;
                }
            }
        }
    }
}