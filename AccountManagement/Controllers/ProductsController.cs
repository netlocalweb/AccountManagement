using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        private readonly IWebHostEnvironment _env;
        public ProductsController(IRepositoryManager repositoryManager,IMapper mapper,ILogger<ProductsController> logger,IWebHostEnvironment env)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
            _env = env;
        }

        //Get products 
        //GET:api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repositoryManager.Products.GetAllProductsAsync(trackchanges: false);
            var productsDto = _mapper.Map<IEnumerable<ProductsDto>>(products);

            //Convert image to base64
            foreach (var product in productsDto)
            {
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var fullPath = Path.Combine(_env.WebRootPath, product.ImagePath);
                    if(System.IO.File.Exists(fullPath))
                    {
                        var imageBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                        product.ImagePath = Convert.ToBase64String(imageBytes);
                    }
                }
            }
            return Ok(productsDto);
        }

        //Get products by id
        //GET:api/products/{id}
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult>GetProductsById(int id)
        {
            var product = await _repositoryManager.Products.GetProductsByIdAsync(id, trackchanges: false);
            if (product == null)
                return NotFound($"Product with Id {id} not found.");

            var productsDto = _mapper.Map<ProductsDto>(product);

            //Image 
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var fullPath = Path.Combine(_env.WebRootPath, product.ImagePath);
                if (System.IO.File.Exists(fullPath))
                {
                    var imageBytes = await System.IO.File.ReadAllBytesAsync (fullPath);
                    productsDto.ImagePath = Convert.ToBase64String(imageBytes);
                }
            }
            return Ok(productsDto);
        }

        ////Create product
        ////POST:api/product
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductForCreationDto dto)
        {
            if (dto == null) 
                return BadRequest("Product data is null.");
            if (string.IsNullOrWhiteSpace(dto.Name)) 
                return BadRequest("Product name is required.");
            if (dto.Price < 0) 
                return BadRequest("Price must be >= 0.");

            var existing = await _repositoryManager.Products.GetProductByNameAsync(dto.Name, false);
            if (existing != null) 
                return Conflict($"A product with name '{dto.Name}' already exists.");

            var product = _mapper.Map<Products>(dto);
            product.DateCreated = DateTime.Now;

            if (dto.ImagePath != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(dto.ImagePath.FileName);
                string filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImagePath.CopyToAsync(stream);

                product.ImagePath = Path.Combine("images", fileName).Replace("\\", "/");
            }

            _repositoryManager.Products.Create(product);
            await _repositoryManager.SaveAsync();

            var productDto = _mapper.Map<ProductsDto>(product);
            return CreatedAtRoute("GetProductById", new { id = productDto.Id }, productDto);
        }

        // PUT: api/products/{id}
        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductForUpdateDto productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null.");

            if (string.IsNullOrWhiteSpace(productDto.Name))
                return BadRequest("Product name is required.");

            var product = await _repositoryManager.Products.GetProductsByIdAsync(id, trackchanges: true);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            // Check if the name exists for another product
            var existingProduct = await _repositoryManager.Products.GetProductByNameAsync(productDto.Name, false);
            if (existingProduct != null && existingProduct.Id != id)
                return Conflict($"A product with the name '{productDto.Name}' already exists.");

            // Map other fields
            _mapper.Map(productDto, product);

            // Handle image file replacement
            if (productDto.ImagePath != null)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, product.ImagePath);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // Save new image
                string folderPath = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImagePath.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.ImagePath.CopyToAsync(stream);
                }

                product.ImagePath = Path.Combine("images", fileName).Replace("\\", "/");
            }

            await _repositoryManager.SaveAsync();
            return NoContent();
        }


        ////Delete product
        ////DELETE:api/products
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repositoryManager.Products.GetProductsByIdAsync(id, false);
            if (product == null)
                return NotFound();

            // Delete image safely
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                try
                {
                    var fullPath = Path.Combine(_env.WebRootPath ?? string.Empty, product.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Could not delete image file: {ex.Message}");
                }
            }

            _repositoryManager.Products.Delete(product);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

    }
}
