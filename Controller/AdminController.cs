using Microsoft.AspNetCore.Mvc;
using FoodOrderSystem.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly OrderSystemContext _context;

        public AdminController(OrderSystemContext context)
        {
            _context = context;
        }

        // Admin Paneli
        public IActionResult AdminPanel()
        {
            var meals = _context.Meals.Include(m => m.Category).ToList();
            var categories = _context.Categories.ToList();

            ViewBag.Categories = categories;
            return View(meals);
        }
    }
}