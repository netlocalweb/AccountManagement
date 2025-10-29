using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }

        //Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await repository.GetAllAsync();
            var dtoList = mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return Ok(dtoList);
        }

        //Get categories by id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await repository.GetByIdAsync(id);
            if (category == null) 
                return NotFound();

            var dto = mapper.Map<CategoryReadDto>(category);
            return Ok(dto);
        }

        //Create category 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var category = mapper.Map<Category>(dto);

            try
            {
                var created = await repository.AddAsync(category);
                var readDto = mapper.Map<CategoryReadDto>(created);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, readDto);
            }
            catch (Exception ex)
            {
                return Conflict($"Could not create category: {ex.Message}");
            }
        }

        //Update category
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Maps updated fields from Dto to existing entity
            mapper.Map(dto, existing);

            try
            {
                var updated = await repository.UpdateAsync(id, existing);
                var readDto = mapper.Map<CategoryReadDto>(updated);
                return Ok(readDto);
            }
            catch (Exception ex)
            {
                return Conflict($"Could not update category: {ex.Message}");
            }
        }

        //Deleting category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);
            if (!deleted) 
                return NotFound();
            return NoContent();
        }
    }
}
