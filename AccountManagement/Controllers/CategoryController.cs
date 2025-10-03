using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using AccountManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
            var result = categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Code = c.Code,
                Description = c.Description,
                DateCreated = c.DateCreated,
                DateModified = c.DateModified
            });
            return Ok(result);
        }

        //Get categories by id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            return Ok(new CategoryReadDto
            {
                Id = category.Id,
                Code = category.Code,
                Description = category.Description,
                DateCreated = category.DateCreated,
                DateModified = category.DateModified
            });
            }

        //Create category 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = new Category
            {
                Code = dto.Code,
                Description = dto.Description
            };

            try
            {
                var created = await repository.AddAsync(category);

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, new CategoryReadDto
                {
                    Id = created.Id,
                    Code = created.Code,
                    Description = created.Description,
                    DateCreated = created.DateCreated
                });
            }
            catch (Exception ex)
            {
                //If its not unique 
                return Conflict($"Could not create category: {ex.Message}");
            }
        }

        //Update category
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var category = new Category
            {
                Description = dto.Description
            };

            var updated = await repository.UpdateAsync(id, category);
            if (updated == null) return NotFound();

            return Ok(new CategoryReadDto
            {
                Id = updated.Id,
                Code = updated.Code,
                Description = updated.Description,
                DateCreated = updated.DateCreated,
                DateModified = updated.DateModified
            });
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
