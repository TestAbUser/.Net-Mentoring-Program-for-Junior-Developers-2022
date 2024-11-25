namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Employee
    {
        public Employee()
        {
            Employees1 = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }

        [DataMember]
        public int EmployeeID { get; set; }

        [DataMember]
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? HireDate { get; set; }

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
        public string HomePhone { get; set; }

        [DataMember]
        [StringLength(4)]
        public string Extension { get; set; }

        [DataMember]
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [DataMember]
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [DataMember]
        public int? ReportsTo { get; set; }

        [DataMember]
        [StringLength(255)]
        public string PhotoPath { get; set; }

        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
