namespace WebApplication1.Models
{
    public static class PersonFactory
    {
        private static int _id;

        static PersonFactory()
        {
            _id = 0;
        }

        public static Person GetPerson(string firstName, string secondName, string paternalName,
            string phoneNumber, string address, string description)
        {
            return new Person {ID = NextId(), FirstName = firstName, SecondName = secondName, PaternalName = paternalName, 
                PhoneNumber = phoneNumber, Address = address, Description = description };
        }

        private static int NextId()
        {
            _id++;
            return _id;
        }
    }
}
