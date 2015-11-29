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
    public class T_ChartDataController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_ChartData
        public ActionResult Index()
        {
            var t_ChartData = db.T_ChartData.Include(t => t.T_DataPoint).Include(t => t.T_SavedChart);
            return View(t_ChartData.ToList());
        }

        // GET: Admin/T_ChartData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_ChartData t_ChartData = db.T_ChartData.Find(id);
            if (t_ChartData == null)
            {
                return HttpNotFound();
            }
            return View(t_ChartData);
        }

        // GET: Admin/T_ChartData/Create
        public ActionResult Create()
        {
            ViewBag.T_DataPointId = new SelectList(db.T_DataPoint, "Id", "Name");
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "T_UserId");
            return View();
        }

        // POST: Admin/T_ChartData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,T_SavedChartId,T_DataPointId")] T_ChartData t_ChartData)
        {
            if (ModelState.IsValid)
            {
                db.T_ChartData.Add(t_ChartData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_DataPointId = new SelectList(db.T_DataPoint, "Id", "Name", t_ChartData.T_DataPointId);
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "T_UserId", t_ChartData.T_SavedChartId);
            return View(t_ChartData);
        }

        // GET: Admin/T_ChartData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_ChartData t_ChartData = db.T_ChartData.Find(id);
            if (t_ChartData == null)
            {
                return HttpNotFound();
            }
            ViewBag.T_DataPointId = new SelectList(db.T_DataPoint, "Id", "Name", t_ChartData.T_DataPointId);
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "T_UserId", t_ChartData.T_SavedChartId);
            return View(t_ChartData);
        }

        // POST: Admin/T_ChartData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,T_SavedChartId,T_DataPointId")] T_ChartData t_ChartData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_ChartData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_DataPointId = new SelectList(db.T_DataPoint, "Id", "Name", t_ChartData.T_DataPointId);
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "T_UserId", t_ChartData.T_SavedChartId);
            return View(t_ChartData);
        }

        // GET: Admin/T_ChartData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_ChartData t_ChartData = db.T_ChartData.Find(id);
            if (t_ChartData == null)
            {
                return HttpNotFound();
            }
            return View(t_ChartData);
        }

        // POST: Admin/T_ChartData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_ChartData t_ChartData = db.T_ChartData.Find(id);
            db.T_ChartData.Remove(t_ChartData);
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
