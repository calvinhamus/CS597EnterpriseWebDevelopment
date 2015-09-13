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
    public class T_PlcController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_Plc
        public ActionResult Index()
        {
            return View(db.T_Plc.ToList());
        }

        // GET: Admin/T_Plc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Plc t_Plc = db.T_Plc.Find(id);
            if (t_Plc == null)
            {
                return HttpNotFound();
            }
            return View(t_Plc);
        }

        // GET: Admin/T_Plc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/T_Plc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Name,IpAddress")] T_Plc t_Plc)
        {
            if (ModelState.IsValid)
            {
                db.T_Plc.Add(t_Plc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Plc);
        }

        // GET: Admin/T_Plc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Plc t_Plc = db.T_Plc.Find(id);
            if (t_Plc == null)
            {
                return HttpNotFound();
            }
            return View(t_Plc);
        }

        // POST: Admin/T_Plc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Name,IpAddress")] T_Plc t_Plc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Plc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Plc);
        }

        // GET: Admin/T_Plc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Plc t_Plc = db.T_Plc.Find(id);
            if (t_Plc == null)
            {
                return HttpNotFound();
            }
            return View(t_Plc);
        }

        // POST: Admin/T_Plc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Plc t_Plc = db.T_Plc.Find(id);
            db.T_Plc.Remove(t_Plc);
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
