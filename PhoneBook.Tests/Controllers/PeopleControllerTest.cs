using Moq;
using NUnit.Framework;
using PhoneBook.Controllers;
using PhoneBook.Models.Abstraction;
using PhoneBook.Models.Book;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PhoneBook.Tests.Controllers
{
    [TestFixture]
    public class PeopleControllerTest
    {
        private IPersonRepo repo;
        private PeopleController controller;
        private List<Person> mockList = new List<Person>
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
            var repoMock = new Mock<IPersonRepo>();
            repoMock.Setup(s => s.Get(1)).Returns(mockList.Single(p => p.Id == 1));
            repoMock.Setup(s => s.Get(2)).Returns(mockList.Single(p => p.Id == 2));
            repoMock.Setup(s => s.Get(-1)).Returns((Person)null);
            repoMock.Setup(s => s.GetPeople()).Returns(mockList.AsQueryable());
            repoMock.Setup(s => s.Add(It.IsAny<Person>())).Callback<Person>(s => mockList.Add(s)).Returns(mockList.Last().Id);
            repoMock.Setup(s => s.Add(null)).Returns(-1);
            repoMock.Setup(s => s.Update(It.IsAny<Person>())).Returns(true);            
            repoMock.Setup(s => s.Delete(2)).Returns(true);            
            repoMock.Setup(s => s.Delete(-2)).Returns(false);
            repo = repoMock.Object;
            controller = new PeopleController(repo);
        }
        [Test]
        public void Index_ResultsIsNotNull()
        {
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [Test]
        public void Create_ResultsIsNotNull()
        {
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]      
        public void Create_ResultsIsAddedAndNotNull()
        {
            Assert.AreEqual(5, mockList.Count);
            var result = controller.Create(mockList.Last()) as RedirectToRouteResult;
            Assert.AreEqual(6, mockList.Count);
            Assert.AreEqual(result.RouteValues["action"], "Index");
            mockList.Remove(mockList.Last());
        }
        [Test]
        public void Create_ResultsIsNotAddedAndNotNull()
        {
            Assert.AreEqual(5, mockList.Count);
            controller.ModelState.AddModelError("Name", "error");
            var result = controller.Create(mockList.Last()) as ViewResult;
            Assert.AreEqual(5, mockList.Count);
            Assert.IsNotNull(result);            
        }

        [Test]
        public void Create_ThrowException()
        {                
            Assert.That(() => controller.Create(null), Throws.Exception);
        }

        [Test]
        [TestCase(null)]
        public void Edit_ResultsIsBadRequest(int? id)
        {
            var result = controller.Edit(id);
            Assert.IsInstanceOf<HttpStatusCodeResult>(result);
        }

        [Test]
        [TestCase(-1)]
        public void Edit_ResultsHttpNotFound(int? id)
        {
            var result = controller.Edit(id);
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        [TestCase(1)]
        public void Edit_ResultsIdNotNull(int? id)
        {
            var result = controller.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
        }       

        [Test]
        [TestCase(2)]
        public void Delete_ResultsIsNotNull(int? id)
        {
            var result = controller.Delete(id) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase(-1)]
        public void Delete_ResultsHttpNotFound(int? id)
        {
            var result = controller.Delete(id);
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        [TestCase(null)]
        public void Delete_ResultsIsBadRequest(int? id)
        {
            var result = controller.Delete(id);
            Assert.IsInstanceOf<HttpStatusCodeResult>(result);
        }
        [Test]
        [TestCase(2)]
        public void DeleteConfirmed_ResultsRedirectToIndex(int id)
        {
            var result = controller.DeleteConfirmed(id) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }
        [Test]
        [TestCase(-2)]
        public void DeleteConfirmed_ResultsDeletingItem(int id)
        {
            var result = controller.DeleteConfirmed(id);
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }
    }
}
