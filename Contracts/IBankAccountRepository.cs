using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBankAccountRepository
    {
        IEnumerable<BankAccount> GetBankAccounts();

        BankAccount GetBankAccountById(int id);

        IEnumerable<BankAccount> GetBankAccountsByClientId(int clientId);

        BankAccount GetBankAccountByCodeAndClientId(string code, int clientId);

        void CreateBankAccount(BankAccount bankAccount);

        void UpdateBankAccount(BankAccount bankAccount);

        void DeleteBankAccount(BankAccount bankAccount);
    }
}