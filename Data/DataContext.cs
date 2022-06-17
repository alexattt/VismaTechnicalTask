using Microsoft.EntityFrameworkCore;
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
    }
}
