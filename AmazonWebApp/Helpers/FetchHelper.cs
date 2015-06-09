using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Providers.Entities;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using AmazonWebApp.DAO;

namespace AmazonWebApp.Helpers
{
    public static class FetchHelper
    {
        public static List<Item> FetchItems(string url, int page, int lastIndexOnPage)
        {
            var list = new List<Item>();
            try
            {
                var request = HttpWebRequest.Create(url);
                var response = request.GetResponse();
                var doc = XDocument.Load(response.GetResponseStream());

                var itemName = XName.Get("Item", Config.Namespace);
                var itemNodes = doc.Descendants(itemName);
                var urlName = XName.Get("DetailPageURL", Config.Namespace);
                itemNodes = itemNodes.Where(x => x.Descendants(urlName).Any());
                var defaultOffset = 13;
                for (var i = lastIndexOnPage; i < lastIndexOnPage + defaultOffset; i++)
                {
                    if (i == itemNodes.Count())
                    {
                        break;
                    }
                    var serializer = new XmlSerializer(typeof(Item));
                    var item = (Item) serializer.Deserialize(itemNodes.ElementAt(i).CreateReader());
                    item.Page = page;
                    item.IndexOnPage = i;
                    list.Add(item);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Caught Exception: " + e.Message);
            }

            return list;
        }

        public static void LoadItems(List<Item> list, string q, int page, int lastIndexOnPage)
        {
            var helper = new SignedRequestHelper(Config.MyAwsSecretKeyId, Config.MyAwsSecretKey, Config.Destination);
            var request = new Dictionary<string, string>();
            request["Service"] = "AWSECommerceService";
            request["Version"] = "2011-08-01";
            request["Operation"] = "ItemSearch";
            request["SearchIndex"] = "Blended";
            request["AssociateTag"] = "test";
            request["ResponseGroup"] = "Medium";
            request["ItemPage"] = page.ToString();
            request["Keywords"] = q;

            var requestUrl = helper.Sign(request);
            var items = FetchItems(requestUrl, page, lastIndexOnPage);
            list.AddRange(items);
            if (list.Count < 13)
            {
                LoadItems(list, q, ++page, 0);
            }
        }

        public static T Deserialize<T>(XDocument doc)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var reader = doc.Root.CreateReader())
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }
    }
}