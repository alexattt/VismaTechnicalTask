using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VismaTechnicalTask.Models
{
    public class ErrorReason
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AppRec")]
        public string AppRecID { get; set; }
        public virtual AppRec AppRec { get; set; }

        public string Err_S { get; set; }
        public string Err_V { get; set; }
        public string Err_DN { get; set; }
        public string Err_OT { get; set; }
    }
}
