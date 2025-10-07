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
            // Ensure the input data from the form satisfies all model validation requirements
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, re-display the form with existing inputs and validation messages
                return View(insuree);
            }

            // Initialize the starting insurance quote with a base price
            decimal baseQuote = 50.00m;

            // Calculate the user's age based on the date of birth
            int age = DateTime.Today.Year - insuree.DateOfBirth.Year;

            // Adjust age if the birthday hasn't occurred yet 
            if (insuree.DateOfBirth > DateTime.Today.AddYears(-age)) age--;

            // Adjust quote based on age brackets
            if (age <= 18)
            {
                baseQuote += 100m; // High-risk 
            }
            else if (age <= 25)
            {
                baseQuote += 50m; // Moderate-risk
            }
            else
            {
                baseQuote += 25m; // Low-risk
            }

            // Add fee if the car is considered too old or too new
            if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
            {
                baseQuote += 25m;
            }

            string make = insuree.CarMake?.Trim().ToLower() ?? "";
            string model = insuree.CarModel?.Trim().ToLower() ?? "";

            // Check for specific vehicle make/model combinations that incur additional fees
            if (make == "Porsche")
            {
                baseQuote += 25m;

                if (model == "911 Carrera")
                {
                    baseQuote += 25m;
                }
            }

            // Apply a flat fee per recorded speeding ticket
            baseQuote += insuree.SpeedingTickets * 10m;

            // If the applicant has a DUI history, increase the quote by 25%
            if (insuree.DUI)
            {
                baseQuote *= 1.25m;
            }

            // If full coverage is requested, raise the total by 50%
            if (insuree.CoverageType)
            {
                baseQuote *= 1.50m;
            }

            // Store the final calculated quote in the insuree object
            insuree.Quote = baseQuote;

            // Persist the insuree record to the database
            db.Insurees.Add(insuree);
            db.SaveChanges();

            //redirect the user to the Index view
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
