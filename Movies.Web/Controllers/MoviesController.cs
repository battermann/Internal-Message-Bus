using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Movies.Commands;
using Movies.Contracts;
using Movies.Web.Models;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieQueryFacade _movieQueryFacade;
        private readonly ICommandSender _commandSender;

        public MoviesController(IMovieQueryFacade movieQueryFacade, ICommandSender commandSender)
        {
            _movieQueryFacade = movieQueryFacade;
            _commandSender = commandSender;
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _movieQueryFacade.GetAll().Select(x => new MovieVm
            {
                Id = x.Id,
                Title = x.Title,
                Genre = x.Genre,
                Year = x.ReleaseDate.Year,
                Price = x.Price
            });

            return View(movies);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieVms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Genre,Price")] MovieVm vm)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateMovie(Guid.NewGuid(), vm.Title, new DateTime(vm.Year,1,1), vm.Genre, vm.Price);
                _commandSender.Send(command);
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: MovieVms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return _movieQueryFacade.GetById(id.Value).Match<ActionResult>(
                onSome: x => View(new MovieVm
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Genre = x.Genre,
                        Year = x.ReleaseDate.Year,
                        Price = x.Price
                    }),
                onNone: HttpNotFound);
        }

        // POST: MovieVms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Genre,Price")] MovieVm vm)
        {
            if (ModelState.IsValid)
            {
                var command = new ChangeMovieTitle(vm.Id, vm.Title);
                _commandSender.Send(command);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}