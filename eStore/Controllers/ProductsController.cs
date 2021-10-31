using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class ProductsController : Controller
    {
        public bool IsLogin { get; set; } = false;
        IProductRepository proRepository = null;
        public ProductsController() => proRepository = new ProductRepository();
        // GET: ProductsController
        public ActionResult Index()
        {
            var proList = proRepository.GetProducts();
            return View(proList);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var pro = proRepository.GetProductByID(id.Value);
                if (pro == null)
                {
                    return NotFound();
                }
                return View(pro);
            }
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    proRepository.InsertProduct(pro);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(pro);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var pro = proRepository.GetProductByID(id.Value);
                if (pro == null)
                {
                    return NotFound();
                }
                return View(pro);
            }
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product pro)
        {
            try
            {
                if (id != pro.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    proRepository.UpdateProduct(pro);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pro = proRepository.GetProductByID(id.Value);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                proRepository.GetProductByID(id);
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
