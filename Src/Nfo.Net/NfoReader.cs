using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nager.PublicSuffix;
using Nfo.Net.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Nfo.Net
{
    public static class NfoReader
    {
        public static MediaMetadata Read(String content, ILogger logger = null)
        {
            logger = logger ?? NullLogger.Instance;
            content = content.Trim('\r', '\n');

            if (!content.StartsWith("<"))
            {
                return ParseUrls(content, logger);
            }
            else
            {
                if (content.EndsWith(">"))
                {
                    ParseXml(content, logger);
                }
                else
                {
                    //this should be a mixed document
                }
            }            

            return null;
        }

        private static void ParseXml(string content, ILogger logger)
        {
            //should be xml
            try
            {
                XDocument xDocument = XDocument.Parse(content);
            }
            catch (System.Xml.XmlException ex)
            {
                logger.LogError(ex, "xml error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "unkown error");
            }
        }

        private static MediaMetadata ParseUrls(string content, ILogger logger)
        {
            MediaMetadata metadata = new MediaMetadata();

            //this should be a url nfo, so just check for url links
            string[] lines = content.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                    );

            foreach (var line in lines)
            {
                try
                {
                    UriBuilder uriBuilder = new UriBuilder(line);
                    
                    var domainParser = new DomainParser(new WebTldRuleProvider());
                    var domainName = domainParser.Get(uriBuilder.Host);

                    Identifier id = new Identifier();
                    id.Type = domainName.Domain;
                    id.Url = uriBuilder.Uri.AbsoluteUri;
                    id.Id = UrlParser.GetMediaId(uriBuilder.Uri);

                    metadata.Identifiers.Add(id);
                }
                catch (UriFormatException)
                {
                    logger.LogError($"Line not url: {line}");
                }
            }

            return metadata;
        }
    }
}
