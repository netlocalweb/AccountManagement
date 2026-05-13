using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITestRepository TestRepository { get; }

        IClientRepository ClientRepository { get; }

        ICurrencyRepository CurrencyRepository { get; }

        IBankAccountRepository BankAccountRepository { get; }

        IBankTransactionRepository BankTransactionRepository { get; }

        Task SaveAsync();
    }
}