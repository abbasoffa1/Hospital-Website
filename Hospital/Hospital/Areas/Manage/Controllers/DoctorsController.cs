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
using System.Data.Entity.Validation;

namespace Hospital.Areas.Manage.Controllers
{
    public class DoctorsController : Controller
    {
        private HospitalFinalEntities db = new HospitalFinalEntities();

        // GET: Manage/Doctors
        public ActionResult Index()
        {
            var doctors = db.Doctors.OrderByDescending(m=>m.Id).Include(d => d.Department);
            return View(doctors.ToList());
        }

        // GET: Manage/Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctors = db.Doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctors);
        }

        // GET: Manage/Doctors/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name");
            return View();
        }

        // POST: Manage/Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,FullName,Photo,Phone,Email,DoctorAbout,DoctorsThoughts")] Doctor doctors,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + Photo.FileName;
                string PhotoPath = Server.MapPath("~/Uploads/");
                Photo.SaveAs(PhotoPath + FileName);
                doctors.Photo = FileName;
                db.Doctors.Add(doctors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name", doctors.DepartmentId);
            return View(doctors);
        }

        // GET: Manage/Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctors = db.Doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name", doctors.DepartmentId);
            return View(doctors);
        }

        // POST: Manage/Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,FullName,Photo,Phone,Email,DoctorAbout,DoctorsThoughts")] Doctor doctors,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctors).State = EntityState.Modified;
                if (Photo != null)
                {
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + Photo.FileName;
                    string PhotoPath = Server.MapPath("~/Uploads/");
                    Photo.SaveAs(PhotoPath + FileName);
                    doctors.Photo = FileName;
                }
                else
                {
                    db.Entry(doctors).Property(d => d.Photo).IsModified = false;
                }

               
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name", doctors.DepartmentId);
            return View(doctors);
        }

        // GET: Manage/Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctors = db.Doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctors);
        }

        // POST: Manage/Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctors = db.Doctors.Find(id);
            db.Doctors.Remove(doctors);
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
