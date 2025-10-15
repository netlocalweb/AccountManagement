
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public ClientController(IRepositoryManager repositoryManager, IMapper mapper,UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        //GET ALL CLIENTS
        //GET: api/client
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _repositoryManager.Client.GetAllClientsAsync(trackChanges: false);
            var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);
            return Ok(clientsDto);
        }

        //Get Clients by id
        //GET:api/client/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var clients = await _repositoryManager.Client.GetClientByIdAsync(id, trackChanges: false);
            if (clients == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ClientDto>(clients));
        }

        //Update CLient
        //UPDATE::api/client/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult>UpdateClient(int id, UpdateClientDto updateClient)
        {
            
            var client = await GetClientAndCheckIfExists(id ,trackChanges: true);

            if (updateClient == null)
                return BadRequest("Update Data is reuired.");

            if (!string.IsNullOrEmpty(updateClient.FirstName) ||
                !string.IsNullOrEmpty(updateClient.LastName) ||
                !string.IsNullOrEmpty(updateClient.Email)||
                updateClient.IsLockedOut !=null)
            {
                var user = await _userManager.FindByIdAsync(client.UserId);
                if (user == null)
                    throw new KeyNotFoundException($"User for client {id} not found.");
                
                if(!string.IsNullOrEmpty(updateClient.FirstName))
                    user.FirstName=updateClient.FirstName;

                if(!string.IsNullOrEmpty(updateClient.LastName))
                    user.LastName=updateClient.LastName;

                if(!string.IsNullOrEmpty(updateClient.Email))
                    user.Email=updateClient.Email;

                await _userManager.UpdateAsync(user);
            }
            _mapper.Map(updateClient, client);
            _repositoryManager.Client.UpdateClient(client);

            await _repositoryManager.SaveAsync();
            return Ok();
        }

        //Delete Client
        //GET:api/client/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await GetClientAndCheckIfExists(id, false);

            _repositoryManager.Client.DeleteClient(client);
            return Ok();
        }

        private async Task<Client> GetClientAndCheckIfExists(int id, bool trackChanges)
        {

            var client = await _repositoryManager.Client.GetClientByIdAsync(id, trackChanges);
            
            if (client == null)
                throw new KeyNotFoundException($"Client with id : {id} doesn't exist in the database. ");

            return client;
        }
    }
}