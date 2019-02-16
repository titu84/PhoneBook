using PhoneBook.Models.Abstraction;
using PhoneBook.Models.Book;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly IPersonRepo repo;
        public PeopleController(IPersonRepo repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            return View(repo.GetPeople().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,EMail,Phone")] Person person)
        {
            if (ModelState.IsValid)
            {
                if (repo.Add(person) > 0)
                {
                    return RedirectToAction("Index");
                }
                throw new Exception("Db Exception");
            }
            return View(person);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = repo.Get((int)id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,EMail,Phone")] Person person)
        {
            if (ModelState.IsValid)
            {
                repo.Update(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = repo.Get((int)id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (repo.Delete(id))
                return RedirectToAction("Index");
            return HttpNotFound();
        }
    }
}
