using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderSystem.Models;
using System.Linq;

namespace FoodOrderSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly OrderSystemContext _context;

        public HomeController(OrderSystemContext context)
        {
            _context = context;
        }

        // Ana sayfa (Ürünler ve Kategoriler)
        public IActionResult Index()
        {
            var meals = _context.Meals.Include(m => m.Category).ToList();
            var categories = _context.Categories.ToList();

            ViewBag.Categories = categories;
            return View(meals);
        }
    }
}