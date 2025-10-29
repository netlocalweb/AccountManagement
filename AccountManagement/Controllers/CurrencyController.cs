using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository repository;
        private readonly IMapper mapper;

        public CurrencyController(ICurrencyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        //Get all currencies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await repository.GetAllAsync();
            var result = mapper.Map<IEnumerable<CurrencyReadDto>>(currencies);
            return Ok(result);
        }

        //Get currency by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currency = await repository.GetByIdAsync(id);
            if (currency == null) 
                return NotFound();

            var dto = mapper.Map<CurrencyReadDto>(currency);
            return Ok(dto);
        }

        //Create new currency
        [HttpPost]
        public async Task<IActionResult> Create(CurrencyCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Code) || string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("Code and Description are required.");

            if (await repository.GetByCodeAsync(dto.Code.ToUpper()) != null)
                return Conflict("Currency code already exists.");

            var currency = mapper.Map<Currency>(dto);
            currency.Code = dto.Code.ToUpper();

            var created = await repository.AddAsync(currency);
            var readDto = mapper.Map<CurrencyReadDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, readDto);
        }

        // PUT: api/currency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CurrencyUpdateDto dto)
        {
            var currency = await repository.GetByIdAsync(id);
            if (currency == null) 
                return NotFound();

            mapper.Map(dto, currency);

            var updated = await repository.UpdateAsync(currency);
            var readDto = mapper.Map<CurrencyReadDto>(updated);

            return Ok(readDto);
        }
        

        //Delete currency
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);
            if (!deleted) 
                return NotFound();//If not found
            return NoContent();//if found
        }
    };
}

