using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trend.Core.Data;
using Trend.Web.Controllers;

namespace Trend.Web.Areas.Admin.Controllers
{
    public class T_DataPointController : BaseAdminController
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_DataPoint
        public ActionResult Index()
        {
            var t_DataPoint = db.T_DataPoint.Include(t => t.T_Plc);
            return View(t_DataPoint.ToList());
        }

        // GET: Admin/T_DataPoint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_DataPoint t_DataPoint = db.T_DataPoint.Find(id);
            if (t_DataPoint == null)
            {
                return HttpNotFound();
            }
            return View(t_DataPoint);
        }

        // GET: Admin/T_DataPoint/Create
        public ActionResult Create()
        {
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name");
            return View();
        }

        // POST: Admin/T_DataPoint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,T_PlcId")] T_DataPoint t_DataPoint)
        {
            if (ModelState.IsValid)
            {
                db.T_DataPoint.Add(t_DataPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name", t_DataPoint.T_PlcId);
            return View(t_DataPoint);
        }

        // GET: Admin/T_DataPoint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_DataPoint t_DataPoint = db.T_DataPoint.Find(id);
            if (t_DataPoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name", t_DataPoint.T_PlcId);
            return View(t_DataPoint);
        }

        // POST: Admin/T_DataPoint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,T_PlcId")] T_DataPoint t_DataPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_DataPoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name", t_DataPoint.T_PlcId);
            return View(t_DataPoint);
        }

        // GET: Admin/T_DataPoint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_DataPoint t_DataPoint = db.T_DataPoint.Find(id);
            if (t_DataPoint == null)
            {
                return HttpNotFound();
            }
            return View(t_DataPoint);
        }

        // POST: Admin/T_DataPoint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_DataPoint t_DataPoint = db.T_DataPoint.Find(id);
            db.T_DataPoint.Remove(t_DataPoint);
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
