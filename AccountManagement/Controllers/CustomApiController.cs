using Contracts;
using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/CustomAPI/")]
    [ApiController]
    public class CustomApiController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public CustomApiController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }



        //GET: FIRSTAPI
        [HttpGet("FirstAPI")]
        public IActionResult First()
        {
            var testStr = _dapperRepository.FirstAPI();

            _logger.LogInfo("First Internship Custom API");

            return Ok(testStr);
        }

        //GET: SECONDAPI
        [HttpGet("SecondAPI")]
        public IActionResult Second(int id)
        {
            
            var testStr = _dapperRepository.SecondApi(id);
            if(testStr != null && testStr.GetEnumerator().MoveNext())
            {
                _logger.LogInfo("Second Internship Custom API");

                return Ok(testStr);
                
            }
            else
            {
                return NotFound("There is no bank account that matches your id or there are no bank transactions for this bank account!");
            }
           
        }

        //GET: THIRDAPI
        [HttpGet("ThirdAPI")]
        public IActionResult Third(int id)
        {
            var testStr = _dapperRepository.ThirdApi(id);
            
            if (testStr != null && testStr.GetEnumerator().MoveNext())
            {
                 _logger.LogInfo("Third Internship Custom API");

                 return Ok(testStr);

            }
            else
            {
                return NotFound("There is no Client that matches your id or there are no bank accounts for this client yet!");
            }
        }

        //GET: FOURTHAPI
        [HttpGet("FourthAPI")]
        public IActionResult Fourth(int id)
        {
            var testStr = _dapperRepository.FourthAPI(id);

            if (testStr != null && testStr.GetEnumerator().MoveNext())
            {
                _logger.LogInfo("Forth Internship Custom API");

                return Ok(testStr);

            }
            else
            {
                return NotFound("There is no Category that matches your id or there are no Products for this Category yet!");
            }
        }



    }
}