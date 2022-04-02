using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/test/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public TestController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Test Api From Base
        /// </summary>
        /// <returns>Test String</returns>
        [HttpGet(Name = "testfrombase")]
        public IActionResult TestFromBase()
        {
            var testStr = _repository.TestRepository.TestMethodFromBase();

            _logger.LogInfo("test method from base is called");

            return Ok(testStr);
        }

        /// <summary>
        /// Test Api
        /// </summary>
        /// <returns>Test String</returns>
        [HttpGet(Name = "testapi")]
        public IActionResult TestApi()
        {
            var testStr = _repository.TestRepository.TestMethod();

            _logger.LogInfo("test method is called");

            return Ok(testStr);
        }
    }
}