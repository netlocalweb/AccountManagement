using Contracts;
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

            _logger.LogInfo("Second Internship Custom API");

            return Ok(testStr);
        }

        //GET: THIRDAPI
        [HttpGet("ThirdAPI")]
        public IActionResult Third(int id)
        {
            var testStr = _dapperRepository.ThirdApi(id);

            _logger.LogInfo("Third Internship Custom API");

            return Ok(testStr);
        }

        //GET: FOURTHAPI
        [HttpGet("FourthAPI")]
        public IActionResult Fourth(int id)
        {
            var testStr = _dapperRepository.FourthAPI(id);

            _logger.LogInfo("Fourth Internship Custom API");

            return Ok(testStr);
        }



    }
}