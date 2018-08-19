using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;
namespace Hospital.Controllers
{
    public class DoctorsController : Controller
    {
        HospitalFinalEntities db = new HospitalFinalEntities();
        // GET: Doctors
        public ActionResult Index()
        {
            AddView model = new AddView();
            model.doctors = db.Doctors.OrderByDescending(m=>m.Id).ToList();
            model.openingHours = db.OpeningHours.OrderByDescending(m => m.Id).Take(3).ToList();
            model.departments = db.Department.OrderByDescending(m => m.Id).ToList();
            model.settings = db.Settings.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }
        public new ActionResult Profile(int id)
        {
            AddView model = new AddView();
            model.departments = db.Department.OrderByDescending(m => m.Id).ToList();
            model.settings = db.Settings.OrderByDescending(m => m.Id).ToList();
            model.certificates = db.Certificates.OrderByDescending(m => m.Id).Where(m=>m.DoctorId==id).ToList();
            model.id= db.Doctors.FirstOrDefault(x=> x.Id==id);
            return View(model);
        }
    }
}