using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System;

namespace AccountManagement.Controllers
{
    [Route("api/product/")]
    [ApiController]
    public class ProductController : Controller
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public ProductController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: CREATE
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateProductDTO productDTO)
        {
            var product = new Product(productDTO.Name, productDTO.ShortDescription, productDTO.LongDescription,productDTO.CategoryId, productDTO.Price);
            
            _repository.ProductRepository.CreateRecord(product, out string ErrorMessage);
            _repository.ProductRepository.SaveChanges();
            
            _logger.LogInfo(ErrorMessage);
            
            return Ok(ErrorMessage);
            
        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.ProductRepository.GetRecordById(id);
            _logger.LogInfo("Get Category records by id");
            return Ok(testStr);
        }

        //GET: GETALL
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var testStr = _repository.ProductRepository.GetAllRecords();

            _logger.LogInfo("Get all Category records");

            return Ok(testStr);
        }

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] CreateProductDTO productDTO)
        {
            var productUpdated = new Product(productDTO.Name, productDTO.ShortDescription, productDTO.LongDescription,productDTO.CategoryId, productDTO.Price);

            _repository.ProductRepository.UpdateRecord(id, productUpdated, out string ErrorMessage);
            _repository.ProductRepository.SaveChanges();
            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.ProductRepository.RemoveRecord(id);
            _repository.ProductRepository.SaveChanges();

            _logger.LogInfo("Delete a category record");

            return Ok("Category deleted from database.");
        }
        
        [HttpPost("uploadImageWithId/{id}")]
        public IActionResult UploadImage(int id, [FromForm] FileUploadDTO imageToUpload)
        {

            var productObj = _repository.ProductRepository.GetRecordById(id);
            var base64Sring = "";
            using (var ms = new MemoryStream())
            {
                imageToUpload.files.CopyTo(ms);
                base64Sring = Convert.ToBase64String(ms.ToArray());
            }
            
            _repository.ProductRepository.UploadImage(id, base64Sring);
            _repository.ProductRepository.SaveChanges();

            _logger.LogInfo("Product image added sucefully!");
            return Ok("Product image added sucefully!");
        }

        //GET: GETBYID
        [HttpGet("getImageStringWithId/{id}")]
        public IActionResult GetImage(int id)
        {
            var productObj = _repository.ProductRepository.GetRecordById(id);
            _logger.LogInfo("Get Category records by id");
            return Ok(productObj.Image);
        }



    }
}

