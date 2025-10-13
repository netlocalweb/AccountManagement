using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankTransactionRepository : IRepositoryBase<BankTransaction>
    {
        Task<IEnumerable<BankTransaction>> GetAllBankTransactionAsync(bool trackchanges);
        Task<BankTransaction?> GetBankTransactionsByIdAsync(int id, bool trackchanges);
        void CreateBankTransaction(BankTransaction bankTransaction);
        void DeleteBankTransaction(BankTransaction bankTransaction);
        void UpdateBankTransaction(BankTransaction bankTransaction);
    }
}
