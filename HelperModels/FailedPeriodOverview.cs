using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VismaTechnicalTask.HelperModels
{

    [Keyless]
    public class FailedPeriodOverview
    {
        [Column("PeriodStart")]
        public DateTime PeriodStart { get; set; }
        [Column("PeriodEnd")]
        public DateTime PeriodEnd { get; set; }
        [Column("Status")]
        public String Status { get; set; }
        [Column("PeriodLength")]
        public int PeriodLength { get; set; }
    }
}
