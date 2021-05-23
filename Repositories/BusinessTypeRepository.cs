using ErxTest.Data;
using ErxTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErxTest.Repositories
{
    public class BusinessTypeRepository : IBusinessTypeRepository
    {
        private readonly ApplicationDbContext _AppContext;

        public BusinessTypeRepository(ApplicationDbContext context)
        {
            _AppContext = context;
        }

        public async Task<BusinessType> Create(BusinessType businessType)
        {
            await _AppContext.BusinessTypes.AddAsync(businessType);
            await _AppContext.SaveChangesAsync();

            return businessType;
        }

        public async Task Delete(int Id)
        {
            var businessType = await _AppContext.BusinessTypes.FindAsync(Id);

            _AppContext.BusinessTypes.Remove(businessType);

            await _AppContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessType>> Get()
        {
            return await _AppContext.BusinessTypes.ToListAsync();
        }

        public async Task<BusinessType> Get(int Id)
        {
            return await _AppContext.BusinessTypes.FindAsync(Id);
        }

        public async Task Update(BusinessType businessType)
        {
            _AppContext.Entry(businessType).State = EntityState.Modified;

            await _AppContext.SaveChangesAsync();
        }
    }
}
