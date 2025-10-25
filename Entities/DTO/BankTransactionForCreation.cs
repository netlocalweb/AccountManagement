using Entities.Enums;

namespace Entities.DTO
{
    public class BankTransactionForCreation
    {
        public int BankAccountId { get; set; }
        public TransactionAction Action { get; set; }
        public decimal Amount { get; set; }
    }
}
