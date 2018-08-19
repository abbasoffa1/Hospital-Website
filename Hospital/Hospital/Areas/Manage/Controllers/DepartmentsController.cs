using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;
using System.IO;
namespace Hospital.Areas.Manage.Controllers
{
    
    public class DepartmentsController : Controller
    {
        HospitalFinalEntities db = new HospitalFinalEntities();
        // GET: Manage/Departments
        public ActionResult Index()
        {
            var departments = db.Department.Include(d => d.Event);
            return View(departments.OrderByDescending(m=>m.Id).ToList());
        }

        // GET: Manage/Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department departments = db.Department.Find(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // GET: Manage/Departments/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name");

            return View();

        }

        // POST: Manage/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Photo,About,EventId")] Department departments,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + Photo.FileName;
                string PhotoPath = Server.MapPath("~/Uploads/");
                Photo.SaveAs(PhotoPath + FileName);
                departments.Photo = FileName;
                db.Department.Add(departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", departments.EventId);
            return View(departments);
        }

        // GET: Manage/Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department departments = db.Department.Find(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", departments.EventId);
            return View(departments);
        }

        // POST: Manage/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Photo,About,EventId")] Department departments, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departments).State = EntityState.Modified;


                if (Photo != null)
                {
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + Photo.FileName;
                    string PhotoPath = Server.MapPath("~/Uploads/");
                    Photo.SaveAs(PhotoPath + FileName);
                    departments.Photo = FileName;
                }
                else
                {
                    db.Entry(departments).Property(d => d.Photo).IsModified = false;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Name", departments.EventId);
            return View(departments);
        }

        // GET: Manage/Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department departments = db.Department.Find(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // POST: Manage/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department departments = db.Department.Find(id);
            db.Department.Remove(departments);
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
