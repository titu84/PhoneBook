using PhoneBook.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PhoneBook.Models.Repos
{
    public class PersonRepo
    {
        private readonly ApplicationDbContext context;
        private readonly Logger.Abstraction.Logger errorLogger;

        public PersonRepo(ApplicationDbContext context, Logger.Abstraction.Logger errorLogger)
        {
            this.context = context;
            this.errorLogger = errorLogger;
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
            return context.People.SingleOrDefault(p=>p.Id == id);
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
            catch (Exception ex)
            {
                errorLogger.Log(ex.Message);
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
                context.Entry(dbItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorLogger.Log(ex.Message);
                return false;
            }
        }
        public bool Delete(int? id)
        {
            try
            {
                var dbItem = context.People.Find(id);
                context.People.Remove(dbItem);
                context.Entry(dbItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorLogger.Log(ex.Message);
                return false;
            }
        }

    }
}