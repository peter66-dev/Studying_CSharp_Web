using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class OrderDetailsController : Controller
    {
        public bool IsLogin { get; set; } = false;
        IOrderDetailRepository ordRepository = null;
        public OrderDetailsController() => ordRepository = new OrderDetailRepository();
        // GET: OrderDetailsController
        public ActionResult Index()
        {
            var oList = ordRepository.GetOrderDetails();
            return View(oList);
        }

        // GET: OrderDetailsController/Details/5
        public ActionResult Details(int? id, int proID)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var o = ordRepository.GetOrderDetailByID(id.Value, proID);
                if (o == null)
                {
                    return NotFound();
                }
                return View(o);
            }
        }

        // GET: OrderDetailsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail o)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ordRepository.InsertOrderDetail(o);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Sorry, this order detail id has existed in list!" + ex.Message;
                return View(o);
            }
        }

        // GET: OrderDetailsController/Edit/5
        public ActionResult Edit(int? id, int proID)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var o = ordRepository.GetOrderDetailByID(id.Value, proID);
                if (o == null)
                {
                    return NotFound();
                }
                return View(o);
            }
        }

        // POST: OrderDetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderDetail o)
        {
            try
            {
                if (id != o.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    ordRepository.UpdateOrderDetail(o);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderDetailsController/Delete/5
        public ActionResult Delete(int? id, int proID)
        {
            if (id == null)
            {
                return NotFound();
            }
            var o = ordRepository.GetOrderDetailByID(id.Value, proID);
            if (o == null)
            {
                return NotFound();
            }
            return View(o);
        }

        // POST: OrderDetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int proID)
        {
            try
            {
                ordRepository.GetOrderDetailByID(id, proID);
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
