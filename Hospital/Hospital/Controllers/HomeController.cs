using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        HospitalFinalEntities db = new HospitalFinalEntities();
        public ActionResult Index()
        {
            AddView model = new AddView();
            model.aboutClinics = db.AboutClinics.OrderByDescending(m => m.Id).Take(4).ToList();
            model.departments = db.Department.OrderByDescending(m => m.Id).ToList();
            model.doctors = db.Doctors.OrderByDescending(m => m.Id).ToList();
            model.settings = db.Settings.OrderByDescending(m => m.Id).ToList();
            model.sliders = db.Sliders.OrderByDescending(m => m.Id).ToList();
            model.openingHours= db.OpeningHours.OrderByDescending(m => m.Id).Take(3).ToList();
            model.testimonials = db.Testimonials.OrderByDescending(m => m.Id).ToList();
            model.appoinments = db.Appoinments.ToList();
            model.aboutPhotos=db.AboutPhotoes.OrderByDescending(m => m.Id).Take(1).ToList();
            return View(model);
        }
     
    }
}