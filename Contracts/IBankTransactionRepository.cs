using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBankTransactionRepository
    {
        IEnumerable<BankTransaction> GetBankTransactions();

        BankTransaction GetBankTransactionById(int id);

        IEnumerable<BankTransaction> GetBankTransactionsByBankAccountId(int bankAccountId);

        void CreateBankTransaction(BankTransaction bankTransaction);

        void UpdateBankTransaction(BankTransaction bankTransaction);

        void DeleteBankTransaction(BankTransaction bankTransaction);
    }
}
