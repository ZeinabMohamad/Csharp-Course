using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            // Check if the submitted form data meets all validation rules
            if (!ModelState.IsValid)
            {
                // Re-render the form with validation messages if any issues found
                return View(insuree);
            }

            // Base rate for insurance before any adjustments
            decimal baseQuote = 50.00m;

            // Determine applicant's age
            int age = DateTime.Today.Year - insuree.DateOfBirth.Year;
            if (insuree.DateOfBirth > DateTime.Today.AddYears(-age)) age--;

            // Modify quote based on age brackets
            if (age <= 18)
            {
                baseQuote += 100m;
            }
            else if (age <= 25)
            {
                baseQuote += 50m;
            }
            else
            {
                baseQuote += 25m;
            }

            // Surcharge if the car is either very old or very new
            if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
            {
                baseQuote += 25m;
            }

            // Normalize make and model input for consistent comparison
            string make = insuree.CarMake?.Trim().ToLower() ?? "";
            string model = insuree.CarModel?.Trim().ToLower() ?? "";

            // Extra charge for specific car brands and models
            if (make == "Mercedes")
            {
                baseQuote += 25m;

                if (model == "G Class")
                {
                    baseQuote += 25m;
                }
            }

            // Add $10 per recorded speeding violation
            baseQuote += insuree.SpeedingTickets * 10m;

            // Increase total quote by 25% if the driver has a DUI
            if (insuree.DUI)
            {
                baseQuote *= 1.25m;
            }

            // Apply a 50% premium if full coverage is requested
            if (insuree.CoverageType)
            {
                baseQuote *= 1.50m;
            }

            //  final calculated quote to the model
            insuree.Quote = baseQuote;

            //  insuree record to the database
            db.Insurees.Add(insuree);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Admin()
        {
            var insurees = db.Insurees.ToList();
            return View(insurees);
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
