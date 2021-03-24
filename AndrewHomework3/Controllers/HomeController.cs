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

        //public IActionResult List()
        //{
        //  return View(TempStorage.Applications);
        //}

        public ViewResult List()
        {
            var movieList = context.ProjectModel.ToList();
            //return View(TempStorage.Applications.Where(r => r.Title != "Independence Day"));
            return View(movieList.Where(r => r.Title != "Independence Day"));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
