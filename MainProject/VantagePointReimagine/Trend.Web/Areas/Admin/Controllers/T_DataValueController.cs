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
    public class T_DataValueController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_DataValue
        public ActionResult Index()
        {
            var t_DataValue = db.T_DataValue.Include(t => t.T_DataPoint1);
            return View(t_DataValue.ToList());
        }

        // GET: Admin/T_DataValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_DataValue t_DataValue = db.T_DataValue.Find(id);
            if (t_DataValue == null)
            {
                return HttpNotFound();
            }
            return View(t_DataValue);
        }

        // GET: Admin/T_DataValue/Create
        public ActionResult Create()
        {
            ViewBag.T_DataPoint = new SelectList(db.T_DataPoint, "Id", "Name");
            return View();
        }

        // POST: Admin/T_DataValue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,T_DataPoint,Value,DateTime")] T_DataValue t_DataValue)
        {
            if (ModelState.IsValid)
            {
                db.T_DataValue.Add(t_DataValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_DataPoint = new SelectList(db.T_DataPoint, "Id", "Name", t_DataValue.T_DataPoint);
            return View(t_DataValue);
        }

        // GET: Admin/T_DataValue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_DataValue t_DataValue = db.T_DataValue.Find(id);
            if (t_DataValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.T_DataPoint = new SelectList(db.T_DataPoint, "Id", "Name", t_DataValue.T_DataPoint);
            return View(t_DataValue);
        }

        // POST: Admin/T_DataValue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,T_DataPoint,Value,DateTime")] T_DataValue t_DataValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_DataValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_DataPoint = new SelectList(db.T_DataPoint, "Id", "Name", t_DataValue.T_DataPoint);
            return View(t_DataValue);
        }

        // GET: Admin/T_DataValue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_DataValue t_DataValue = db.T_DataValue.Find(id);
            if (t_DataValue == null)
            {
                return HttpNotFound();
            }
            return View(t_DataValue);
        }

        // POST: Admin/T_DataValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_DataValue t_DataValue = db.T_DataValue.Find(id);
            db.T_DataValue.Remove(t_DataValue);
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
