using Microsoft.EntityFrameworkCore;
using RobertTraining.Models;

namespace RobertTraining.Data
{
    public class ContextDb :DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options ) :base(options)
        {
            // this.Configuration.LazyLoadingEnabled = false;

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        
    }
}
