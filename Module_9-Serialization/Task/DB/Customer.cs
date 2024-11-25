namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            CustomerDemographics = new HashSet<CustomerDemographic>();
        }

        [DataMember]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [DataMember]
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string ContactName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string ContactTitle { get; set; }

        [DataMember]
        [StringLength(60)]
        public string Address { get; set; }

        [DataMember]
        [StringLength(15)]
        public string City { get; set; }

        [DataMember]
        [StringLength(15)]
        public string Region { get; set; }

        [DataMember]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [DataMember]
        [StringLength(15)]
        public string Country { get; set; }

        [DataMember]
        [StringLength(24)]
        public string Phone { get; set; }

        [DataMember]
        [StringLength(24)]
        public string Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }
}
