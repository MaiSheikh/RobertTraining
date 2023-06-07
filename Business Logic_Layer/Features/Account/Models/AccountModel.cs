namespace Business_Logic_Layer.Features.Account.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public decimal? Balance { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public Guid? ReferenceId { get; set; }
    }
}
