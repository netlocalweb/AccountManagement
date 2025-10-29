using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IMapper mapper;

        public ProductController(IProductRepository repository, IWebHostEnvironment env, IMapper mapper)
        {
            this.repository = repository;
            this.env = env;
            this.mapper = mapper;
        }
        //Get all method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await repository.GetAllAsync();
            var dtos = mapper.Map<IEnumerable<ProductReadDto>>(products);

          
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                    dto.ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{dto.ImageUrl}";
            }

            return Ok(dtos);
        }

        //Get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) 
                return NotFound();

            var dto = mapper.Map<ProductReadDto>(product);

            if (!string.IsNullOrEmpty(dto.ImageUrl))
                dto.ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{dto.ImageUrl}";

            return Ok(dto);
        }

        //Create method
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductsCreateDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

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
            var product = mapper.Map<Product>(dto);
            product.ImageUrl = imagePath;

            var created = await repository.AddAsync(product);
            var readDto = mapper.Map<ProductReadDto>(created);

            if (!string.IsNullOrEmpty(readDto.ImageUrl))
                readDto.ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{readDto.ImageUrl}";

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, readDto);
        }

        //Update method
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductsCreateDto dto)
        {
            var existing = await repository.GetByIdAsync(id);
            if (existing == null) 
                return NotFound();

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
            mapper.Map(dto, existing);

            await repository.UpdateAsync(existing);
            return NoContent();
        }

        //Delete method
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
