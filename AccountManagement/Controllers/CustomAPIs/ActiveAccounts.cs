using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers.CustomAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveAccounts : ControllerBase
    {
        private readonly IClientRepository _clientRepo;
        private readonly IBankAccountRepository _bankAccRepo;
        private readonly ICurrencyRepository _currencyRepo;
        private readonly IMapper _mapper;

        public ActiveAccounts(
            IClientRepository clientRepo,
            IBankAccountRepository bankAccRepo,
            ICurrencyRepository currencyRepo,
            IMapper mapper)
        {
            _clientRepo = clientRepo;
            _bankAccRepo = bankAccRepo;
            _currencyRepo = currencyRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetActiveAccountsInfo(int id)
        {
            var client = _clientRepo.FindById(id);

            if (client == null)
            {
                return NotFound($"The Client with ID {id} not found.");
            }

            var accounts = _bankAccRepo.FindByClientId(id);

            if (accounts == null || !accounts.Any())
            {
                return NotFound($"No existing accounts were found for this client.");
            }

            var accountsDto = _mapper.Map<List<BankAccountDTO>>(accounts);

            return Ok(accountsDto);
        }
    }
}
