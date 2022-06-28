using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccountManagement.Controllers
{
    [Route("api/category/")]
    [ApiController]
    public class CategoryController : Controller
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public CategoryController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: CREATE
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var category = new Category(createCategoryDTO.Code, createCategoryDTO.Description);

            _repository.CategoryRepository.CreateRecord(category, out string ErrorMessage);
            _repository.CategoryRepository.SaveChanges();

            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);

        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.CategoryRepository.GetRecordById(id);
            _logger.LogInfo("Get Category records by id");
            if (testStr == null)
            {
                return NotFound("There is no Category with this ID in Database");
            }
            else
            {
                return Ok(testStr);
            }
        }

        //GET: GETALL
        [HttpPost("getall")]
        public IActionResult GetAll([FromBody] PagingParameter pagingParameter)
        {
            var testStr = _repository.CategoryRepository.GetAllRecords(pagingParameter.PageNumber, pagingParameter.PageSize, out int totalRecords);
            var pageInfo = new Pager<IEnumerable<Category>>(totalRecords, pagingParameter.PageNumber, pagingParameter.PageSize, data: testStr);
            if(pagingParameter.PageNumber > pageInfo.TotalPages)
            {
                return BadRequest("The records you are requesting have less pages than your requested page number!");
            }
            else
            {
                _logger.LogInfo("Get all Category records");

                return Ok(pageInfo);
            }
            
        }

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCategoryDTO update)
        {
            var categoryUpdated = new Category(update.Code, update.Description);

            _repository.CategoryRepository.UpdateRecord(id, categoryUpdated, out string ErrorMessage);
            _repository.CategoryRepository.SaveChanges();
            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.CategoryRepository.RemoveRecord(id, out bool check);
            if (check == false)
            {
                return NotFound("There is no Category with this ID in Database");
            }
            else
            {
                _repository.CategoryRepository.SaveChanges();

                _logger.LogInfo("Delete a category record");

                return Ok("Category deleted from database.");
            }
            
        }
    }
}
