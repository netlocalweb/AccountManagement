using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AccountManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        //Get all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await repository.GetAllAsync();
            return Ok(products);
        }
        //Get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        //Create 
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest("Id is not matching");

            var created = await repository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        }
        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if(id != product.Id) return BadRequest("Id is not matching");

            var updated = await repository.UpdateAsync(product);
            return Ok(updated);

        }
        //Delete by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();


        }
    }
}
