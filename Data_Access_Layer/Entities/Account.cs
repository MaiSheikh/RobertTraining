
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public decimal? Balance { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public Guid? ReferenceId { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
