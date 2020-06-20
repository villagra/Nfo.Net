using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nfo.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Nfo.Net
{
    public class NfoReader
    {
        public IMedia Read(String content, ILogger logger = null)
        {
            logger = logger ?? NullLogger.Instance;
            content = content.Trim('\r', '\n');

            if (!content.StartsWith("<"))
            {
                //this should be a url nfo 
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

        private void ParseXml(string content, ILogger logger = null)
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
    }
}
