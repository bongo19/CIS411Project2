using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetUs.Models;
using MeetUs.Context;

namespace MeetUs.Controllers
{
    public class SpotController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Spot/

        public ActionResult Index()
        {
            return View(db.Spots.ToList());
        }

        //
        // GET: /Spot/Details/5

        public ActionResult Details(int id = 0)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        //
        // GET: /Spot/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Spot/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Spot spot)
        {
            if (ModelState.IsValid)
            {
                spot.DateAdded = DateTime.Now;
                db.Spots.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spot);
        }

        //
        // GET: /Spot/Join/5

        public ActionResult Join(int id = 0)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            var userProfile = db.UserProfiles.Where(user => user.UserName == User.Identity.Name).Single();
            spot.UserProfiles.Add(userProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /Spot/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        //
        // POST: /Spot/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spot);
        }

        //
        // GET: /Spot/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        //
        // POST: /Spot/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spot spot = db.Spots.Find(id);
            db.Spots.Remove(spot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}