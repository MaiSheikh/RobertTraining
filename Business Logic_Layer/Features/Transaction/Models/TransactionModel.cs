namespace Business_Logic_Layer.Features.Account.Models;

public class TransactionModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }

    public int Delta { get; set; }
    public DateTime TimeStamp { get; set; }
}