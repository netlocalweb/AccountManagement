using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/clients/")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public ClientsController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: CREATE
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateClientDTO createClientDto)
        {
            //var client = new Clients(createClientDto.FirstName, createClientDto.LastName, createClientDto.Email, createClientDto.Birthdate,
              //  createClientDto.Phone, createClientDto.Username, createClientDto.Password);

            _repository.ClientsRepository.CreateRecord(createClientDto, out string ErrorMessage);
            _repository.ClientsRepository.SaveChanges();

            _logger.LogInfo("Create a new client record");

            return Ok(ErrorMessage);

        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.ClientsRepository.GetRecordById(id);
            _logger.LogInfo("Get Client records by id");
            if (testStr == null)
            {
                return NotFound("There is no user with this ID in Database");
            }
            else
            {
                return Ok(testStr);
            }
        }

        //GET: GETALL
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var testStr = _repository.ClientsRepository.GetAllRecords();

            _logger.LogInfo("Get all Clients records");

            return Ok(testStr);
        }

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] UpdateClientDTO createClientDto)
        {
            //var clientUpdated = new Clients(createClientDto.FirstName, createClientDto.LastName, createClientDto.Email, createClientDto.Birthdate,
               // createClientDto.Phone, createClientDto.Username, createClientDto.Password);

            _repository.ClientsRepository.UpdateRecord(id, createClientDto, out string ErrorMessage);
            _repository.ClientsRepository.SaveChanges();
            _logger.LogInfo("Update a record");

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.ClientsRepository.RemoveRecord(id, out bool check);
            _repository.ClientsRepository.SaveChanges();

            _logger.LogInfo("Delete a clients record");

            return Ok("Client deleted from database");
        }

    }
}