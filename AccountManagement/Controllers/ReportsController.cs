using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public ReportsController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet("all-accounts")]
        public async Task<IActionResult> GetAllAccountsReport()
        {
            var data = await _repositoryContext.BankAccounts
                .AsNoTracking()
                .Include(a => a.Currency)
                .Join(_repositoryContext.Clients, 
                    a => a.ClientId, 
                    c => c.Id, 
                    (a, c) => new ReportRowDto
                    {
                        ClientCode = c.Username, // using Username as Client Code
                        ClientName = c.FirstName + " " + c.LastName,
                        AccountCode = a.Code,
                        AccountName = a.Name,
                        Currency = a.Currency != null ? a.Currency.Code : string.Empty,
                        CurrentBalance = a.Balance
                    })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("account-transactions/{accountId}")]
        public async Task<IActionResult> GetAccountTransactionsReport(int accountId)
        {
            var data = await _repositoryContext.BankTransactions
                .AsNoTracking()
                .Where(t => t.BankAccountId == accountId && t.IsActive)
                .OrderBy(t => t.DateCreated)
                .Select(t => new ReportTransactionRowDto
                {
                    Action = t.Action == 1 ? "Depozitim" : (t.Action == 2 ? "Terheqje" : "Tjeter"),
                    Amount = t.Amount,
                    Date = t.DateCreated
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("client-accounts/{clientId}")]
        public async Task<IActionResult> GetClientAccountsReport(int clientId)
        {
            var data = await _repositoryContext.BankAccounts
                .AsNoTracking()
                .Include(a => a.Currency)
                .Where(a => a.ClientId == clientId && a.IsActive)
                .Select(a => new ReportAccountRowDto
                {
                    AccountCode = a.Code,
                    AccountName = a.Name,
                    Currency = a.Currency != null ? a.Currency.Code : string.Empty,
                    CurrentBalance = a.Balance
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
