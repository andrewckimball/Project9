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
            this.context = context; //adding context from the ProjectDbContext model

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
            //Checking to see if data was recorded correctly in the view
            if (ModelState.IsValid)
            {
                //Add the currentMovieModel object to the databse
                context.ProjectModel.Add(appResponse);
                context.SaveChanges();
                return View("Confirmation", appResponse); //Return to the Confirmation page, passing in the data
            }

            return View();
        }

        public ViewResult List()
        {
            //Set the movieList variable to the ProjectModel, display to a list
            var movieList = context.ProjectModel.ToList();
            return View(movieList.Where(r => r.Title.ToUpper() != "INDEPENDENCE DAY")); //Model exludes "Independence Day" title from being stored
        }


        //Controller handling the updating of the models
        public IActionResult Update(int movieid) //Passing in the movieid
        {
            //MovieModel movie = context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault(); *Same logic as line 68*
            var movieList = context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault();
            return View(movieList);
        }

        [HttpPost]
        public IActionResult Update(MovieModel movie, int movieid)
        {
            //Checking if the title was Independence Day. If so, return confirmation with info regarding the error
            if (movie.Title.ToUpper() == "INDEPENDENCE DAY")
            {
                return View("Confirmation", movie);
            }

            else if (ModelState.IsValid)
            {
                //On post, update each attribute of the model
                context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Category = movie.Category;
                context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Year = movie.Year;
                context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Rating = movie.Rating;
                context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().LentTo = movie.LentTo;
                context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Notes = movie.Notes;
                context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault().Title = movie.Title;

                //Saving changes to database
                context.SaveChanges();
                return RedirectToAction("List"); 
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int movieid)
        {
            //MovieModel movie = context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault(); *Same logic as line 92*
            var movieList = context.ProjectModel.Where(e => e.MovieID == movieid).FirstOrDefault();
            context.ProjectModel.Remove(movieList);
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
