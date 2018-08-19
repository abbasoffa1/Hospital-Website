using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models;
namespace Hospital.Models
{
    public class AddView
    {
        public List<AboutClinic> aboutClinics { get; set; }
        public List<AboutPhoto> aboutPhotos { get; set; }
        public List<Certificate> certificates { get; set; }
        public List<Doctor> doctors { get; set; }
        public List<Department> departments { get; set; }
        public List<Day> days { get; set; }
        public List<DayTime> daytimes { get; set; }
        public List<Timetable> timetables { get; set; }
        public List<Setting> settings { get; set; }
        public List<Counter> counters { get; set; }
        public List<Appoinment> appoinments { get; set; }
        public List<Event> events { get; set; }
        public List<OpeningHour> openingHours { get; set; }
        public List<Slider> sliders { get; set; }
        public List<Testimonial> testimonials { get; set; }

        public Doctor id { get; set; }

    }
}