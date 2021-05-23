using ErxTest.Data;
using ErxTest.Models;
using ErxTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ErxTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController 
    {
        [HttpGet]
        public async Task<IEnumerable<CountryModel>> Get()
        {
            return await Helpers.MyHelp.GetCountryList();
        }
    }
}
