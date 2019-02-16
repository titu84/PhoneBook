using PhoneBook.Models.Abstraction;
using PhoneBook.Models.Book;
using System.Collections.Generic;
using System.Linq;


namespace PhoneBook.Models.Repos
{
    public class PersonRepo : IPersonRepo
    {
        private readonly IPeopleContext context;

        public PersonRepo(IPeopleContext context)
        {
            this.context = context;           
        }
        public List<Person> GetSamplePeople()
        {
            List<Person> result = new List<Person>
            {
                new Person { Id = 1, Name = "Roman", LastName = "Kowalski", EMail = "roman.kowalski@titu84.pl", Phone = 111_222_333 },
                new Person { Id = 2, Name = "Czesław", LastName = "Staszek", EMail = "czeslaw.staszek@titu84.pl", Phone = 444_222_333 },
                new Person { Id = 3, Name = "Katarzyna", LastName = "Mikusiowa", EMail = "katarzyna.mikusiowa@titu84.pl", Phone = 555_222_333 },
                new Person { Id = 4, Name = "Krzysztof", LastName = "Kowalski", EMail = "krzysztof.kowalski@titu84.pl", Phone = 111_222_666 },
                new Person { Id = 5, Name = "Roman", LastName = "Nowak", EMail = "roman.nowak@titu84.pl", Phone = 777_999_888 }
            };
            return result;
        }
        public Person Get(int id)
        {
            return context.People.SingleOrDefault(p => p.Id == id);
        }
        public IQueryable<Person> GetPeople()
        {
            return context.People;
        }
        public int Add(Person item)
        {
            try
            {
                context.People.Add(item);
                context.SaveChanges();
                return item.Id;
            }
            catch 
            {
                return -1;
            }
        }
        public bool Update(Person item)
        {
            try
            {
                var dbItem = context.People.Find(item.Id);
                dbItem.Name = item.Name;
                dbItem.LastName = item.LastName;
                dbItem.EMail = item.EMail;
                dbItem.Phone = item.Phone;
                context.MarkAsModified(dbItem);
                context.SaveChanges();
                return true;
            }
            catch
            {                
                return false;
            }
        }
        public bool Delete(int? id)
        {
            try
            {
                var dbItem = context.People.Find(id);
                if (dbItem == null)                
                    return false;                
                context.People.Remove(dbItem);
                context.MarkAsDeleted(dbItem);
                context.SaveChanges();
                return true;
            }
            catch 
            {              
                return false;
            }
        }
    }
}