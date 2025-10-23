using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankAccountRepository : IRepositoryBase<BankAccount>
    {
        Task<IEnumerable<BankAccount>> GetAllBAnkAccountsAsync(bool trackchanges);
        Task<BankAccount?> GetBankAccountsByIdAsync(int id ,bool trackchanges);
        Task<bool>CodeExistsForClientAsync(string code, int clientId );
        void CreateBankAccount(BankAccount bankAccount);
        void UpdateBankAccount(BankAccount bankAccount);

    }
}
