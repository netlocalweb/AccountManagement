using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        //Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await repository.GetAllAsync();
            return Ok(categories);
        }

        //Get categories by id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await repository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        //Create category 
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await repository.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        //Update category
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id) return BadRequest("ID not matching");
            var updated = await repository.UpdateAsync(category);
            return Ok(updated);
        }

        //Deleting category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
