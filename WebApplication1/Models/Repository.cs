using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Repository
    {
        static Random randomizer;

        static Repository()
        {
            randomizer = new Random();
        }

        private string _pathToClients;
        private string _pathToGenericFirstNames;
        private string _pathToGenericSecondNames;
        private string _pathToGenericPathernalNames;

        /// <summary>
        /// Random collection of first names from the internal data base
        /// </summary>
        private List<string> _firstNames;

        /// <summary>
        /// Random collection of second names from the internal data base
        /// </summary>
        private List<string> _secondNames;

        /// <summary>
        /// Random collection of paternal names from the internal data base
        /// </summary>
        private List<string> _paternalNames;

        private List<Person> _persons;
        public List<Person> Persons
        {
            get => _persons;
        }

        public Repository(int count)
        {
            _pathToClients = "C:/Users/user/source/repos/WebApplicationTest/TelephoneBook.json";
            _pathToGenericFirstNames = "C:/Users/user/source/repos/WebApplicationTest/GenericFirstNames.json";
            _pathToGenericSecondNames = "C:/Users/user/source/repos/WebApplicationTest/GenericSecondNames.json";
            _pathToGenericPathernalNames = "C:/Users/user/source/repos/WebApplicationTest/GenericPaternalNames.json";

            _persons = new List<Person>();

            if (!File.Exists(_pathToClients))
            {
                GeneratePeople(count);
            }
            else
            {
                _persons = this.LoadData<List<Person>>(_pathToClients);
            }
        }

        private T LoadData<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"{path} doesn't exist");
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                    return data;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed load data due to: {e.Message}");
                    throw e;
                }
            }
        }

        private void SaveData<T>(T data, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }

        public void SaveChanges() => SaveData<List<Person>>(_persons, _pathToClients);

        private void GeneratePeople(int count)
        {
            _firstNames = LoadData<List<string>>(_pathToGenericFirstNames);
            _secondNames = LoadData<List<string>>(_pathToGenericSecondNames);
            _paternalNames = LoadData<List<string>>(_pathToGenericPathernalNames);

            for (int i = 0; i < count; i++)
            {
                _persons.Add(PersonFactory.GetPerson(
                    _firstNames[Repository.randomizer.Next(_firstNames.Count())],
                    _secondNames[Repository.randomizer.Next(_secondNames.Count())],
                    _paternalNames[Repository.randomizer.Next(_paternalNames.Count())],
                    ((long)(Repository.randomizer.Next(1000000000, 1111111111)) * (long)(Repository.randomizer.Next(13, 20))).ToString(),
                    ((long)(Repository.randomizer.Next(1000000000, 1111111111)) * (long)(Repository.randomizer.Next(3, 10))).ToString(),
                    ((long)(Repository.randomizer.Next(1000000000, 1111111111)) * (long)(Repository.randomizer.Next(3, 10))).ToString()
                    ));
            }
            this.SaveData(_persons, _pathToClients);
        }
    }
}
