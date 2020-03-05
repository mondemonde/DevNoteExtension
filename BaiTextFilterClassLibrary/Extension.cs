using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTextFilterClassLibrary
{

   public static class Extension
    {
        public static bool HasClass(this HtmlNode node, params string[] classValueArray)
        {
            var classValue = node.GetAttributeValue("class", "");
            var classValues = classValue.Split(' ');
            return classValueArray.All(c => classValues.Contains(c));
        }
    }
}
