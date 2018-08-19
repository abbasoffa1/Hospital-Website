using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;
namespace Hospital.Controllers
{
    public class DepartmentsController : Controller
    {
        HospitalFinalEntities db = new HospitalFinalEntities();
        // GET: Departments
        public ActionResult Index()
        {
            AddView model = new AddView();
            model.departments = db.Department.OrderByDescending(m => m.Id).ToList();
            model.openingHours = db.OpeningHours.OrderByDescending(o => o.Id).Take(3).ToList();
            model.settings = db.Settings.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }
    }
}