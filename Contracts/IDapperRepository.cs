using Entities.DTO.Reports;

namespace Contracts
{
    public interface IDapperRepository
    {
        //Merr llogarite e klienteve
        Task<IEnumerable<ClientAccountDto>> GetClientAccountAsync();
        //Merr transaksionet te nje llogarie
        Task<IEnumerable<AccountTransactionDto>> GetAccountTransactionsAsync(int accountId);
        //Merr llogarite aktive per nje klient
        Task<IEnumerable<ClientAccountsDto>> GetClientActiveAccountsAsync(int clientId);
        //Merr prd sipas nje katerie 
        Task<IEnumerable<CategoryProductsDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
