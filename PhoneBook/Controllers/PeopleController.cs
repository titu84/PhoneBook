using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Models;
using PhoneBook.Models.Abstraction;
using PhoneBook.Models.Book;

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
                repo.Add(person);
                return RedirectToAction("Index");
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

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: People/Delete/5
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
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
