using ErxTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErxTest.Repositories
{
    public interface IPersonalInfoRepository
    {
        Task<IEnumerable<PersonalInfo>> Get();

        Task<PersonalInfo> Get(string Id);

        Task<PersonalInfo> Create(PersonalInfo personalInfo);

        Task Update(PersonalInfo personalInfo);

        Task Delete(string Id);
    }
}
