using System;
using System.Xml.Serialization;

namespace AmazonWebApp.DAO
{
    [XmlRoot(ElementName = "Item", Namespace = "http://webservices.amazon.com/AWSECommerceService/2011-08-01")]
    public class Item
    {
        public string DetailPageURL { get; set; }
        public MediumImage MediumImage { get; set; }
        public ItemAttributes ItemAttributes { get; set; }
        public int Page { get; set; }
        public int IndexOnPage { get; set; }
        public OfferSummary OfferSummary { get; set; }

    }

    public class MediumImage
    {
        public string URL { get; set; }
    }

    public class ItemAttributes
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Department { get; set; }
        public Price ListPrice { get; set; }
    }

    public class OfferSummary
    {
        public Price LowestNewPrice { get; set; }
    }

    public class Price
    {
        public string FormattedPrice { get; set; }
    }
}