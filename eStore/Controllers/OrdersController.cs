using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class OrdersController : Controller
    {
        public bool IsLogin { get; set; } = false;
        IOrderRepository ordRepository = null;
        public OrdersController() => ordRepository = new OrderRepository();
        // GET: OrdersController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                var ordList = ordRepository.GetOrders();
                ViewData["Total"] = ordRepository.GetTotalMoney((List<Order>)ordList);
                return View(ordList);
            }
        }
        public ActionResult Statistic(string startDay, string endDay)
        {
            List<Order> ordList = new List<Order>();
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                try
                {
                    if (int.TryParse(startDay, out int start) && int.TryParse(endDay, out int end) && (start < end && start > 0))
                    {
                        ordList = (List<Order>)ordRepository.GetStatistic(start, end);
                        ViewData["Total"] = ordRepository.GetTotalMoney(ordList);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Value input is not valid!";
                    return View("Privacy", "Home");
                }
            }
            return View(nameof(Index), ordList);
        }
        public ActionResult Sortasc()
        {
            List<Order> ordList = new List<Order>();
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                ordList = (List<Order>)ordRepository.SortAsc();
                ViewData["Total"] = ordRepository.GetTotalMoney(ordList);
            }
            return View(nameof(Index), ordList);
        }

        public ActionResult Sortdesc()
        {
            List<Order> ordList = new List<Order>();
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                ordList = (List<Order>)ordRepository.SortDesc();
                ViewData["Total"] = ordRepository.GetTotalMoney(ordList);
            }
            return View(nameof(Index), ordList);
        }


        // GET: OrdersController/Details/5
        public ActionResult Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var o = ordRepository.GetOrderByID(id.Value);
                    if (o == null)
                    {
                        return NotFound();
                    }
                    return View(o);
                }
            }
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return View();
            }
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order o)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ordRepository.InsertOrder(o);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Sorry, this order id has existed in list!" + ex.Message;
                    return View(o);
                }
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var o = ordRepository.GetOrderByID(id.Value);
                    if (o == null)
                    {
                        return NotFound();
                    }
                    return View(o);
                }
            }
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order o)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                try
                {
                    if (id != o.OrderId)
                    {
                        return NotFound();
                    }
                    if (ModelState.IsValid)
                    {
                        ordRepository.UpdateOrder(o);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View();
                }
            }
        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var o = ordRepository.GetOrderByID(id.Value);
                if (o == null)
                {
                    return NotFound();
                }
                return View(o);
            }
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                try
                {
                    ordRepository.GetOrderByID(id);
                    ViewBag.Message = "Sorry, we can't delete because it's a foreign key's another table in system!";
                    return View(nameof(Index), ordRepository.GetOrders());
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View();
                }
            }
        }
    }
}
