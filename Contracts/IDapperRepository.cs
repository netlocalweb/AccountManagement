using Entities.DTOs.ReportsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDapperRepository
    {
        Task<IEnumerable<ClientAccountReportDto>> GetClientAccountsAsync();
        Task<IEnumerable<TransactionReportDto>> GetTransactionsByAccountAsync(int accountId);
        Task<IEnumerable<ClientActiveAccountDto>> GetActiveAccountsByClientAsync(int clientId);
        Task<IEnumerable<ProductByCategoryDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
