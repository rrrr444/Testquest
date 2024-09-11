using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var persons = _context.Persons.ToList();
            return View(persons);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = false,
                }))
                {
                    var records = new List<Person>();
                    while (csv.Read())
                    {
                        var record = new Person
                        {
                            
                            Name = csv.GetField<string>(0),
                            Age = csv.GetField<int>(1),
                            Email = csv.GetField<string>(2)
                        };

                        
                        if (!_context.Persons.Any(p => p.Id == record.Id))
                        {
                            records.Add(record);
                        }
                        else
                        {
                            
                        }
                    }

                    _context.Persons.AddRange(records);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View();
        }

    }
}

