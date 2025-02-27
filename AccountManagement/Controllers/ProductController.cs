using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.FindAll();
            var productDTOs = _mapper.Map<List<ProductDTO>>(products);
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public IActionResult AddProduct(CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(createProductDTO);
            _productRepository.Create(product);

            var productDTO = _mapper.Map<ProductDTO>(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null || id != createProductDTO.Id)
            {
                return BadRequest();
            }

            var existingProduct = _productRepository.FindById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<Product>(createProductDTO);
            _productRepository.Update(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);
            return NoContent();
        }
    }
}
