using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;

namespace Eindwerk2018.Controllers
{
    public class UserController : Controller
    {
        private Db_User dbUsers = new Db_User();

        // GET: 
        public ActionResult Index()
        {
            var viewModel = dbUsers.List();
            return View(viewModel);
        }

        // GET: USer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Odf odf = odfLijst.Find(x => x.Id.Equals(id));
            User user = dbUsers.Get((int)id);
            if (user == null) return HttpNotFound();

            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,UserName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                dbUsers.Add(user);
                return RedirectToAction("Index");
                //return RedirectToAction("User", "Details", user.Id); //should let add return id
            }

            return View(user);
        }

        // GET: user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = dbUsers.Get((int)id);

            if (user == null) return HttpNotFound();
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                dbUsers.Edit(user);
                return RedirectToAction("User", "Details",user.Id);
            }

            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = dbUsers.Get((int)id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get info
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //delete
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
