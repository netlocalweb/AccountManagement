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
        [HttpGet]
        [Route("{id:int}")]
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
        public async Task<IActionResult> CreateProduct( [FromBody] ProductForCreationDto productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null.");

            if (string.IsNullOrWhiteSpace(productDto.Name))
                return BadRequest("Prodct name is required.");

            //Checking if the product exists 
            var existingProduct = await _repositoryManager.Products.GetProductByNameAsync(productDto.Name, trackchanges: false);

            if (existingProduct == null)
                return Conflict($"A product with the name '{productDto.Name}' alredy exists .");

            var product = _mapper.Map<Products>(productDto);
            product.DateCreated = DateTime.Now;

            //Base64 image 
            if (!string.IsNullOrEmpty(productDto.ImagePath))
            {
                try
                {
                    var imageBytes = Convert.FromBase64String(productDto.ImagePath);

                    //Save a file 
                    string folderPath = Path.Combine(_env.WebRootPath, "images");
                    Directory.CreateDirectory(folderPath);

                    string fileNAme = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(folderPath, fileNAme);

                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                    //Save relative path for saving 
                    product.ImagePath = Path.Combine("images", fileNAme);
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid image fromat.");
                }
            }
            _repositoryManager.Products.Create(product);
            await _repositoryManager.SaveAsync();

            var productToReturn = _mapper.Map<ProductsDto>(product);
            return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);

        }
        //Update product
        //PUT:api/product/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductForUpdateDto productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null.");

            if (string.IsNullOrWhiteSpace(productDto.Name))
                return BadRequest("Product name is required.");

            var product = await _repositoryManager.Products.GetProductsByIdAsync(id ,trackchanges: true);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
            //Check if the name exists
            var existingProduct = await _repositoryManager.Products.GetProductByNameAsync(productDto.Name, trackchanges: false);

            if (existingProduct != null && existingProduct.Id != id)
                return Conflict($"A product with the name '{productDto.Name}' alredy exists.");

            _mapper.Map(productDto,product);
            product.DateModified = DateTime.Now;

            //Base64 image replecement 
            if (!string.IsNullOrEmpty(productDto.ImagePath))
            {
                try
                {
                    if (!string.IsNullOrEmpty(productDto.ImagePath))
                    {
                        var oldImagePAth = Path.Combine(_env.WebRootPath, productDto.ImagePath);
                        if (System.IO.File.Exists(oldImagePAth))
                            System.IO.File.Delete(oldImagePAth);
                    }

                    //THe new image
                    var imageBytes = Convert.FromBase64String(productDto.ImagePath);

                    string folderPath = Path.Combine(_env.WebRootPath, "image");
                    Directory.CreateDirectory(folderPath);

                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(folderPath, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                    product.ImagePath = Path.Combine("images", fileName);
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid image format.");
                }
            }
                await _repositoryManager.SaveAsync();
                return NoContent();
            }


        ////Delete product
        ////DELETE:api/products
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repositoryManager.Products.GetProductsByIdAsync(id, trackchanges: false);
            if (product == null)
                return NotFound();

            //Delete image 
            if(!string.IsNullOrEmpty(product.ImagePath))
            {
                var fullPath = Path.Combine(_env.WebRootPath, product.ImagePath);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }

            _repositoryManager.Products.Delete(product);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
