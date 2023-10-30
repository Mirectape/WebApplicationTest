using WebApplication1.Interfaces;
using WebApplication1.DataContext;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class PersonData : IPersonData
    {
        private readonly PersonDbContext _context;

        public PersonData(PersonDbContext context)
        {
            _context = context;
        }

        public void AddPerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void RemovePerson(Person person)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public void EditPerson(int id, Person updatedPerson) 
        {
            var editPerson = _context.People.Where((p) => p.ID == id).Single();
            editPerson = updatedPerson;
            _context.SaveChanges();
        }

        public IEnumerable<Person> GetPeople() 
        {
            return _context.People;
        }
    }
}
