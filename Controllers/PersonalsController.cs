using ErxTest.Models;
using ErxTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErxTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalsController : ControllerBase
    {
        private readonly IPersonalInfoRepository _PersonalInfoRepository;
        // GET: PersonalsController
        public PersonalsController(IPersonalInfoRepository personalInfoRepository)
        {
            _PersonalInfoRepository = personalInfoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonalInfo>> GetPersonals()
        {
            return await _PersonalInfoRepository.Get();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PersonalInfo>> GetPersonals(string Id)
        {
            return await _PersonalInfoRepository.Get(Id);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalInfo>> PostPersonals([FromBody] PersonalInfo personalInfo)
        {
            var newPersonalInfo = await _PersonalInfoRepository.Create(personalInfo);
            return CreatedAtAction(nameof(GetPersonals), new { Id = personalInfo.Id }, newPersonalInfo);
        }

        [HttpPut]
        public async Task<ActionResult> PutPersonals(string Id, [FromBody] PersonalInfo personalInfo)
        {
            if (Id != personalInfo.Id.ToString())
            {
                return BadRequest();
            }

            await _PersonalInfoRepository.Update(personalInfo);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            var personalInfo = await _PersonalInfoRepository.Get(Id);
            if (personalInfo == null)
                return NotFound();

            await _PersonalInfoRepository.Delete(personalInfo.Id.ToString());

            return NoContent();
        }
    }
}
