using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Areas.Manage.Controllers
{
    public class AboutClinicsController : Controller
    {
        private HospitalFinalEntities db = new HospitalFinalEntities();

        // GET: Manage/AboutClinics
        public ActionResult Index()
        {
            return View(db.AboutClinics.OrderByDescending(m=>m.Id).ToList());
        }

        // GET: Manage/AboutClinics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutClinic aboutClinic = db.AboutClinics.Find(id);
            if (aboutClinic == null)
            {
                return HttpNotFound();
            }
            return View(aboutClinic);
        }

        // GET: Manage/AboutClinics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/AboutClinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Header,Text,IconPhoto")] AboutClinic aboutClinic,HttpPostedFileBase IconPhoto)
        {
            if (ModelState.IsValid)
            {
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + IconPhoto.FileName;
                string PhotoPath = Server.MapPath("~/Uploads/");
                IconPhoto.SaveAs(PhotoPath + FileName);
                aboutClinic.IconPhoto = FileName;
                db.AboutClinics.Add(aboutClinic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutClinic);
        }

        // GET: Manage/AboutClinics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutClinic aboutClinic = db.AboutClinics.Find(id);
            if (aboutClinic == null)
            {
                return HttpNotFound();
            }
            return View(aboutClinic);
        }

        // POST: Manage/AboutClinics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,Text,IconPhoto")] AboutClinic aboutClinic,HttpPostedFileBase IconPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aboutClinic).State = EntityState.Modified;
                if (IconPhoto != null)
                {
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + IconPhoto.FileName;
                    string PhotoPath = Server.MapPath("~/Uploads/");
                    IconPhoto.SaveAs(PhotoPath + FileName);
                    aboutClinic.IconPhoto = FileName;
                }
                else
                {
                    db.Entry(aboutClinic).Property(d => d.IconPhoto).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutClinic);
        }

        // GET: Manage/AboutClinics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutClinic aboutClinic = db.AboutClinics.Find(id);
            if (aboutClinic == null)
            {
                return HttpNotFound();
            }
            return View(aboutClinic);
        }

        // POST: Manage/AboutClinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutClinic aboutClinic = db.AboutClinics.Find(id);
            db.AboutClinics.Remove(aboutClinic);
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
