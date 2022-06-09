using System.ComponentModel.DataAnnotations;

namespace VismaTechnicalTask.Models
{
    public class Dept
    {
        [Key]
        public string Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string TypeId { get; set; }
    }
}
