using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;
namespace Hospital.Controllers
{
    public class ContactController : Controller
    {
        HospitalFinalEntities db = new HospitalFinalEntities();
        // GET: Contact
        public ActionResult Index()
        {
            AddView model = new AddView();
            model.departments = db.Department.OrderByDescending(m => m.Id).ToList();
            model.settings = db.Settings.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }
    }
}