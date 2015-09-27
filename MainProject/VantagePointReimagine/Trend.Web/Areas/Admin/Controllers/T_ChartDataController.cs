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
    public class T_ChartDataController : BaseAdminController
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_ChartData
        public ActionResult Index()
        {
            var t_ChartData = db.T_ChartData.Include(t => t.T_DataValue).Include(t => t.T_SavedChart);
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
            ViewBag.T_DataValueId = new SelectList(db.T_DataValue, "Id", "Id");
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "Name");
            return View();
        }

        // POST: Admin/T_ChartData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,T_SavedChartId,T_DataValueId")] T_ChartData t_ChartData)
        {
            if (ModelState.IsValid)
            {
                db.T_ChartData.Add(t_ChartData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_DataValueId = new SelectList(db.T_DataValue, "Id", "Id", t_ChartData.T_DataValueId);
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "Name", t_ChartData.T_SavedChartId);
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
            ViewBag.T_DataValueId = new SelectList(db.T_DataValue, "Id", "Id", t_ChartData.T_DataValueId);
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "Name", t_ChartData.T_SavedChartId);
            return View(t_ChartData);
        }

        // POST: Admin/T_ChartData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,T_SavedChartId,T_DataValueId")] T_ChartData t_ChartData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_ChartData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_DataValueId = new SelectList(db.T_DataValue, "Id", "Id", t_ChartData.T_DataValueId);
            ViewBag.T_SavedChartId = new SelectList(db.T_SavedChart, "Id", "Name", t_ChartData.T_SavedChartId);
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
