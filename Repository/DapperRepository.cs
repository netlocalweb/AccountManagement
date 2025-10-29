using Contracts;
using Dapper;
using Entities.DTOs.ReportsDto;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConfiguration config;

        public DapperRepository(IConfiguration config)
        {
            this.config = config;
        }

        private IDbConnection Connection => new SqlConnection(config.GetConnectionString("DefaultConnection"));

        public async Task<IEnumerable<ClientAccountReportDto>> GetClientAccountsAsync()
        {
            var query = @"
                SELECT 
                  CAST(c.Id AS NVARCHAR(50)) AS ClientCode,
            (c.FirstName + ' ' + c.LastName) AS ClientName,
            a.Code AS AccountCode,
            a.Name AS AccountName,
            cur.Code AS Currency,
            a.Balance AS CurrentBalance
        FROM Clients c
        JOIN BankAccounts a ON c.Id = a.ClientId
        JOIN Currencies cur ON a.CurrencyId = cur.Id";

           
                using var conn = Connection;
                return await conn.QueryAsync<ClientAccountReportDto>(query);
            }
          

        public async Task<IEnumerable<TransactionReportDto>> GetTransactionsByAccountAsync(int accountId)
        {
            var query = @"
                SELECT 
                    CASE t.Action
                        WHEN 1 THEN 'Deposit'
                        WHEN 2 THEN 'Withdraw'
                    END AS Action,
                    t.Amount,
                    t.DateCreated AS Date
                FROM BankTransactions t
                WHERE t.BankAccountId = @accountId
                ORDER BY t.DateCreated DESC";

            using var conn = Connection;
            return await conn.QueryAsync<TransactionReportDto>(query, new { accountId });
        }

        public async Task<IEnumerable<ClientActiveAccountDto>> GetActiveAccountsByClientAsync(int clientId)
        {
            var query = @"
                SELECT 
                    a.Code AS AccountCode,
                    a.Name AS AccountName,
                    cur.Code AS Currency,
                    a.Balance AS CurrentBalance
                FROM BankAccounts a
                JOIN Currencies cur ON a.CurrencyId = cur.Id
                WHERE a.ClientId = @clientId AND a.IsActive = 1";

            using var conn = Connection;
            return await conn.QueryAsync<ClientActiveAccountDto>(query, new { clientId });
        }

        public async Task<IEnumerable<ProductByCategoryDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var query = @"
                SELECT 
                    CAST(p.Id AS NVARCHAR(50)) AS ProductCode,
                    p.Name AS ProductName,
                    p.Price,
                    p.DateCreated
                FROM Products p
                WHERE p.CategoryId = @categoryId
                ORDER BY p.DateCreated DESC";

            using var conn = Connection;
            return await conn.QueryAsync<ProductByCategoryDto>(query, new { categoryId });
        }
    }
}
