using System;
using System.Xml.Serialization;

namespace HomeTask1
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://library.by/catalog")]
    public class Book
    {
        private string isbn;
        private string author;
        private string title;
        private Genre genre;
        private string publisher;
        private DateTime publishDate;
        private string description;
        private DateTime registrationDate;
        private string id;

        [XmlElement("isbn")]
        public string Isbn
        {
            get => isbn;
            set => isbn = value;
        }

        [XmlElement("author")]
        public string Author
        {
            get => author;
            set => author = value;
        }

        [XmlElement("title")]
        public string Title
        {
            get => title;
            set => title = value;
        }

        [XmlElement("genre")]
        public Genre Genre
        {
            get => genre;
            set => genre = value;
        }

        [XmlElement("publisher")]
        public string Publisher
        {
            get => publisher;
            set => publisher = value;
        }

        [XmlElement("publish_date", DataType = "date")]
        public DateTime PublishDate
        {
            get => publishDate;
            set => publishDate = value;
        }

        [XmlElement("description")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        [XmlElement("registration_date", DataType = "date")]
        public DateTime RegistrationDate
        {
            get => registrationDate;
            set => registrationDate = value;
        }

        [XmlAttribute("id")]
        public string Id
        {
            get => id;
            set => id = value;
        }
    }

    public enum Genre
    {
        [XmlEnum(Name = "Computer")]
        Computer,
        [XmlEnum(Name = "Fantasy")]
        Fantasy,
        [XmlEnum(Name = "Romance")]
        Romance,
        [XmlEnum(Name = "Horror")]
        Horror,
        [XmlEnum(Name = "Science Fiction")]
        ScienceFiction
    };
}
