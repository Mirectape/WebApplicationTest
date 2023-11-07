using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApplication1.AuthPersonApp;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class PersonDataApi : IPersonData
    {
        private HttpClient _httpClient;

        public PersonDataApi()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(@"https://localhost:44390/api/person")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<Person> GetPeople()
        {
            string url = @"https://localhost:44390/api/person";

            string json = _httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
        }

        public void AddPerson(Person person)
        {
            string url = @"https://localhost:44390/api/person";

            var r = _httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        public async void RemovePerson(int id)
        {
            string url = @"https://localhost:44390/api/person";

            var response = await _httpClient.DeleteAsync(
                $"{url}/{id}");
        }

        public async void EditPerson(int id, Person updatedPerson)
        {
            string url = @"https://localhost:44390/api/person";

            var r = await _httpClient.PutAsync(
                $"{url}/{id}",
                new StringContent(JsonConvert.SerializeObject(updatedPerson), 
                Encoding.UTF8, "application/json"));
            r.EnsureSuccessStatusCode();
        }
    }
}
