using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nfo.Net
{
    public static class UrlParser
    {
        public static string GetMediaId(Uri uri)
        {
            string id = String.Empty;
            var domainParser = new DomainParser(new WebTldRuleProvider());
            var domainName = domainParser.Get(uri.Host);

            if (domainName.Domain == "imdb")
            {
                if (uri.Segments.Last().ToLowerInvariant().StartsWith("tt"))
                {
                    id = uri.Segments.Last().ToLowerInvariant();
                }                
            }
            else if (domainName.Domain == "themoviedb" && uri.Segments.Any())
            {
                var lastSegmentSplit = uri.Segments.Last().Split('-'); // "210577-gone-girl"
                if (lastSegmentSplit.Any() && int.TryParse(lastSegmentSplit.First(), out int idS))
                {
                    id = idS.ToString();
                }
            }


            if (!String.IsNullOrWhiteSpace(id))
            {
                if (id.EndsWith("/"))
                {
                    id = id.Substring(0, id.Length - 1);
                }
            }

            return id;
        }
    }
}
