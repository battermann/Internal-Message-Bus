using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movies.Web.Models;

namespace Movies.Web.Controllers
{
    public class MovieVmsController : Controller
    {
        private MoviesWebContext db = new MoviesWebContext();

        // GET: MovieVms
        public ActionResult Index()
        {
            return View(db.MovieVms.ToList());
        }

        // GET: MovieVms/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieVm movieVm = db.MovieVms.Find(id);
            if (movieVm == null)
            {
                return HttpNotFound();
            }
            return View(movieVm);
        }

        // GET: MovieVms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieVms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Genre,Price")] MovieVm movieVm)
        {
            if (ModelState.IsValid)
            {
                movieVm.Id = Guid.NewGuid();
                db.MovieVms.Add(movieVm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieVm);
        }

        // GET: MovieVms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieVm movieVm = db.MovieVms.Find(id);
            if (movieVm == null)
            {
                return HttpNotFound();
            }
            return View(movieVm);
        }

        // POST: MovieVms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Genre,Price")] MovieVm movieVm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieVm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieVm);
        }

        // GET: MovieVms/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieVm movieVm = db.MovieVms.Find(id);
            if (movieVm == null)
            {
                return HttpNotFound();
            }
            return View(movieVm);
        }

        // POST: MovieVms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MovieVm movieVm = db.MovieVms.Find(id);
            db.MovieVms.Remove(movieVm);
            db.SaveChanges();
            return RedirectToAction("Index");
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
