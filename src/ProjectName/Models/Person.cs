namespace ProjectName.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Query
    {
        public IEnumerable<Person> GetPeople() => new List<Person> {
                new Person {FirstName = "John", LastName = "Doe"},
                 new Person {FirstName = "Jane", LastName = "Doe"},
            };


    }
}