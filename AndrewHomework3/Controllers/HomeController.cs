using AndrewHomework3.Models;
//using AndrewHomework3.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewHomework3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProjectDbContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, ProjectDbContext context)
        {
            _logger = logger;
            this.context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MovieApplication()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieApplication(MovieModel appResponse)
        {
            if (ModelState.IsValid)
            {
                context.ProjectModel.Add(appResponse);
                context.SaveChanges();
                return View("Confirmation", appResponse);
            }

            return View();
        }

        public ViewResult List()
        {
            var movieList = context.ProjectModel.ToList();
            return View(movieList.Where(r => r.Title != "Independence Day"));
        }


        //Trying to update the models...
        public IActionResult Update(int movieid)
        {
            MovieModel movie = context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Update(MovieModel movie, int movieid)
        {
            context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Category = movie.Category;
            context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Year = movie.Year;
            context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Rating = movie.Rating;
            context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().LentTo = movie.LentTo;
            context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Notes = movie.Notes;
            context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Title = movie.Title;

            context.SaveChanges();
            return RedirectToAction("List"); //not sure about this one yet
        }

        [HttpPost]
        public IActionResult Delete(int movieid)
        {
            MovieModel movie = context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault();
            context.ProjectModel.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
