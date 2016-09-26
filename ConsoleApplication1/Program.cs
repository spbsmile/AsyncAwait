using System;
using System.Collections.Generic;
using System.Dynamic;
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
      
        private static async Task<Shop> GetRequest(string url)
        {
            var fromWeb = await Task.Run(() => XElement.Load(url));
            var shopRoot = fromWeb.Descendants().FirstOrDefault(d => d.Name.LocalName.Equals("shop"));

            return (Shop)new XmlSerializer(typeof(Shop)).Deserialize(shopRoot.CreateReader());
        }

        private static async void SendRequest(Shop shop, string url)
        {
            var offer = shop.Offers.FirstOrDefault(x => x.Id == 12347); 

            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(offer.GetType());
            serializer.Serialize(stringwriter, offer);

            var doc = new XmlDocument();
            doc.LoadXml(stringwriter.ToString());

            await new WebClient().UploadStringTaskAsync(new Uri(url), "POST", JsonConvert.SerializeXmlNode(doc));
        }
    }

    [XmlRoot("shop")]
    public class Shop
    {
        [XmlArray("offers")]
        [XmlArrayItem("offer")]
        public Offer[] Offers { get; set; }
    }

    // todo view dynamic variant 
    //public class DynamicXml : DynamicObject
    //{
    //    XElement _root;
    //    private DynamicXml(XElement root)
    //    {
    //        _root = root;
    //    }

    //    public static DynamicXml Parse(string xmlString)
    //    {
    //        return new DynamicXml(XDocument.Parse(xmlString).Root);
    //    }

    //    public static DynamicXml Load(string filename)
    //    {
    //        return new DynamicXml(XDocument.Load(filename).Root);
    //    }

    //    public override bool TryGetMember(GetMemberBinder binder, out object result)
    //    {
    //        result = null;

    //        var att = _root.Attribute(binder.Name);
    //        if (att != null)
    //        {
    //            result = att.Value;
    //            return true;
    //        }

    //        var nodes = _root.Elements(binder.Name);
    //        if (nodes.Count() > 1)
    //        {
    //            result = nodes.Select(n => new DynamicXml(n)).ToList();
    //            return true;
    //        }

    //        var node = _root.Element(binder.Name);
    //        if (node != null)
    //        {
    //            if (node.HasElements)
    //            {
    //                result = new DynamicXml(node);
    //            }
    //            else
    //            {
    //                result = node.Value;
    //            }
    //            return true;
    //        }

    //        return true;
    //    }
    //}

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


        [XmlElement("author")]
        public string Author { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("publisher")]
        public string Publisher { get; set; }
        [XmlElement("series")]
        public string Series { get; set; }
        [XmlElement("year")]
        public string Year { get; set; }
        [XmlElement("ISBN")]
        public string Isbn { get; set; }
        [XmlElement("volume")]
        public string Volume { get; set; }
        [XmlElement("part")]
        public string Part { get; set; }

        [XmlElement("binding")]
        public string Binding { get; set; }
        [XmlElement("page_extent")]
        public string PageExtent { get; set; }
        [XmlElement("downloadable")]
        public string Downloadable { get; set; }
        [XmlElement("performed_by")]
        public string PerformedBy { get; set; }
        [XmlElement("performance_type")]
        public string PerformanceType { get; set; }
        [XmlElement("storage")]
        public string Storage { get; set; }
        [XmlElement("recording_length")]
        public string RecordingLength { get; set; }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("media")]
        public string Media { get; set; }
        [XmlElement("starring")]
        public string Starring { get; set; }
        [XmlElement("director")]
        public string Director { get; set; }
        [XmlElement("originalName")]
        public string OriginalName { get; set; }
        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("worldRegion")]
        public string WorldRegion { get; set; }
        [XmlElement("region")]
        public string Region { get; set; }
        [XmlElement("days")]
        public string Days { get; set; }
        [XmlElement("dataTour")]
        public string DataTour { get; set; }

        [XmlElement("hotel_stars")]
        public string HotelStars { get; set; }

        [XmlElement("room")]
        public string Room { get; set; }

        [XmlElement("meal")]
        public string Meal { get; set; }
        [XmlElement("included")]
        public string Included { get; set; }
        [XmlElement("transport")]
        public string Transport { get; set; }
        [XmlElement("hall_part")]
        public string HallPart { get; set; }
        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("is_premiere")]
        public string IsPremiere { get; set; }
        [XmlElement("is_kids")]
        public string IsKids { get; set; }

        [XmlElement("artist")]
        public string Artist { get; set; }

        [XmlElement("format")]
        public string Format { get; set; }

        [XmlElement("categoryId")]
        public CategoryId CategoryId { get; set; }
        [XmlElement("hall")]
        public Hall Hall { get; set; }
    }

    public class CategoryId
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlText]
        public int id;
    }

    public class Hall
    {
        [XmlAttribute("plan")]
        public string Plan{ get; set; }
        [XmlText]
        public string name;
    }
}
