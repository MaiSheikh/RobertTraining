using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RobertTraining.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal? Balance { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public Guid? ReferenceId { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
