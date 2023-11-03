using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.AuthPersonApp;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonData _personData;
        // private readonly ILogger _log;

        public HomeController(IPersonData personData)
        {
            _personData = personData;
            //_log = log;
        }

        [AllowAnonymous]
        public IActionResult AllView()
        {
            // _log.LogCritical("Everything went fine");
            ViewBag.Persons = _personData.GetPeople();
            return View(_personData.GetPeople());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(string firstName, string secondName, string paternalName,
            string phoneNumber, string address, string description)
        {
            var person = new Person()
            {
                FirstName = firstName,
                SecondName = secondName,
                PaternalName = paternalName,
                PhoneNumber = phoneNumber,
                Address = address,
                Description = description
            };

            _personData.AddPerson(person);
            return Redirect("~/");
        }

        public IActionResult PersonFullData(int ID)
        {
            var persons = _personData.GetPeople().AsQueryable();
            persons = persons.Where((p) => p.ID == ID);
            ViewBag.ChosenPersons = persons;
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult DeletePerson(int ID)
        {
            var deletePerson = _personData.GetPeople().Where((p) => p.ID == ID).Single();
            _personData.RemovePerson(deletePerson);
            return Redirect("~/");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int ID)
        {
            var persons = _personData.GetPeople().AsQueryable();
            var editPerson = persons.Where((p) => p.ID == ID).Single();
            ViewBag.ChosenPersons = editPerson;
            return View(editPerson);
        }

        public IActionResult EditPerson(int ID, string firstName, string secondName, string paternalName,
            string phoneNumber, string address, string description)
        {
            var editPerson = _personData.GetPeople().Where((p) => p.ID == ID).Single();
            editPerson.FirstName = firstName;
            editPerson.SecondName = secondName;
            editPerson.PaternalName = paternalName;
            editPerson.PhoneNumber = phoneNumber;
            editPerson.Address = address;
            editPerson.Description = description;

            _personData.EditPerson(ID, editPerson);

            return Redirect("~/");
        }
    }
}
