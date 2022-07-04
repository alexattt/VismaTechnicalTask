using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VismaTechnicalTask.HelperModels
{

    [Keyless]
    public class ErrorTop
    {
        [Column("Err_DN")]
        public String ErrorReason { get; set; }
        [Column("ErrorCount")]
        public int ErrorCount { get; set; }
    }
}
