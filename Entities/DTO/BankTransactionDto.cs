using Entities.Enums;

namespace Entities.DTO
{
    public class BankTransactionDto
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string BankAccountCode { get; set; }
        public TransactionAction Action { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
