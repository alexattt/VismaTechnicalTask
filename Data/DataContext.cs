using Microsoft.EntityFrameworkCore;
using VismaTechnicalTask.HelperModels;
using VismaTechnicalTask.Models;

namespace VismaTechnicalTask.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<AppRec> AppRecs { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<ErrorReason> ErrorReasons { get; set; }
        public DbSet<HelperInfo> HelperInfo { get; set; }
        public virtual DbSet<SenderStatusRatio> SenderStatusRatios { get; set; }
        public virtual DbSet<ErrorTop> ErrorTops { get; set; }
        public virtual DbSet<FailedPeriodOverview> FailedPeriodOverviews { get; set; }
    }
}
