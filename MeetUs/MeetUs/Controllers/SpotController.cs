﻿using System;
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

            string fbData = "You can find this info in the FB documentation.";
            SpotDetailsResponse response = new SpotDetailsResponse();
            response.FbLikeButtonData = fbData;
            response.Spot = spot;

            return View(response);
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
        public ActionResult Create(SpotCreateRequest request)
        {
            Spot spot = new Spot();
            if (ModelState.IsValid)
            {
                
                spot.Name = request.Name;
                spot.Description = request.Description;

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
        [Authorize(Roles="Administrator")]
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
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpotEditRequest request)
        {
            Spot spot = new Spot();
            if (ModelState.IsValid)
            {
                //Find spot that matches request
                spot = db.Spots.Find(request.SpotId);
                spot.Description = request.Description;
                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spot);
        }

        //
        // GET: /Spot/Delete/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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