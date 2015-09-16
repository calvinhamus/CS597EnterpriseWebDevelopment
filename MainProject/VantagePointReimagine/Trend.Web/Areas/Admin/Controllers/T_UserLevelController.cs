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
    public class T_UserLevelController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_UserLevel
        public ActionResult Index()
        {
            return View(db.T_UserLevel.ToList());
        }

        // GET: Admin/T_UserLevel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_UserLevel t_UserLevel = db.T_UserLevel.Find(id);
            if (t_UserLevel == null)
            {
                return HttpNotFound();
            }
            return View(t_UserLevel);
        }

        // GET: Admin/T_UserLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/T_UserLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] T_UserLevel t_UserLevel)
        {
            if (ModelState.IsValid)
            {
                db.T_UserLevel.Add(t_UserLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_UserLevel);
        }

        // GET: Admin/T_UserLevel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_UserLevel t_UserLevel = db.T_UserLevel.Find(id);
            if (t_UserLevel == null)
            {
                return HttpNotFound();
            }
            return View(t_UserLevel);
        }

        // POST: Admin/T_UserLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] T_UserLevel t_UserLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_UserLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_UserLevel);
        }

        // GET: Admin/T_UserLevel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_UserLevel t_UserLevel = db.T_UserLevel.Find(id);
            if (t_UserLevel == null)
            {
                return HttpNotFound();
            }
            return View(t_UserLevel);
        }

        // POST: Admin/T_UserLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_UserLevel t_UserLevel = db.T_UserLevel.Find(id);
            db.T_UserLevel.Remove(t_UserLevel);
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
