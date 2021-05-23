using ErxTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErxTest.Repositories
{
    public interface IBusinessTypeRepository
    {
        Task<IEnumerable<BusinessType>> Get();

        Task<BusinessType> Get(int Id);

        Task<BusinessType> Create(BusinessType businessType);

        Task Update(BusinessType businessType);

        Task Delete(int Id);
    }
}
