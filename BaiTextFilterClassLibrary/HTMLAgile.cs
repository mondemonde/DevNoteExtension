using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaiTextFilterClassLibrary
{
    public class HTMLAgile
    {

        public HTMLAgile(string name = "index")
        {
            HTMLName = name;
        }

        public string HTMLName { get; set; }


        public string SourceFilePath
        {
            get
            {
                var baseDir = LogApplication.Agent.GetCurrentDir() + "\\RawHtml\\";//AppDomain.CurrentDomain.BaseDirectory;
                string theHtml = Path.Combine(baseDir, HTMLName + ".html").Replace("file:\\","");
                return theHtml;
            }
        }
        public List<HtmlNode> GetItemsByContainer(string selector)
        {
            var result = new List<HtmlNode>();

            HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            // filePath is a path to a file containing the html
            //htmlDoc.Load(SourceFilePath);

            var content = File.ReadAllText(SourceFilePath);
            htmlDoc.LoadHtml(content); // to load from a string (was htmlDoc.LoadXML(xmlString)


            //WEB LOAD
            //var url = @"https://www.olx.ph/ph-00-makati/real-estate";
            //var web = new HtmlWeb();
            //htmlDoc = web.Load(url);



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

                        // Do something with bodyNode
                        var nodes = htmlDoc.DocumentNode.SelectNodes(".//" + selector);
                        //HtmlNode[] aNodes = node.SelectNodes(".//a").ToArray();

                        if (nodes != null)
                        {
                            var nodeList = nodes.ToList();
                            result = nodeList;
                            foreach (HtmlNode item in nodeList)
                            {
                                LogApplication.Agent.LogWarn(item.OuterHtml);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public string GetPropertyBySeletor(string container, string selector)
        {
            string result = string.Empty;
            HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(container); // to load from a string (was htmlDoc.LoadXML(xmlString)

            // ParseErrors is an ArrayList containing any errors from the Load statement
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required
                LogApplication.Agent.LogError(htmlDoc.ParseErrors.First().Reason);
                //Console.WriteLine(htmlDoc.ParseErrors.First().Reason);
            }
            else
            {

                if (htmlDoc.DocumentNode != null)
                {
                    HtmlAgilityPack.HtmlNode elementNode = htmlDoc.DocumentNode.SelectSingleNode("//"+ selector);

                    if (elementNode != null)
                    {
                        result = elementNode.InnerHtml;
                        Console.WriteLine("-->>>>>>>>>>>>>>>>>>>>>>>" + result + "<<<<<<<<<<<<<<<<<<<<<<<<<<---");
                        LogApplication.Agent.LogWarn(elementNode.OuterHtml);
                    }
                }
            }
            return result;
        }


        public  string GetPropertyWithClass(string container, string propertyClass )
        {
            string result = string.Empty;
            HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(container); // to load from a string (was htmlDoc.LoadXML(xmlString)

            // ParseErrors is an ArrayList containing any errors from the Load statement
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required
                LogApplication.Agent.LogError(htmlDoc.ParseErrors.First().Reason);
                //Console.WriteLine(htmlDoc.ParseErrors.First().Reason);
            }
            else
            {

                if (htmlDoc.DocumentNode != null)
                {
                    //HtmlAgilityPack.HtmlNode elementNode = htmlDoc.DocumentNode.SelectSingleNode("//" + selector);

                    var elementNodes = GetNodesWithClass(htmlDoc.DocumentNode, propertyClass);

                    if(elementNodes.Count()>0)
                    {
                        var elementNode = elementNodes.First();
                        result = elementNode.InnerHtml;
                        Console.WriteLine("-->>>>>>>>>>>>>>>>>>>>>>>" + result + "<<<<<<<<<<<<<<<<<<<<<<<<<<---");
                        LogApplication.Agent.LogWarn(elementNode.OuterHtml);
                    }

                    
                }
            }
            return result;
        }

        private  IEnumerable<HtmlNode> GetNodesWithClass(HtmlNode node, String className)
        {

            return node
                .Descendants()
                //.Where(n => n.NodeType == NodeType.Element)
                .Where(e =>
                 //  e.Name == "div" &&
                   CheapClassListContains(
                       e.GetAttributeValue("class", ""),
                       className,
                       StringComparison.Ordinal
                   )
                );
        }

        public    List<string> GetHrefsOfNode(HtmlNode node)
        {

          var nodes =  node
                .Descendants("a")
                //.Where(n => n.NodeType == NodeType.Element)
                .Where(e =>
                 e.Attributes["href"] != null                  
                );

            List<string> result = new List<string>();
            foreach(var n in nodes)
            {
                result.Add(n.Attributes["href"].Value);
            }
            return result;
          
        }

        /// <summary>Performs optionally-whitespace-padded string search without new string allocations.</summary>
        /// <remarks>A regex might also work, but constructing a new regex every time this method is called would be expensive.</remarks>
        private  Boolean CheapClassListContains(String haystack, String needle, StringComparison comparison)
        {
            if (String.Equals(haystack, needle, comparison)) return true;
            Int32 idx = 0;
            while (idx + needle.Length <= haystack.Length)
            {
                idx = haystack.IndexOf(needle, idx, comparison);
                if (idx == -1) return false;

                Int32 end = idx + needle.Length;

                // Needle must be enclosed in whitespace or be at the start/end of string
                Boolean validStart = idx == 0 || Char.IsWhiteSpace(haystack[idx - 1]);
                Boolean validEnd = end == haystack.Length || Char.IsWhiteSpace(haystack[end]);
                if (validStart && validEnd) return true;

                idx++;
            }
            return false;
        }

        #region FIND TEXT
        public FindItem FindTextBySelector(string selector,List<string> listOfText,string content)
        {
            FindItem newFind = new FindItem();

            //HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"my_control_id\"]");
            //string value = (node == null) ? "Error, id not found" : node.InnerHtml;

            HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            // filePath is a path to a file containing the html
            //htmlDoc.Load(SourceFilePath);
            try
            {
                //var content = File.ReadAllText(SourceFilePath);
                //content = WebUtility.HtmlDecode(content);
                File.WriteAllText(SourceFilePath, content);
                //htmlDoc.DetectEncodingAndLoad(SourceFilePath);

                htmlDoc.Load(SourceFilePath, Encoding.UTF8);//, Encoding.UTF8)

                // ParseErrors is an ArrayList containing any errors from the Load statement
                if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
                {
                    // Handle any parse errors as required
                    //LogApplication.Agent.LogError(htmlDoc.ParseErrors.First().Reason);
                    Console.WriteLine(htmlDoc.ParseErrors.First().Reason);
                }
                else if (listOfText != null && listOfText.Count > 0)
                {

                    if (htmlDoc.DocumentNode != null)
                    {
                        HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                        if (bodyNode != null)
                        {
                            var nodes = htmlDoc.DocumentNode.SelectNodes(selector);
                            var nodes1 = htmlDoc.DocumentNode.SelectNodes(selector + "//*");

                            var nodeList = new List<HtmlNode>();

                            if (nodes1 != null && nodes != null)
                            {
                                var n1 = nodes1.ToList();
                                var n2 = nodes.ToList();
                                nodeList.AddRange(n1);
                                nodeList.AddRange(n2);
                            }
                            else
                            {
                                nodeList = nodes.ToList();
                            }


                            foreach (HtmlNode item in nodeList)
                            {
                                //LogApplication.Agent.LogWarn(item.OuterHtml);
                                var node = item;//.SelectSingleNode("text()[normalize-space()]");
                                if (node == null || string.IsNullOrEmpty(node.InnerHtml))
                                    continue;

                                var line1 = node.InnerText.Trim();
                                var line2 = line1.ToLower();
                                Console.WriteLine(line1);
                                newFind.ListFound.Add(line1);

                                if (!newFind.IsFound)
                                {
                                    foreach (string find in listOfText)
                                    {
                                        if (line2.IndexOf(find.ToLower()) > -1)
                                            newFind.IsFound = true;
                                    }
                                }


                            }

                        }//end body
                    }
                }
                else
                {
                    if (htmlDoc.DocumentNode != null)
                    {
                        HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                        if (bodyNode != null)
                        {
                            // Do something with bodyNode
                            //var nodes = htmlDoc.DocumentNode.SelectNodes(".//" + selector);
                            var nodes = htmlDoc.DocumentNode.SelectNodes(selector);
                            var nodes1 = htmlDoc.DocumentNode.SelectNodes(selector + "//*");

                            var nodeList = new List<HtmlNode>();

                            if (nodes1!=null && nodes !=null)
                            {
                                var n1 = nodes1.ToList();
                                var n2 = nodes.ToList();
                                nodeList.AddRange(n1);
                                nodeList.AddRange(n2);
                            }
                            else
                            {
                                nodeList = nodes.ToList();
                            }
                                

                            //HtmlNode[] aNodes = node.SelectNodes(".//a").ToArray();
                               //nodeList = nodes.ToList();
                                foreach (HtmlNode item in nodeList)
                                {
                                    //LogApplication.Agent.LogWarn(item.OuterHtml);
                                    var node = item;//.SelectSingleNode("text()[normalize-space()]");
                                    if (node == null || string.IsNullOrEmpty(node.InnerHtml))
                                        continue;

                                    var line = node.InnerText.Trim();
                                    Console.WriteLine(line);
                                    newFind.ListFound.Add(line);

                                }
                           
                        }//end body
                    }

                }

            }
            catch (Exception err)
            {
                LogApplication.Agent.LogError(err.Message);

            }   

            return newFind;
        }


        #endregion

    }
}
