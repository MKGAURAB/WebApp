using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebApp.Models
{
    [XmlRoot("Transaction")]
    public class XmlModel
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDate { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }
    }

    [XmlRoot("PaymentDetails")]
    public class PaymentDetails
    {
        [XmlElement("Amount")]
        public string Amount { get; set; }

        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }

    [XmlRoot("Transactions")]
    public class XmlRootModel
    {
        [XmlElement("Transaction")]
        public List<XmlModel> XmlModels { get; set; }
    }
}
