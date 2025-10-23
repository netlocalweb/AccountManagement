using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var product = await _repositoryManager.Products.GetAllProductsAsync(trackchanges: false);
            var productsDto = _mapper.Map<IEnumerable<ProductsDto>>(product);
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
                return NotFound();

            var productsDto = _mapper.Map<ProductsDto>(product);
            return Ok(productsDto);
        }

        ////Create product
        ////POST:api/product
        //[HttpPost]
        //public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto productDto)
        //{

        //}

        ////Delete product
        ////DELETE:api/products
        //[HttpDelete]
        //[Route("{id:int}")]
        //public async Task<IActionResult>DeleteProduct(int id)
        //{

        //}
    }
}
