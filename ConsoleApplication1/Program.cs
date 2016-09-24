using System;
using System.Collections.Generic;
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
      
        private static async Task<OffersData> GetRequest(string url)
        {
            var fromWeb = await Task.Run(() => XElement.Load(url));

            var offersRoot = fromWeb.Descendants().FirstOrDefault(d => d.Name.LocalName.Equals("offers"));
            return (OffersData)new XmlSerializer(typeof(OffersData)).Deserialize(offersRoot.CreateReader());
        }

        private static async void SendRequest(OffersData offersData, string url)
        {
            var offer = offersData.Offers
                .FirstOrDefault(x => x.Id == 12344);

            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(offer.GetType());
            serializer.Serialize(stringwriter, offer);

            var doc = new XmlDocument();
            doc.LoadXml(stringwriter.ToString());

            await new WebClient().UploadStringTaskAsync(new Uri(url), "POST", JsonConvert.SerializeXmlNode(doc));
        }
    }

    [XmlRoot("offers")]
    public class OffersData
    {
        public OffersData()
        {
            Offers = new List<Offer>();
        }

        [XmlElement("offer")]
        public List<Offer> Offers { get; set; }
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

    public class CategoryId
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlText]
        public int id;
    }
}
