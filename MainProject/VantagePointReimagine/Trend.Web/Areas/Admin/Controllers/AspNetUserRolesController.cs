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
    public class AspNetUserRolesController : BaseAdminController
    {
        private TrendData db = new TrendData();

        // GET: Admin/AspNetUserRoles
        public ActionResult Index()
        {
            var aspNetUserRoles = db.AspNetUserRoles.Include(a => a.AspNetRole).Include(a => a.AspNetUser);
            return View(aspNetUserRoles.ToList());
        }

        // GET: Admin/AspNetUserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRole);
        }

        // GET: Admin/AspNetUserRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Admin/AspNetUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RoleId")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserRoles.Add(aspNetUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: Admin/AspNetUserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // POST: Admin/AspNetUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RoleId")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }

        // GET: Admin/AspNetUserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRole);
        }

        // POST: Admin/AspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
            db.AspNetUserRoles.Remove(aspNetUserRole);
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
