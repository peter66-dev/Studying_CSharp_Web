using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        bool CheckLogin(AdminAccount admin)
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
                if (admin.Username.Equals(account.Username) && admin.Password.Equals(account.Password))
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
            }
            return check;
        }
        public ActionResult Login(AdminAccount admin)
        {
            _logger.LogInformation("Hello Admin");
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Can you run to here");
                    if (CheckLogin(admin))
                    {
                        Session["admin"] = admin;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Your username or password is not correct! Please, try again!";
                        return View("Index");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
