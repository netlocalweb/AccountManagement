using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers.CustomAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsOfEach : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductsOfEach(
            ICategoryRepository categoryRepo,
            IProductRepository productRepo,
            IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductsOfCategory(int id)
        {
            var catg = _categoryRepo.FindById(id);

            if (catg == null)
            {
                return NotFound($"The Category with ID {id} not found.");
            }

            var products = _productRepo.FindByCategoryId(id);

            if (products == null || !products.Any())
            {
                return NotFound($"No products found of this category.");
            }

            var productsDto = _mapper.Map<List<ProductDTO>>(products);

            return Ok(productsDto);
        }
    }
}
