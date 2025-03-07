using Microsoft.AspNetCore.Mvc;
using FoodOrderSystem.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly OrderSystemContext _context;

        public AccountController(OrderSystemContext context)
        {
            _context = context;
        }

        // Giriş Sayfası
        public IActionResult Login()
        {
            return View();
        }

        // Giriş İşlemi
        [HttpPost]
        public IActionResult Login(string userName)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı!";
                return View();
            }

            if (user.Role.RoleName == "Admin")
            {
                return RedirectToAction("AdminPanel", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}