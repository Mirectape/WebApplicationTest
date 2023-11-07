using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IPersonData
    {
        IEnumerable<Person> GetPeople();
        void AddPerson(Person person);
        void RemovePerson(int id);

        /// <summary>
        /// To what id we put an updated person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPerson"></param>
        void EditPerson(int id, Person updatedPerson);
    }
}
