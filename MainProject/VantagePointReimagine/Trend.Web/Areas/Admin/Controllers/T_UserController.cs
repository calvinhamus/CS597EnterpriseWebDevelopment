using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trend.Core.Data;

namespace Trend.Web.Areas.Admin.Controllers
{
    public class T_UserController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_User
        public ActionResult Index()
        {
            var t_User = db.T_User.Include(t => t.T_UserLevel1);
            return View(t_User.ToList());
        }

        // GET: Admin/T_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User t_User = db.T_User.Find(id);
            if (t_User == null)
            {
                return HttpNotFound();
            }
            return View(t_User);
        }

        // GET: Admin/T_User/Create
        public ActionResult Create()
        {
            ViewBag.T_UserLevel = new SelectList(db.T_UserLevel, "Id", "Name");
            return View();
        }

        // POST: Admin/T_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,password,T_UserLevel")] T_User t_User)
        {
            if (ModelState.IsValid)
            {
                db.T_User.Add(t_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_UserLevel = new SelectList(db.T_UserLevel, "Id", "Name", t_User.T_UserLevel);
            return View(t_User);
        }

        // GET: Admin/T_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User t_User = db.T_User.Find(id);
            if (t_User == null)
            {
                return HttpNotFound();
            }
            ViewBag.T_UserLevel = new SelectList(db.T_UserLevel, "Id", "Name", t_User.T_UserLevel);
            return View(t_User);
        }

        // POST: Admin/T_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,password,T_UserLevel")] T_User t_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_UserLevel = new SelectList(db.T_UserLevel, "Id", "Name", t_User.T_UserLevel);
            return View(t_User);
        }

        // GET: Admin/T_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_User t_User = db.T_User.Find(id);
            if (t_User == null)
            {
                return HttpNotFound();
            }
            return View(t_User);
        }

        // POST: Admin/T_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_User t_User = db.T_User.Find(id);
            db.T_User.Remove(t_User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
