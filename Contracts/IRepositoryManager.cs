namespace Contracts
{
    public interface IRepositoryManager
    {
        IClientsRepository ClientsRepository { get; }  

        ICurrencyRepository CurrencyRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        IBankAccountRepository BankAccountRepository { get; }

        IBankTransactionRepository BankTransactionRepository { get; }

       
    }
}