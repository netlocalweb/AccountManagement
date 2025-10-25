using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DapperContext _context;
        public ReportsController(DapperContext context)
        {
            _context = context;
        }

        //[HttpGet("clients-with-accounts")]
        //public async Task<IActionResult>GetClientWithAccounts()
        //{
         
        //}
    }
}
