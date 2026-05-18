using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace AccountManagement.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public ReportsController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet("accounts")]
        public IActionResult GetAccountsReport()
        {
            var report = from account in _context.BankAccounts
                         join client in _context.Clients
                             on account.ClientId equals client.Id
                         join currency in _context.Currencies
                             on account.CurrencyId equals currency.Id
                         select new AccountReportDTO
                         {
                             ClientCode = client.Username,
                             ClientName = client.FirstName + " " + client.LastName,
                             AccountCode = account.Code,
                             AccountName = account.Name,
                             Currency = currency.Code,
                             CurrentBalance = account.Balance
                         };

            return Ok(report.ToList());
        }

        [HttpGet("account-transactions/{accountId}")]
        public IActionResult GetTransactionsByAccount(int accountId)
        {
            var account = _context.BankAccounts
                .FirstOrDefault(a => a.Id == accountId);

            if (account == null)
                return NotFound("Bank account not found.");

            var transactions = _context.BankTransactions
                .Where(t => t.BankAccountId == accountId)
                .OrderByDescending(t => t.DateCreated)
                .Select(t => new AccountTransactionReportDTO
                {
                    Action = t.Action == 1 ? "Depozitim" : "Terheqje",
                    Amount = t.Amount,
                    Date = t.DateCreated
                })
                .ToList();

            return Ok(transactions);
        }

        [HttpGet("client-active-accounts/{clientId}")]
        public IActionResult GetActiveAccountsByClient(int clientId)
        {
            var client = _context.Clients
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
                return NotFound("Client not found.");

            var activeAccounts = from account in _context.BankAccounts
                                 join currency in _context.Currencies
                                     on account.CurrencyId equals currency.Id
                                 where account.ClientId == clientId && account.IsActive == true
                                 select new ClientActiveAccountReportDTO
                                 {
                                     AccountCode = account.Code,
                                     AccountName = account.Name,
                                     Currency = currency.Code,
                                     CurrentBalance = account.Balance
                                 };

            return Ok(activeAccounts.ToList());
        }
    }
}