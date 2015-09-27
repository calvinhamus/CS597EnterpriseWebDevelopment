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
    public class T_UserPlcController : Controller
    {
        private TrendData db = new TrendData();

        // GET: Admin/T_UserPlc
        public ActionResult Index()
        {
            var t_UserPlc = db.T_UserPlc.Include(t => t.AspNetUser).Include(t => t.T_Plc);
            return View(t_UserPlc.ToList());
        }

        // GET: Admin/T_UserPlc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_UserPlc t_UserPlc = db.T_UserPlc.Find(id);
            if (t_UserPlc == null)
            {
                return HttpNotFound();
            }
            return View(t_UserPlc);
        }

        // GET: Admin/T_UserPlc/Create
        public ActionResult Create()
        {
            ViewBag.T_UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name");
            return View();
        }

        // POST: Admin/T_UserPlc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,T_UserId,T_PlcId")] T_UserPlc t_UserPlc)
        {
            if (ModelState.IsValid)
            {
                db.T_UserPlc.Add(t_UserPlc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.T_UserId = new SelectList(db.AspNetUsers, "Id", "Email", t_UserPlc.T_UserId);
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name", t_UserPlc.T_PlcId);
            return View(t_UserPlc);
        }

        // GET: Admin/T_UserPlc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_UserPlc t_UserPlc = db.T_UserPlc.Find(id);
            if (t_UserPlc == null)
            {
                return HttpNotFound();
            }
            ViewBag.T_UserId = new SelectList(db.AspNetUsers, "Id", "Email", t_UserPlc.T_UserId);
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name", t_UserPlc.T_PlcId);
            return View(t_UserPlc);
        }

        // POST: Admin/T_UserPlc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,T_UserId,T_PlcId")] T_UserPlc t_UserPlc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_UserPlc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.T_UserId = new SelectList(db.AspNetUsers, "Id", "Email", t_UserPlc.T_UserId);
            ViewBag.T_PlcId = new SelectList(db.T_Plc, "Id", "Name", t_UserPlc.T_PlcId);
            return View(t_UserPlc);
        }

        // GET: Admin/T_UserPlc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_UserPlc t_UserPlc = db.T_UserPlc.Find(id);
            if (t_UserPlc == null)
            {
                return HttpNotFound();
            }
            return View(t_UserPlc);
        }

        // POST: Admin/T_UserPlc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_UserPlc t_UserPlc = db.T_UserPlc.Find(id);
            db.T_UserPlc.Remove(t_UserPlc);
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
