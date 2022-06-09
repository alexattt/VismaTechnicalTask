using System.ComponentModel.DataAnnotations;

namespace VismaTechnicalTask.Models
{
    public class HCPerson
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string TypeId { get; set; }
    }
}
