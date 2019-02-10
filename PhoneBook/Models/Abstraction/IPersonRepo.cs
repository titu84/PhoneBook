using System.Collections.Generic;
using System.Linq;
using PhoneBook.Models.Book;

namespace PhoneBook.Models.Abstraction
{
    public interface IPersonRepo
    {
        int Add(Person item);
        bool Delete(int? id);
        Person Get(int id);
        IQueryable<Person> GetPeople();
        List<Person> GetSamplePeople();
        bool Update(Person item);
    }
}