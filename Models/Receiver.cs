using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VismaTechnicalTask.Models
{
    // Receiver unites Receiver, HCP, Inst/HCProf, Address
    public class Receiver
    {
        [Key]
        public int Id { get; set; }

        // either Inst or HCProf Id
        public string ReceiverId { get; set; }
        public string Role { get; set; }
        public string MedSpeciality { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Type { get; set; }
        public string DeptId { get; set; }
        public string DeptType { get; set; }
        public string DeptName { get; set; }
        public string DeptTypeId { get; set; }
        public string HCPersonId { get; set; }
        public string HCPersonName { get; set; }
        public string HCPersonTypeId { get; set; }
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
