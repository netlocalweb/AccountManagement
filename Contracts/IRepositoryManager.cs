namespace Contracts
{
    //Interface per menaxhimin e te gjitha repository-ve
    public interface IRepositoryManager
    {
        IClientRepository Client {  get; }
        ICurrencyRepository Currency { get; }
        ICategoryRepository Category { get; }
        IProductRepository Products { get; }
        IBankAccountRepository BankAccount { get; }
        IBankTransactionRepository BankTransaction { get; }

        Task SaveAsync();  
    }
}
