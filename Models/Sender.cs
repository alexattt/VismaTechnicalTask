using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VismaTechnicalTask.Models
{
    // Sender unites Sender, HCP, Inst/HCProf, Address
    public class Sender
    {
        [Key]
        // either Inst or HCProf Id
        public string Id { get; set; }
        public string Role { get; set; }
        public string MedSpeciality { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Type { get; set; }

        [ForeignKey("Dept")]
        public string DeptID { get; set; }
        public virtual Dept Dept { get; set; }

        [ForeignKey("HCPerson")]
        public string HCPersonID { get; set; }
        public virtual HCPerson HCPerson { get; set; }

        public string AdrType { get; set; }
        public string StreetAdr { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        // public string County { get; set; }
        // public string Country { get; set; }
        // public string CityDistr { get; set; }
        public string TeleAddress { get; set; }
    }
}
