using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VismaTechnicalTask.HelperModels
{
    [Keyless]
    public class SenderStatusRatio
    {
        [Column("Status")]
        public String Status { get; set; }
        [Column("StatusCount")]
        public int StatusCount { get; set; }
    }
}
