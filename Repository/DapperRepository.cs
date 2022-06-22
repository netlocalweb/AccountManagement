using Contracts;
using Dapper;
using Entities;
using Entities.DTO;
using System.Collections.Generic;

namespace Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DapperContext _context;

        public DapperRepository(DapperContext context)
        {
            _context = context;
        }

        //First CustomAPI
        public IEnumerable<CustomFirstApiDTO> FirstAPI()
        {
            var query = "Select Clients.FirstName,Clients.LastName,BankAccounts.Code, BankAccounts.Name,Currencies.Description, BankAccounts.Balance From Clients, BankAccounts, Currencies Where BankAccounts.ClientId = Clients.Id AND BankAccounts.CurrencyId = Currencies.Id";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<CustomFirstApiDTO>(query);
                return result;
            }
        }
        //Second CustomAPI
        public IEnumerable<CustomSecondApiDTO> SecondApi(int id)
        {

            var query = "Select BankTransactions.Action, BankTransactions.Amount, BankTransactions.DateCreated From BankTransactions, BankAccounts Where BankTransactions.BankAccountId = BankAccounts.Id AND BankAccounts.Id = @Id Order By BankTransactions.DateCreated ASC";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<CustomSecondApiDTO>(query, new { id });
                return result;
            }
        }
        //Third CustomAPI
        public IEnumerable<CustomThirdApiDTO> ThirdApi(int id)
        {
            var query = "Select BankAccounts.Code, BankAccounts.Name, Currencies.Description, BankAccounts.Balance From Clients, BankAccounts, Currencies Where BankAccounts.ClientId = Clients.Id AND BankAccounts.CurrencyId = Currencies.Id AND BankAccounts.IsActive = 1 AND Clients.Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<CustomThirdApiDTO>(query, new { id });
                return result;
            }
        }
        //Fourth CustomAPI
        public IEnumerable<CustomFourthApiDTO> FourthAPI(int id)
        {
            var query = "Select Products.Name, Products.Price, Products.DateCreated From Categories, Products Where Products.CategoryId = Categories.Id AND Categories.Id = @Id Order By Products.DateCreated ASC";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<CustomFourthApiDTO>(query, new { id });
                return result;
            }
        }

    }
}
