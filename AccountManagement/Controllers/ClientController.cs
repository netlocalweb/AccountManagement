using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var clients = _clientRepository.FindAll();
            var clientDTOs = _mapper.Map<List<ClientDTO>>(clients);
            return Ok(clientDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientRepository.FindById(id);
            if (client == null)
            {
                return NotFound();
            }
            var clientDTO = _mapper.Map<ClientDTO>(client);
            return Ok(clientDTO);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, ClientRegisterDTO clientRegisterDTO)
        {
            if (clientRegisterDTO == null || id != clientRegisterDTO.Id)
            {
                return BadRequest();
            }

            var existingClient = _clientRepository.FindById(id);
            if (existingClient == null)
            {
                return NotFound();
            }

            var client = _mapper.Map<Client>(clientRegisterDTO);
            _clientRepository.Update(client);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = _clientRepository.FindById(id);
            if (client == null)
            {
                return NotFound();
            }

            _clientRepository.Delete(id);
            return NoContent();
        }
    }
}
