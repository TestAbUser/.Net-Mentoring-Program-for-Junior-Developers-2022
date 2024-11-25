using System;
using System.Xml.Serialization;

namespace HomeTask1
{
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://library.by/catalog")]
    [XmlRoot("catalog", Namespace = "http://library.by/catalog", IsNullable = false)]
    public class Catalog
    {
        private Book[] book;
        private DateTime date;

        [XmlElement("book")]
        public Book[] Book
        {
            get => book;
            set => book = value;
        }

        [XmlAttribute("date", DataType = "date")]
        public DateTime Date
        {
            get => date;
            set => date = value;
        }
    }
}
