using Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("client-accounts")]
        public async Task<IActionResult>GetClientAccounts()
        {
            var result = await _dapperRepository.GetClientAccountAsync();
            return Ok(result);
        }

        [HttpGet("account-transaction/{accountId}")]
        public async Task<IActionResult>GetTransactions(int accountId)
        {
            var result = await _dapperRepository.GetAccountTransactionsAsync(accountId);
            return Ok(result);
        }
        [HttpGet("client-active-accounts/{clientId}")]
        public async Task<IActionResult>GetClientActiveAccounts(int clientId)
        {
            var result = await _dapperRepository.GetClientActiveAccountsAsync(clientId);
            return Ok(result);
        }

        [HttpGet("catgeory-products/{categoryId}")]
        public async Task<IActionResult>GetProductsByCategory(int categoryId)
        {
            var result = await _dapperRepository.GetProductsByCategoryAsync(categoryId);
            return Ok(result);
        }
 
    }
}
