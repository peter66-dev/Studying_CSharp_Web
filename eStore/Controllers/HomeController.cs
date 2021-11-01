using DataAccess;
using eStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMemberRepository memRepository = new MemberRepository();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        bool CheckLoginAdmin(string username, string password)
        {
            bool check = false;
            try
            {
                AdminAccount account = null;
                //string workingDirectory = Environment.CurrentDirectory;
                //string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                //string appsettings_path = projectDirectory + "\\" + "accountAdmin.json";
                StreamReader r = new StreamReader("accountAdmin.json");
                var json = r.ReadToEnd();
                account = JsonConvert.DeserializeObject<AdminAccount>(json);
                if (username.Equals(account.Username) && password.Equals(account.Password))
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
            }
            return check;
        }
        int CheckLoginUser(string username, string password)
        {
            int id = 0;
            foreach (var mem in memRepository.GetMembers())
            {
                if (mem.Email.Equals(username) && mem.Password.Equals(password))
                {
                    id = mem.MemberId;
                }
            }
            return id;
        }
        public ActionResult Login(string username, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (CheckLoginAdmin(username, password))
                    {
                        HttpContext.Session.SetString("admin", username);
                        return RedirectToAction("Index", "Members");
                    }
                    else if (CheckLoginUser(username, password) != 0)
                    {
                        HttpContext.Session.SetString("user", username);
                        return RedirectToAction("CustomerDetails", "Members", new { id = CheckLoginUser(username, password) });
                    }
                    else
                    {
                        ViewBag.Message = "Sorry, your username or password is not correct! Please, try again!";
                        return View("Index", new AdminAccount(username, password));
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error at login: " + ex.Message;
                return RedirectToAction("Privacy", "Home");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("admin");
                HttpContext.Session.Remove("user");
                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
