using ErxTest.Data;
using ErxTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErxTest.Repositories
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly ApplicationDbContext _AppContext;

        public PersonalInfoRepository(ApplicationDbContext context)
        {
            _AppContext = context;
        }

        public async Task<PersonalInfo> Create(PersonalInfo personalInfo)
        {
            personalInfo.Id = new Guid();
            await _AppContext.Personals.AddAsync(personalInfo);
            await _AppContext.SaveChangesAsync();

            return personalInfo;
        }

        public async Task Delete(string Id)
        {
            var personalInfo = await _AppContext.Personals.FindAsync(Id);

            _AppContext.Personals.Remove(personalInfo);
            await _AppContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonalInfo>> Get()
        {
            return await _AppContext.Personals.ToListAsync();
        }

        public async Task<PersonalInfo> Get(string Id)
        {
            return await _AppContext.Personals.FindAsync(Id);
        }

        public async Task Update(PersonalInfo personalInfo)
        {
            _AppContext.Entry(personalInfo).State = EntityState.Modified;
            await _AppContext.SaveChangesAsync();
        }
    }
}
