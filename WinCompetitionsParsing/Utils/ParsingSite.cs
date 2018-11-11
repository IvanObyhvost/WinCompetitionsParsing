using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using WinCompetitionsParsing.BL.Models;

namespace WinCompetitionsParsing.Utils
{
    public class ParsingSite
    {
        private string userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
        private string language = "en-us;q=0.5,en;q=0.3";
        private HtmlParser parser;

        public ParsingSite()
        {
            parser = new HtmlParser();
        }

        public string GetHtml(string uri)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
                    webClient.Headers.Add(HttpRequestHeader.AcceptLanguage, language);
                    webClient.Encoding = Encoding.UTF8;
                    var html = webClient.DownloadString(uri);
                    return html;
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
           
        }
        public IElement FindOneElement(string html, string find)
        {
            var document = parser.Parse(html);
            return document.QuerySelector(find);
        }
        public string FindOneElement(string html, string find, string attr)
        {
            var document = parser.Parse(html);
            return document.QuerySelector(find).GetAttribute(attr);
        }
        public IEnumerable<IElement> FindElements(string html, string find)
        {
            var document = parser.Parse(html);
            return document.QuerySelectorAll(find);
        }
        public IEnumerable<string> FindElements(string html, string find, string attr)
        {
            var document = parser.Parse(html);
            return document.QuerySelectorAll(find).Select(x => x.GetAttribute(attr));
        }

        public void GetDefaultInformationAboutProduct(string html, ProductModel productModel)
        {
           

            //проверить рабочий или нет
            var price = FindOneElement(html, "span.product-item__price");
            if (price != null)
                productModel.IsWorking = true;

            //найти категорию 
            var category = FindOneElement(html, "div.bread-crumbs li:nth-child(2)");
            if (category != null)
                productModel.Category = FindOneElement(category.Html(), "span").Text();

            //найти подкатигорию 
            var subCategory = FindOneElement(html, "div.bread-crumbs li:nth-child(3)");
            if (subCategory != null)
                productModel.SubCategory = FindOneElement(subCategory.Html(), "span").Text();

            //найти имя 
            var name = FindOneElement(html, ".product-item__name");
            if (name != null)
                productModel.Name = name.Text();
        }

        
        public bool CheckFindLink(string uri, string findLink)
        {
            var html = GetHtml(uri);
            if (html == string.Empty) return false;
            var link = FindOneElement(html, "div.product-info__description a", "href");
            if (link == null) return false;
            if (link == findLink) return true;
            return false;
        }
    }
}
