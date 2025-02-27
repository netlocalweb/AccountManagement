
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IBankAccountRepository : IRepositoryBase<BankAccount>
    {
        List<BankAccount> FindByClientId(int id);
        void SoftDelete(BankAccount entity);
    }
}
