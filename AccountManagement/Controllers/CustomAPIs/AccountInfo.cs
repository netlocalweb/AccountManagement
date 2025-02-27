using Contracts; 
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Entities.DTO;

namespace AccountManagement.Controllers.CustomAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountInfo : ControllerBase
    {
        private readonly IClientRepository _clientRepo;
        private readonly IBankAccountRepository _bankAccRepo;
        private readonly ICurrencyRepository _currencyRepo;
        private readonly IMapper _mapper;

        public AccountInfo(
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

        [HttpGet("bankaccountinfo/{id}")]
        public IActionResult GetBankAccountInfo(int id)
        {
            var acc = _bankAccRepo.FindById(id);

            if (acc == null)
            {
                return NotFound($"The Account with ID {id} not found.");
            }

            var client = _clientRepo.FindById(acc.ClientId);
            var clientDto = _mapper.Map<ClientDTO>(client);

            var currency = _currencyRepo.FindById(acc.CurrencyId);
            var currencyDto = _mapper.Map<CurrencyDTO>(currency);

            return Ok(new
            {
                AccountCode = acc.Id,
                ClientId = clientDto.Id,
                ClientName = clientDto.Username,
                Currency = currencyDto.Description,
                CurrentBalance = acc.Balance,
            });
        }
    }
}
