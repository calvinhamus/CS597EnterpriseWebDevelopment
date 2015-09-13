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
    public class T_SavedChartController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_SavedChart
        public ActionResult Index()
        {
            var t_SavedChart = db.T_SavedChart.Include(t => t.T_User);
            return View(t_SavedChart.ToList());
        }

        // GET: Admin/T_SavedChart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_SavedChart t_SavedChart = db.T_SavedChart.Find(id);
            if (t_SavedChart == null)
            {
                return HttpNotFound();
            }
            return View(t_SavedChart);
        }

        // GET: Admin/T_SavedChart/Create
        public ActionResult Create()
        {
            ViewBag.T_UserId = new SelectList(db.T_User, "Id", "username");
            return View();
        }

        // POST: Admin/T_SavedChart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,T_UserId,Name,Created,Updated")] T_SavedChart t_SavedChart)
        {
            if (ModelState.IsValid)
            {
                db.T_SavedChart.Add(t_SavedChart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_UserId = new SelectList(db.T_User, "Id", "username", t_SavedChart.T_UserId);
            return View(t_SavedChart);
        }

        // GET: Admin/T_SavedChart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_SavedChart t_SavedChart = db.T_SavedChart.Find(id);
            if (t_SavedChart == null)
            {
                return HttpNotFound();
            }
            ViewBag.T_UserId = new SelectList(db.T_User, "Id", "username", t_SavedChart.T_UserId);
            return View(t_SavedChart);
        }

        // POST: Admin/T_SavedChart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,T_UserId,Name,Created,Updated")] T_SavedChart t_SavedChart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_SavedChart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_UserId = new SelectList(db.T_User, "Id", "username", t_SavedChart.T_UserId);
            return View(t_SavedChart);
        }

        // GET: Admin/T_SavedChart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_SavedChart t_SavedChart = db.T_SavedChart.Find(id);
            if (t_SavedChart == null)
            {
                return HttpNotFound();
            }
            return View(t_SavedChart);
        }

        // POST: Admin/T_SavedChart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_SavedChart t_SavedChart = db.T_SavedChart.Find(id);
            db.T_SavedChart.Remove(t_SavedChart);
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
