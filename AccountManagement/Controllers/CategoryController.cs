using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(IRepositoryManager repositoryManager, IMapper mapper, ILogger<CategoryController> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;

        }

        //Get all categories
        //GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var category = await _repositoryManager.Category.GetAllCategoriesAsync(trackChanges: false);
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(category);
            return Ok(categoryDto);
        }

        //Get category by id
        //GET:api/category/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _repositoryManager.Category.GetCategoryByIdAsync(id, trackChanges: false);
            if (category == null)
                return NotFound();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        //Create Category
        //POST:api/category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Category is null");

            categoryDto.Code = categoryDto.Code.ToUpper();

            //Kontrollojme nqs nje category me te njejtin code ekziston
            var exists = await _repositoryManager.Category.GetCategoryByCodeAsync(categoryDto.Code,false);
            if (exists != null)
                return Conflict($"A category with code '{categoryDto.Code}' alredy exists.");


            var category = _mapper.Map<Category>(categoryDto);
            category.DateCreated = DateTime.Now;

            _repositoryManager.Category.CreateCategory(category);
            await _repositoryManager.SaveAsync();

            var categoryToReturn = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryToReturn.Id }, categoryToReturn);

        }

        //Update Category
        //PUT:api/category
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult>UpdateCategory(int id, [FromBody] CategoryForUpdateDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Category is null.");

            var category = await _repositoryManager.Category.GetCategoryByIdAsync(id, trackChanges: true);
            if (category == null)
                return NotFound($"Category with id {id} not found.");

            var newCode = categoryDto.Code.ToUpper();
            category.Code = newCode;
            category.Description = categoryDto.Description;
            category.DateModified = DateTime.Now;

            await _repositoryManager.SaveAsync();
            return NoContent();
        }

        //Delete Category
        //DELETE:api/category
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            var category = await _repositoryManager.Category.GetCategoryByIdAsync(id ,trackChanges :false);
            if (category == null)
                return NotFound();

            _repositoryManager.Category.DeleteCategory(category);
            await _repositoryManager.SaveAsync();

            return NoContent();

        }

    }
}
