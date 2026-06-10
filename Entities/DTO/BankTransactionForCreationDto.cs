namespace Entities.DTO
{
    public class BankTransactionForCreationDto
    {
        public int BankAccountId { get; set; }
        public int Action { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
