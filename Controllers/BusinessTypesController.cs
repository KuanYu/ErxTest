using ErxTest.Models;
using ErxTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErxTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessTypesController : ControllerBase
    {
        private readonly IBusinessTypeRepository _BusinessTypeRepository;

        public BusinessTypesController(IBusinessTypeRepository businessTypeRepository)
        {
            _BusinessTypeRepository = businessTypeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<BusinessType>> GetBusinessTypes()
        {
            return await _BusinessTypeRepository.Get();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BusinessType>> GetBusinessTypes(int Id)
        {
            return await _BusinessTypeRepository.Get(Id);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessType>> PostBusinessTypes([FromBody] BusinessType businessType)
        {
            var newBusinessType = await _BusinessTypeRepository.Create(businessType);

            return CreatedAtAction(nameof(GetBusinessTypes), new { Id = businessType.Id }, newBusinessType);
        }

        [HttpPut]
        public async Task<ActionResult> PutBusinessTypes(int Id, string name)
        {
            BusinessType businessType = await _BusinessTypeRepository.Get(Id);

            if (Id != businessType.Id)
            {
                return BadRequest();
            }

            businessType.Name = name;

            await _BusinessTypeRepository.Update(businessType);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var businessType = await _BusinessTypeRepository.Get(Id);
            if (businessType == null)
                return NotFound();

            await _BusinessTypeRepository.Delete(businessType.Id);
            return NoContent();
        }

    }
}
