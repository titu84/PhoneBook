using Moq;
using NUnit.Framework;
using PhoneBook.Models;
using PhoneBook.Models.Book;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.Models.Repos;
using PhoneBook.Tests.TestHelpers;

namespace PhoneBook.Tests.Models.Repos
{
    [TestFixture]
    public class PersonRepoTests
    {
        private IPeopleContext context;        
        private PersonRepo repo;
        List<Person> mockList = new List<Person>
            {
                new Person { Id = 1, Name = "Roman", LastName = "Kowalski", EMail = "roman.kowalski@titu84.pl", Phone = 111_222_333 },
                new Person { Id = 2, Name = "Czesław", LastName = "Staszek", EMail = "czeslaw.staszek@titu84.pl", Phone = 444_222_333 },
                new Person { Id = 3, Name = "Katarzyna", LastName = "Mikusiowa", EMail = "katarzyna.mikusiowa@titu84.pl", Phone = 555_222_333 },
                new Person { Id = 4, Name = "Krzysztof", LastName = "Kowalski", EMail = "krzysztof.kowalski@titu84.pl", Phone = 111_222_666 },
                new Person { Id = 5, Name = "Roman", LastName = "Nowak", EMail = "roman.nowak@titu84.pl", Phone = 777_999_888 }
            };
        [SetUp]
        public void Init()
        {     
            var queryablePeople = mockList.AsQueryable();
            Mock<DbSet<Person>> mockPeopleDbSet = new Mock<DbSet<Person>>();

            mockPeopleDbSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(queryablePeople.Provider);
            mockPeopleDbSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(queryablePeople.Expression);
            mockPeopleDbSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(queryablePeople.ElementType);
            mockPeopleDbSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(queryablePeople.GetEnumerator);

            mockPeopleDbSet.Setup(s => s.Add(It.IsAny<Person>())).Callback((Person person) => mockList.Add(person));
            mockPeopleDbSet.Setup(s => s.Add((Person)null)).Throws(new System.Exception());
            mockPeopleDbSet.Setup(s => s.Remove(It.IsAny<Person>())).Callback((Person person) => mockList.Remove(person));
            mockPeopleDbSet.Setup(s => s.Find(1)).Returns(mockList.Find(p => p.Id == 1));
            mockPeopleDbSet.Setup(s => s.Find(2)).Returns(mockList.Find(p => p.Id == 2));
            mockPeopleDbSet.Setup(s => s.Find(3)).Returns(mockList.Find(p => p.Id == 3));
            mockPeopleDbSet.Setup(s => s.Find(4)).Returns(mockList.Find(p => p.Id == 4));
            mockPeopleDbSet.Setup(s => s.Find(5)).Returns(mockList.Find(p => p.Id == 5));

            var mockContext = new Mock<TestAppContext>();    
            context = mockContext.Object;
            context.People = mockPeopleDbSet.Object;
            
            repo = new PersonRepo(context);           
        }
        [Test]
        public void GetSamplePeople_ResultsList()
        {
            var result = repo.GetSamplePeople();
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Roman", result.First(a => a.Id == 1).Name);
            Assert.AreEqual("Czesław", result.First(a => a.Id == 2).Name);
            Assert.AreEqual("Katarzyna", result.First(a => a.Id == 3).Name);
            Assert.AreEqual("Krzysztof", result.First(a => a.Id == 4).Name);
            Assert.AreEqual("Roman", result.First(a => a.Id == 5).Name);
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]      
        public void Get_ResultsPerson(int id)
        {
            var result = repo.Get(id);
            Assert.AreEqual(mockList.First(a => a.Id == id), result);
        }

        [Test]
        public void Add_ReturnsMinusOne()
        {
            var result = repo.Add(null);
            Assert.AreEqual(-1, result);
        }
        [Test]
        public void Add_ReturnsNewId()
        {
            var item = new Person { Id = 6, Name = "Maria", LastName = "Nowak", EMail = "maria.nowak@titu84.pl", Phone = 999_000_111 };
            var result = repo.Add(item);
            Assert.AreNotEqual(-1, result);
            Assert.AreEqual(mockList.Last().Id, result);
        }
        [Test]
        public void Update_ReturnsFalse()
        {
            var result = repo.Update(null);
            Assert.IsFalse(result);
        }
        [Test]
        public void Update_ReturnsTrue()
        {
            var name = mockList.First(a => a.Id == 5).Name;
            var item = new Person { Id = 5, Name = "Maria", LastName = "Nowak", EMail = "maria.nowak@titu84.pl", Phone = 999_000_111 };
            var result = repo.Update(item);
            Assert.AreNotEqual(name, mockList.First(a => a.Id == 5).Name);
            Assert.IsTrue(result);
        }
        [Test]
        [TestCase(-1)]
        [TestCase(24)]
        [TestCase(null)]
        public void Delete_ReturnsFalse(int? id)
        {
            var result = repo.Delete(id);
            Assert.IsFalse(result);
        }
        [Test]       
        [TestCase(3)]
        [TestCase(4)]       
        public void Delete_ReturnsTrue(int? id)
        {
            var result = repo.Delete(id);
            Assert.IsTrue(result);
        }
    }
}
