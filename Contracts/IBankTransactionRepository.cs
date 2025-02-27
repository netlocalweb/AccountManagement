
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IBankTransactionRepository : IRepositoryBase<BankTransaction>
    {
        List<BankTransaction> FindByBankAccountId(int id);
    }
}
