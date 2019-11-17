using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebDentist.Entities;
using WebDentist.Helpers;
using WebDentist.ViewModels;

namespace WebDentist.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;
        public CountryController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public IActionResult GetCountries(string name, string page)
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

            string fileDestDir = _env.ContentRootPath;
            fileDestDir = Path.Combine(fileDestDir, "Bigimot");
            string imageName = Path.GetRandomFileName()+".jpg";

            var bitmap = model.Image.Split(',')[1].FromBase64StringToImage();

            var outbtmp = ImageWorker.CreateImage(bitmap, 100, 100);
            outbtmp.Save(Path.Combine(fileDestDir, imageName), ImageFormat.Jpeg);
            //bitmap.Save(Path.Combine(fileDestDir,imageName), ImageFormat.Jpeg);

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