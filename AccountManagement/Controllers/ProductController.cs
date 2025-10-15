using AccountManagement.Models;
using AccountManagement.Repositories;
using AccountManagement.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;
        private readonly IWebHostEnvironment env;

        public ProductController(IProductRepository repository, IWebHostEnvironment env)
        {
            this.repository = repository;
            this.env = env;
        }
        //Get all method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await repository.GetAllAsync();
            var dtos = products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                ShortDescription = p.ShortDescription,
                LongDescription = p.LongDescription,
                CategoryId = p.CategoryId,
                Price = p.Price,
                ImageUrl = p.ImageUrl != null ? $"{Request.Scheme}://{Request.Host}/images/{p.ImageUrl}" : null,
                DateCreated = p.DateCreated,
                DateModified = p.DateModified
            });

            return Ok(dtos);
        }

        //Get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            var dto = new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ImageUrl = product.ImageUrl != null ? $"{Request.Scheme}://{Request.Host}/images/{product.ImageUrl}" : null,
                DateCreated = product.DateCreated,
                DateModified = product.DateModified
            };
            return Ok(dto);
        }

        //Create method
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductsCreateDto dto)
        {
            string? imagePath = null;

            if (dto.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
                var path = Path.Combine(env.WebRootPath, "images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = fileName;
            }

            var product = new Product
            {
                Name = dto.Name,
                ShortDescription = dto.ShortDescription,
                LongDescription = dto.LongDescription,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                ImageUrl = imagePath
            };

            var created = await repository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        //Update method
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductsCreateDto dto)
        {
            var existing = await repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (dto.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
                var path = Path.Combine(env.WebRootPath, "images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                existing.ImageUrl = fileName;
            }

            existing.Name = dto.Name;
            existing.ShortDescription = dto.ShortDescription;
            existing.LongDescription = dto.LongDescription;
            existing.CategoryId = dto.CategoryId;
            existing.Price = dto.Price;

            await repository.UpdateAsync(existing);
            return NoContent();
        }

        //Delete method
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
