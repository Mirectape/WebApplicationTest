using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ContextFolder;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {

        public IActionResult AllView()
        {
            ViewBag.Persons = new DataContext().Persons;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult ShowPersonFullData(int ID)
        {
            var persons = new DataContext().Persons.AsQueryable();
            persons = persons.Where((p) => p.ID == ID);
            ViewBag.ChosenPersons = persons;
            return View();
        }

        public IActionResult Index()
        {
            
            Repository repository = new Repository(5);
            List<Person> persons = repository.Persons;

            return View(persons);
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(string firstName, string secondName, string paternalName,
            string phoneNumber, string address, string description)
        {
            using (var db = new DataContext())
            {
                db.Persons.Add(
                    new Person()
                    {
                        FirstName = firstName,
                        SecondName = secondName,
                        PaternalName = paternalName,
                        PhoneNumber = phoneNumber,
                        Address = address,
                        Description = description
                    });

                db.SaveChanges();
            }
            return Redirect("~/");
        }
    }
}
