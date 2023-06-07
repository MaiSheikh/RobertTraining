using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Entities;


namespace Data_Access_Layer.Data
{
    public class ContextDb :DbContext
    {
        public ContextDb()
        {
            
        }
        public ContextDb(DbContextOptions<ContextDb> options ) :base(options)
        {
            // this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

              //  optionsBuilder.UseSqlServer("server=SELNK1350100323;database=RobertTrainingDb1; Trusted_Connection=True");
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
