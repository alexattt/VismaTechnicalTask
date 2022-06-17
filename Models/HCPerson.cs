using System.ComponentModel.DataAnnotations;

namespace VismaTechnicalTask.Models
{
    public class HCPerson
    {
        [Key]
        public int Id { get; set; }
        public string HCPersonId { get; set; }

        public string Name { get; set; }

        public string TypeId { get; set; }
    }
}
