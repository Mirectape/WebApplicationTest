using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ContextFolder;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;

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

        public IActionResult DeleteData(int ID)
        {
            using (var db = new DataContext())
            {
                var deletePerson = db.Persons.Where((p) => p.ID == ID).Single();
                db.Persons.Remove(deletePerson);
                db.SaveChanges();
            }
            return Redirect("~/");
        }

        public IActionResult Edit(int ID)
        {
            var persons = new DataContext().Persons.AsQueryable();
            var editPerson = persons.Where((p) => p.ID == ID).Single();
            ViewBag.ChosenPersons = editPerson;
            return View(editPerson);
        }

        public IActionResult EditPerson(int ID, string firstName, string secondName, string paternalName,
            string phoneNumber, string address, string description)
        {
            using (var db = new DataContext())
            {
                var editPerson = db.Persons.Where((p) => p.ID == ID).Single();
                editPerson.FirstName = firstName;   
                editPerson.SecondName = secondName;
                editPerson.PhoneNumber = phoneNumber;
                editPerson.Address = address;
                editPerson.Description = description;

                db.SaveChanges();
            }
            return Redirect("~/");
        }
    }
}
