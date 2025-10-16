using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
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
