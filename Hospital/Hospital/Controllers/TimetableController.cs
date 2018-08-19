using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;
namespace Hospital.Controllers
{
    public class TimetableController : Controller
    {
        HospitalFinalEntities db = new HospitalFinalEntities();
        // GET: Timetable
        public ActionResult Index()
        {
            AddView model = new AddView();
            model.departments = db.Department.ToList();
            model.doctors = db.Doctors.OrderByDescending(m => m.Id).ToList();
            model.timetables = db.Timetables.OrderByDescending(m => m.Id).ToList();
            model.daytimes = db.DayTimes.ToList();
            model.settings = db.Settings.OrderByDescending(m => m.Id).ToList();
            model.days = db.Days.ToList();
            model.aboutPhotos = db.AboutPhotoes.OrderByDescending(m => m.Id).Take(1).ToList();
            return View(model);
        }
    }
}