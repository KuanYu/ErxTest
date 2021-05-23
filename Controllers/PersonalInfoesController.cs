using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErxTest.Data;
using ErxTest.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.IO;
using ErxTest.Repositories;

namespace ErxTest.Controllers 
{
    public class PersonalInfoesController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public PersonalInfoesController(ApplicationDbContext context)
        {
            _Context = context;
        }

        // GET: PersonalInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _Context.Personals.ToListAsync());
        }

        // GET: PersonalInfoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInfo = await _Context.Personals.FirstOrDefaultAsync(m => m.Id == id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            return View(personalInfo);
        }

        public async Task<IEnumerable<SelectListItem>> GetBusinessTypes()
        {
            List<BusinessType> businessTypeModel = await _Context.BusinessTypes.OrderBy(x => x.Name).ToListAsync();

            // Set the List Item with the countries.
            IEnumerable<SelectListItem> businessTypes = businessTypeModel.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });

            // Create a ViewBag property with the final content.
            return businessTypes;
        }

        // GET: PersonalInfoes/Create
        public async Task<IActionResult> Create()
        {
            // Create a ViewBag property with the final content.
            ViewBag.BusinessTypes = await GetBusinessTypes();

            // Create a ViewBag property with the final content.
            ViewBag.Countries = await GetCountryList();

            return View();
        }

        public async Task<IEnumerable<SelectListItem>> GetCountryList()
        {
            List<CountryModel> countryModel = await Helpers.MyHelp.GetCountryList();

            return countryModel.Select(x => new SelectListItem() { Value = x.name, Text = x.name });
        }

        // POST: PersonalInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, FirstName, LastName, BirthDate, Country, AddressHome, AddressWork, BusinessTypeId, Occupation")] PersonalInfo personalInfo)
        {
            bool valid = true;

            //Check duplicate
            PersonalInfo dupplicate = await _Context.Personals.Where(x => x.FirstName.ToLower() == personalInfo.FirstName.ToLower().Trim() && x.LastName.ToLower() == personalInfo.LastName.ToLower().Trim()).FirstOrDefaultAsync();
            if (dupplicate != null)
            {
                ModelState.AddModelError("FirstName", "Existing in Database");
                ModelState.AddModelError("LastName", "Existing in Database");

                valid = false;
            }

            if (ModelState.IsValid && valid)
            {
                personalInfo.Id = Guid.NewGuid();
                personalInfo.FirstName = personalInfo.FirstName.Trim();
                personalInfo.LastName = personalInfo.LastName.Trim();

                _Context.Add(personalInfo);

                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(personalInfo);
        }

        // GET: PersonalInfoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInfo = await _Context.Personals.FindAsync(id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            ViewBag.BusinessTypes = await GetBusinessTypes();

            IEnumerable<SelectListItem> countries = await GetCountryList();
            foreach (SelectListItem item in countries)
            {
                item.Selected = item.Text == personalInfo.Country || item.Value == personalInfo.Country;
            }

            // Create a ViewBag property with the final content.
            ViewBag.Countries = countries;

            return View(personalInfo);
        }

        // POST: PersonalInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Title, FirstName, LastName, BirthDate, Country, AddressHome, AddressWork, BusinessTypeId, Occupation")] PersonalInfo personalInfo)
        {
            if (id != personalInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(personalInfo);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalInfoExists(personalInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(personalInfo);
        }

        // GET: PersonalInfoes/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInfo = await _Context.Personals.FirstOrDefaultAsync(m => m.Id == id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            return View(personalInfo);
        }

        // POST: PersonalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personalInfo = await _Context.Personals.FindAsync(id);
            _Context.Personals.Remove(personalInfo);

            await _Context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInfoExists(Guid id)
        {
            return _Context.Personals.Any(e => e.Id == id);
        }
    }
}
