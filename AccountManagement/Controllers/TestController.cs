//using Contracts;
//using Microsoft.AspNetCore.Mvc;

//namespace AccountManagement.Controllers
//{
//    [Route("api/test/[action]")]
//    [ApiController]
//    public class TestController : ControllerBase
//    {
//        private readonly IRepositoryManager _repository;
//        private readonly ILoggerManager _logger;
//        private readonly IDapperRepository _dapperRepository;

//        public TestController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
//        {
//            _repository = repository;
//            _logger = logger;
//            _dapperRepository = dapperRepository;
//        }

//        /// <summary>
//        /// Test Api From Base
//        /// </summary>
//        /// <returns>Test String</returns>
//        [HttpGet(Name = "testfrombase")]
//        public IActionResult TestFromBase()
//        {
//            var testStr = _repository.TestRepository.TestMethodFromBase();

//            _logger.LogInfo("test method from base is called");

//            return Ok(testStr);
//        }

//        /// <summary>
//        /// Test Api
//        /// </summary>
//        /// <returns>Test String</returns>
//        [HttpGet(Name = "testapi")]
//        public IActionResult TestApi()
//        {
//            var testStr = _repository.TestRepository.TestMethod();

//            _logger.LogInfo("test method is called");

//            return Ok(testStr);
//        }

//        [HttpGet("dapper-get-all")]
//        public IActionResult DapperGetAll()
//        {
//            var result = _dapperRepository.GetAll();
//            return Ok(result);
//        }

//        [HttpGet("dapper-get-by-id/{id}")]
//        public IActionResult DapperGetById(int id)
//        {
//            var result = _dapperRepository.GetById(id);
//            if (result == null)
//                return NotFound($"Entity with id {id} not found");

//            return Ok(result);
//        }
//    }
//}