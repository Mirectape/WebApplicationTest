using Newtonsoft.Json;
using System.Text;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class PersonDataApi : IPersonData
    {
        private HttpClient httpClient { get; set; }

        public PersonDataApi()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<Person> GetPeople()
        {
            string url = @"https://localhost:44390/api/person";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
        }

        public void AddPerson(Person person)
        {
            string url = @"https://localhost:44390/api/person";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        public void RemovePerson(Person person)
        {
            string url = @"https://localhost:44390/api/person";

            var r = httpClient.DeleteAsync(
                requestUri: url, CancellationToken.None).Result;
        }

        public void EditPerson(int id, Person updatedPerson)
        {
            string url = @"https://localhost:44390/api/person";

            var r = httpClient.PutAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(updatedPerson), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
    }
}
