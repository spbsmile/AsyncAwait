using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            var fromWeb = GetRequest("https://yandex.st/market-export/1.0-17/partner/help/YML.xml").Result;

            SendRequest(fromWeb, "http://oreilly.com");

            Console.ReadKey();
        }
      
        private static async Task<XElement> GetRequest(string url)
        {
            var fromWeb = await Task.Run(() => XElement.Load(url));
            new XmlSerializer(typeof (UmlCatalog)).Deserialize(fromWeb.CreateReader());
            return fromWeb;
        }

        private static async void SendRequest(XElement data, string url)
        {
            var result = data.Descendants("offer")
                .FirstOrDefault(el => el.Attribute("id") != null &&
                                      el.Attribute("id").Value == "12344");

            var doc = new XmlDocument();
            doc.LoadXml(result.ToString());

            await new WebClient().UploadStringTaskAsync(new Uri(url), "POST", JsonConvert.SerializeXmlNode(doc));
        }
    }

    [XmlRoot("yml_catalog")]
    public class UmlCatalog
    {
        [XmlElement("shop")]
        public Shop Shop { get; set; }
    }

    public class Shop
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("company")]
        public string Company { get; set; }
        [XmlElement("url")]
        public string Url { get; set; }

        [XmlArray("currencies")]
        [XmlArrayItem("currency")]
        public Currency[] Currencies { get; set; }
        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public Category[] Categories { get; set; }
        [XmlElement("local_delivery_cost")]
        public string LocalDileveryCost { get; set; }
        [XmlArray("offers")]
        [XmlArrayItem("offer")]
        public Offer[] Offers { get; set; }
    }

    public class Currency
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("rate")]
        public string Rate { get; set; }
        [XmlAttribute("plus")]
        public string Plus { get; set; }
    }

    public class Category
    {
        [XmlAttribute("id")]//, DefaultValue("")
        public int Id { get; set; }
        [XmlText]
        public string Name { get; set; }
        [XmlAttribute("parentId")]
        public int ParentId { get; set; }
    }

    public class CategoryId
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlText] public int id;
    }

    public class Offer
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlAttribute("bid")]
        public int Bid { get; set; }
        [XmlAttribute("cbid")]
        public int Cbid { get; set; }
        [XmlAttribute("available")]
        public bool Available { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
        [XmlElement("price")]
        public int Price { get; set; }
        [XmlElement("currencyId")]
        public string CurrencyId { get; set; }

        [XmlElement("categoryId")]
        public CategoryId CategoryId { get; set; }


        [XmlElement("picture")]
        public string Picture { get; set; }

        [XmlElement("delivery")]
        public bool Delivery { get; set; }
        [XmlElement("local_delivery_cost")]
        public int LocalDeliveryCost { get; set; }
        [XmlElement("typePrefix")]
        public string TypePrefix { get; set; }
        [XmlElement("vendor")]
        public string Vendor { get; set; }
        [XmlElement("vendorCode")]
        public string VendorCode { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("manufacturer_warranty")]
        public bool ManufacturerWarranty { get; set; }
        [XmlElement("country_of_origin")]
        public string CountryOfOrigin { get; set; }
    }
}
