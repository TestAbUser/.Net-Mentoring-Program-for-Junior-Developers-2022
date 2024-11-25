namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [DataMember]
        public int ShipperID { get; set; }

        [DataMember]
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [DataMember]
        [StringLength(24)]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
