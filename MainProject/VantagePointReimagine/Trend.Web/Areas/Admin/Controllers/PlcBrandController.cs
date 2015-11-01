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
    public class PlcBrandController : BaseAdminController
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_PlcBrand
        public ActionResult Index()
        {
            return View(db.T_PlcBrand.ToList());
        }

        // GET: Admin/T_PlcBrand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_PlcBrand t_PlcBrand = db.T_PlcBrand.Find(id);
            if (t_PlcBrand == null)
            {
                return HttpNotFound();
            }
            return View(t_PlcBrand);
        }

        // GET: Admin/T_PlcBrand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/T_PlcBrand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] T_PlcBrand t_PlcBrand)
        {
            if (ModelState.IsValid)
            {
                db.T_PlcBrand.Add(t_PlcBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_PlcBrand);
        }

        // GET: Admin/T_PlcBrand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_PlcBrand t_PlcBrand = db.T_PlcBrand.Find(id);
            if (t_PlcBrand == null)
            {
                return HttpNotFound();
            }
            return View(t_PlcBrand);
        }

        // POST: Admin/T_PlcBrand/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] T_PlcBrand t_PlcBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_PlcBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_PlcBrand);
        }

        // GET: Admin/T_PlcBrand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_PlcBrand t_PlcBrand = db.T_PlcBrand.Find(id);
            if (t_PlcBrand == null)
            {
                return HttpNotFound();
            }
            return View(t_PlcBrand);
        }

        // POST: Admin/T_PlcBrand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_PlcBrand t_PlcBrand = db.T_PlcBrand.Find(id);
            db.T_PlcBrand.Remove(t_PlcBrand);
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
