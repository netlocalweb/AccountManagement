using Contracts;
using Entities.DTO;  // Use the DTO namespace
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using AutoMapper;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;  // AutoMapper

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Get All Categories
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryRepository.FindAll();
            // Map to CategoryDTO list
            var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }

        // Get Category By Id
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
            {
                return NotFound();
            }
            // Map to CategoryDTO
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }

        // Add a New Category
        [HttpPost]
        public IActionResult AddCategory(AddCategoryDTO addCategoryDTO)
        {
            if (addCategoryDTO == null)
            {
                return BadRequest();
            }

            // Map AddCategoryDTO to Category entity
            var category = _mapper.Map<Category>(addCategoryDTO);
            _categoryRepository.Create(category);

            // Map the newly created category to CategoryDTO for response
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDTO);
        }

        // Update an Existing Category
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, AddCategoryDTO addCategoryDTO)
        {
            if (addCategoryDTO == null || id != addCategoryDTO.Id)
            {
                return BadRequest();
            }

            var existingCategory = _categoryRepository.FindById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Map AddCategoryDTO to Category entity
            var category = _mapper.Map<Category>(addCategoryDTO);
            _categoryRepository.Update(category);

            return NoContent();
        }

        // Delete a Category
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}
