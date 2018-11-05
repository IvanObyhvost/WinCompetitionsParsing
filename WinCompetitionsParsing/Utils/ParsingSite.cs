using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private HtmlParser parser;

        public ParsingSite()
        {
            parser = new HtmlParser();
        }

        public string GetHtml(string uri)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
                return webClient.DownloadString(uri);
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

        public ProductModel GetDefaultInformationAboutProduct(string html)
        {
            var product = new ProductModel();
            //проверить рабочий или нет
            var price = FindOneElement(html, "span.product-item__price");
            if (price != null)
                product.IsWorking = true;

            //найти категорию 
            var category = FindOneElement(html, "div.bread-crumbs li:nth-child(2)");
            if (category != null)
                product.Category = FindOneElement(category.Html(), "span").Text();

            //найти подкатигорию 
            var subCategory = FindOneElement(html, "div.bread-crumbs li:nth-child(3)");
            if (subCategory != null)
                product.Category = FindOneElement(subCategory.Html(), "span").Text();

            return product;
        }

        public bool CheckNotFindPageProduct(string html)
        {
            var product = FindOneElement(html, "div.page_not_found");
            if (product != null)
                return true;
            return false;
        }
        public void CheckFindLink(string findLink)
        {
            
        }
    }
}
