using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        OnlineExamSystemContext context = new OnlineExamSystemContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            List<Category> categories = context.Categories.ToList();
            ViewBag.categories = categories;

            ViewBag.listExam = listExam;
            ViewBag.Page = 1;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



    }
}