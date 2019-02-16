using PhoneBook.Models;
using PhoneBook.Models.Book;
using System.Data.Entity;

namespace PhoneBook.Tests.TestHelpers
{
    public class TestAppContext : IPeopleContext
    {
        public TestAppContext()
        {
            People = new TestPersonDbSet();
        }
        public DbSet<Person> People { get; set; }

        public void MarkAsDeleted(Person item)
        {           
        }

        public void MarkAsModified(Person item)
        {           
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
