using Entities.DTO;
using Entities.DTO.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDapperRepository
    {
        Task<IEnumerable<ClientAccountDto>> GetClientAccountAsync();
        Task<IEnumerable<AccountTransactionDto>> GetAccountTransactionsAsync(int accountId);
        Task<IEnumerable<ClientAccountsDto>> GetClientActiveAccountsAsync(int clientId);
        Task<IEnumerable<CategoryProductsDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
