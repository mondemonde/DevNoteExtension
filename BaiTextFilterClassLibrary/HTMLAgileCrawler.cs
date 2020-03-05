using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTextFilterClassLibrary
{
  public  class HTMLAgileCrawler
    {
        public HTMLAgileCrawler(string name)
        {
            HTMLName = name;
        }

        public string BaseURL  { get; set; }

        public string HTMLName { get; set; }

        public int NestLevel { get; set; }

        int _tempLevel;

        public string SourceFilePath
        {
            get
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                return baseDir + HTMLName + ".html";

            }
        }

        HashSet<string> _visitedLinks;
        public HashSet<string> VisitedLinks
        {
            get { return _visitedLinks; }
        }

        public void AddToVisitedLinks(string url)
        {
            _visitedLinks.Add(url);
        }


        public List<string> ParsedLinks { get; set; }

        public  List<string> ParseLinks(string urlToCrawl)
        {

            #region GOTO page



            #endregion

            HashSet<string> list = new HashSet<string>();

            HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            // filePath is a path to a file containing the html
            //htmlDoc.Load(SourceFilePath);

            var content = File.ReadAllText(SourceFilePath);
            htmlDoc.LoadHtml(content); // to load from a string (was htmlDoc.LoadXML(xmlString)

            // ParseErrors is an ArrayList containing any errors from the Load statement
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required
                //LogApplication.Agent.LogError(htmlDoc.ParseErrors.First().Reason);
                Console.WriteLine(htmlDoc.ParseErrors.First().Reason);
            }
            else
            {
                if (htmlDoc.DocumentNode != null)
                {
                    HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");
                    if (bodyNode != null)
                    {
                        var doc = htmlDoc;
                        //doc.LoadHtml(download);
                        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@href]");

                        if (nodes.Count > 0)
                        {                          
                            _tempLevel += 1;
                            if (_tempLevel > NestLevel)
                                NestLevel = _tempLevel;

                        }
                        else
                        {
                            _tempLevel = 0;
                        }


                            foreach (var n in nodes)
                            {
                                string href = n.Attributes["href"].Value;
                                list.Add(GetAbsoluteUrlString(href));

                                //make if recursive
                                ParseLinks(href);

                            }
                       
                    }
                }
            }
            ParsedLinks = list.ToList();
            ParsedLinks.Sort();
            return ParsedLinks;
        }

        public string  GetAbsoluteUrlString( string url)
        {
            if (string.IsNullOrEmpty(BaseURL))
                throw new ApplicationException("BaseURL not supplied.");

            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(BaseURL), uri);
            return uri.ToString();
        }

    }
}
