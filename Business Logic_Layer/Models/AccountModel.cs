using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public decimal? Balance { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public Guid? ReferenceId { get; set; }
    }
}
