using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Data
{
    class PersonDataApi
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
    }
}
