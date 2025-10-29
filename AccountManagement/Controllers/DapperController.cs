using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Repository;
using Microsoft.AspNetCore.Authorization;
using Contracts;
using System;


namespace AccountManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
            private readonly IDapperRepository repository;

            public DapperController(IDapperRepository repository)
            {
            this.repository = repository;
            }

        [HttpGet("clients-accounts")]
        public async Task<IActionResult> GetClientAccountReport()
        {
            
                var result = await repository.GetClientAccountsAsync();
                return Ok(result);
            }
         
        


        [HttpGet("transactions/{accountId}")]
            public async Task<IActionResult> GetTransactionsByAccount(int accountId)
            {
                var result = await repository.GetTransactionsByAccountAsync(accountId);
                return Ok(result);
            }


            [HttpGet("client/{clientId}/accounts")]
            public async Task<IActionResult> GetAccountsByClient(int clientId)
            {
                var result = await repository.GetActiveAccountsByClientAsync(clientId);
                return Ok(result);
            }


            [HttpGet("category/{categoryId}/products")]
            public async Task<IActionResult> GetProductsByCategory(int categoryId)
            {
                var result = await repository.GetProductsByCategoryAsync(categoryId);
                return Ok(result);
            }
        }
    }



