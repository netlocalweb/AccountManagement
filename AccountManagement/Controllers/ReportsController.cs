using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDapperRepository _dapperRepository;
        public ReportsController(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        //Merr llogarite e klienteve
        [HttpGet("client-accounts")]
        public async Task<IActionResult>GetClientAccounts()
        {
            var result = await _dapperRepository.GetClientAccountAsync();
            return Ok(result);
        }
        //Merr te gjitha transasionet e nje llogarie
        [HttpGet("account-transaction/{accountId}")]
        public async Task<IActionResult>GetTransactions(int accountId)
        {
            var result = await _dapperRepository.GetAccountTransactionsAsync(accountId);
            return Ok(result);
        }
        //Merr llogarite aktive per nje klient
        [HttpGet("client-active-accounts/{clientId}")]
        public async Task<IActionResult>GetClientActiveAccounts(int clientId)
        {
            var result = await _dapperRepository.GetClientActiveAccountsAsync(clientId);
            return Ok(result);
        }

        //Merr produktet sipas nje kategorie
        [HttpGet("category-products/{categoryId}")]
        public async Task<IActionResult>GetProductsByCategory(int categoryId)
        {
            var result = await _dapperRepository.GetProductsByCategoryAsync(categoryId);
            return Ok(result);
        }
 
    }
}
