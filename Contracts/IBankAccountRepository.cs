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
        Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync(bool trackchanges);
        Task<BankAccount?> GetBankAccountByIdAsync(int id ,bool trackchanges);
        Task<bool>CodeExistsForClientAsync(string code, int clientId );
        void CreateBankAccount(BankAccount bankAccount);
        void UpdateBankAccount(BankAccount bankAccount);

    }
}
