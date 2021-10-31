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
            var ordList = ordRepository.GetOrders();
            return View(ordList);
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int? id)
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

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order o)
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
                ViewBag.Message ="Sorry, this order id has existed in list!" +ex.Message;
                return View(o);
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order o)
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

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ordRepository.GetOrderByID(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
