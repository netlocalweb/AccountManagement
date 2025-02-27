using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryRepository.FindAll();
            var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
            {
                return NotFound();
            }
            
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryDTO addCategoryDTO)
        {
            if (addCategoryDTO == null)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(addCategoryDTO);
            _categoryRepository.Create(category);

            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDTO);
        }

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

            var category = _mapper.Map<Category>(addCategoryDTO);
            _categoryRepository.Update(category);

            return NoContent();
        }

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
