using System.ComponentModel.DataAnnotations;

namespace VismaTechnicalTask.Models
{
    public class Dept
    {
        [Key]
        public int Id { get; set; }
        public string DeptId { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string TypeId { get; set; }
    }
}
