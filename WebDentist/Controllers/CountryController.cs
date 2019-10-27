using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDentist.Entities;
using WebDentist.ViewModels;

namespace WebDentist.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCountries()
        {
            var model = _context.Countries
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Create([FromBody]CountryAddViewModel model)
        {
            var country = _context.Countries
                .SingleOrDefault(c => c.Name == model.Name);
            if(country!=null)
            {
                return BadRequest(new { invalid = "Така країна уже є в БД!" });
            }
            country = new Country
            {
                Name = model.Name,
                FlagImage = model.FlagImage
            };
            _context.Countries.Add(country);
            _context.SaveChanges();
            return Ok(new CountryViewModel
            {
                Id=country.Id,
                Name=country.Name
            });
        }
    }
}