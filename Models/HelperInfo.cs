using System;
using System.ComponentModel.DataAnnotations;

namespace VismaTechnicalTask.Models
{
    public class HelperInfo
    {
        [Key]
        public int Id { get; set; }
        public DateTime LastAddedXmlDate { get; set; }
    }
}
