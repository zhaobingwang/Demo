using Newtonsoft.Json;
using System;
using System.Xml;

namespace XML2JSON
{
    class XML2JSON
    {
        static void Main(string[] args)
        {
            string xml = @"<?xml version='1.0' standalone='no'?>
                            <root>
                              <person id='1'>
                              <name>Alan</name>
                              <url>http://www.google.com</url>
                              </person>
                              <person id='2'>
                              <name>Louis</name>
                              <url>http://www.yahoo.com</url>
                              </person>
                            </root>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            string json = JsonConvert.SerializeXmlNode(doc);
            Console.WriteLine(json);
        }
    }
}
