using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public int Delta { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
