using Contracts;
using Dapper;
using Entities;
using Entities.DTO.Reports;

namespace Repository
{
    public class ReportsRepository : IDapperRepository
    {
        private readonly DapperContext _dapperContext;

        public ReportsRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        //Marr transaksionet e nje llogarie bankare
        public async Task<IEnumerable<AccountTransactionDto>> GetAccountTransactionsAsync(int accountId)
        {
            var query = @"
            SELECT
                CASE 
                WHEN t.Action = 1 THEN 'Depozitim'
                WHEN t.Action = 2 THEN 'Terheqje'
               END AS ActionName,
                t.Amount,
                t.DateCreated AS Date
            FROM BankTransaction t 
            WHERE t.BankAccountId = @accountId
            ORDER BY t.DateCreated DESC;";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<AccountTransactionDto>(query ,new {accountId});
            }

        }
        //Merr llogarite e klienteve dhe detajet perkatese 
        public async Task<IEnumerable<ClientAccountDto>> GetClientAccountAsync()
        {
            var query = @"
                SELECT 
                    c.UserId AS ClientCode,
                    c.FirstName + '' + c.LastName As ClientName,
                    a.Code AS AccountCode,
                    a.Name AS AccountName,
                    cu.Code AS Currency,
                    a.Balance AS CurrentBalance
                    FROM Clients c
                    INNER JOIN BankAccounts a ON c.Id = a.ClientId
                    INNER JOIN Currencies cu ON a.CurrencyId = cu.Id;
            ";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<ClientAccountDto>(query);
            }

        }
        //Merr llogarite aktive te nje klienti 
        public async Task<IEnumerable<ClientAccountsDto>> GetClientActiveAccountsAsync(int clientId)
        {
            var query = @"
                SELECT
                    a.Code AS AccountCode,
                    a.Name AS AccountName,
                    cu.Code AS Currency,
                    a.Balance AS CurrentBalance
                FROM BankAccounts a 
                INNER JOIN Currencies cu ON a.CurrencyId = cu.Id
                WHERE a.ClientId = @clientId
                AND a.IsActive = 1;
            ";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<ClientAccountsDto>(query , new {clientId});
            }

        }
        //Merr produktet sipas nje kategorie
        public async Task<IEnumerable<CategoryProductsDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var query = @"
                SELECT 
                    p.Name As ProductName,
                    p.ShortDescription,
                    p.Price,
                    p.DateCreated
                FROM Products p
                WHERE p.CategoryId = @categoryId
                ORDER BY p.DateCreated DESC;
            ";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<CategoryProductsDto>(query,new {categoryId});
            }
        }
    }
}
