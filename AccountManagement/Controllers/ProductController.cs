using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

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
            var product = new Product(productDTO.Name, productDTO.ShortDescription, productDTO.LongDescription, productDTO.CategoryId, productDTO.Price);

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
            if (testStr == null)
            {
                return NotFound("There is no Product with this ID in Database");
            }
            else
            {
                return Ok(testStr);
            }
        }

        //GET: GETALL
        [HttpPost("getall")]
        public IActionResult GetAll([FromBody] PagingParameter pagingParameter)
        {
            var testStr = _repository.ProductRepository.GetAllRecords(pagingParameter.PageNumber, pagingParameter.PageSize, out int totalRecords);
            var pageInfo = new Pager<IEnumerable<Product>>(totalRecords, pagingParameter.PageNumber, pagingParameter.PageSize, data: testStr);
            if (pagingParameter.PageNumber > pageInfo.TotalPages)
            {
                return BadRequest("The records you are requesting have less pages than your requested page number!");
            }
            else
            {
                _logger.LogInfo("Get all Category records");

                return Ok(pageInfo);
            }
        }

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] CreateProductDTO productDTO)
        {
            var productUpdated = new Product(productDTO.Name, productDTO.ShortDescription, productDTO.LongDescription, productDTO.CategoryId, productDTO.Price);

            _repository.ProductRepository.UpdateRecord(id, productUpdated, out string ErrorMessage);
            _repository.ProductRepository.SaveChanges();
            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.ProductRepository.RemoveRecord(id, out bool check);
            if (check == false)
            {
                return NotFound("There is no Product with this ID in Database");
            }
            else
            {
                _repository.ProductRepository.SaveChanges();

                _logger.LogInfo("Delete a category record");

                return Ok("Category deleted from database.");
            }



        }

        [HttpPost("uploadImageWithId/{id}")]
        public IActionResult UploadImage(int id, [FromForm] FileUploadDTO imageToUpload)
        {

            var productObj = _repository.ProductRepository.GetRecordById(id);
            if (productObj == null)
            {
                return NotFound("There is no Product with this ID in Database");
            }
            else
            {
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

        }

        //GET: GETBYID
        [HttpGet("getImageStringWithId/{id}")]
        public IActionResult GetImage(int id)
        {
            var productObj = _repository.ProductRepository.GetRecordById(id);
            if (productObj == null)
            {
                return NotFound("There is no Product with this ID in Database");
            }
            else
            {
                _logger.LogInfo("Get Category records by id");
                return Ok(productObj.Image);
            }

        }



    }
}

