using PhoneBook.Models.Book;
using System;
using System.Linq;

namespace PhoneBook.Tests.TestHelpers
{
    public class TestPersonDbSet: TestDbSet<Person>
    {
        public override Person Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }
    }
}
