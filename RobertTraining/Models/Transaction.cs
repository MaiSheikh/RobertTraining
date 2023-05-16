using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RobertTraining.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public int Delta { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
