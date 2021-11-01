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
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                var proList = proRepository.GetProducts();
                return View(proList);
            }
        }

        // GET: ProductsController/Details/5
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
                    var pro = proRepository.GetProductByID(id.Value);
                    if (pro == null)
                    {
                        return NotFound();
                    }
                    return View(pro);
                }
            }
        }

        // GET: ProductsController/Create
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

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pro)
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
        }

        // GET: ProductsController/Edit/5
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
                    var pro = proRepository.GetProductByID(id.Value);
                    if (pro == null)
                    {
                        return NotFound();
                    }
                    return View(pro);
                }
            }
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product pro)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
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
        }

        // GET: ProductsController/Delete/5
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
                var pro = proRepository.GetProductByID(id.Value);
                if (pro == null)
                {
                    return NotFound();
                }
                return View(pro);
            }
        }

        // POST: ProductsController/Delete/5
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

        public ActionResult Sortasc()// chưa có view của Sortasc
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                var proList = proRepository.Sortasc();
                return View(nameof(Index), proList);
            }
        }

        public ActionResult Sortdesc() // chưa có view của Sortdesc
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                var proList = proRepository.Sortdesc();
                return View(nameof(Index), proList);
            }
        }

        public ActionResult Search(string valueInput) // chưa có view của Sortdesc
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                var proList = proRepository.GetProductByName(valueInput);
                if (proList.Count == 0)
                {
                    ViewBag.Message = "Sorry, we can't find any this product name in system";
                }
                return View(nameof(Index), proList);
            }
        }

        public ActionResult FindPrice() // chưa có view của Sortdesc
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                int valuePrice = int.Parse(HttpContext.Request.Form["valuePrice"]);
                var proList = proRepository.GetProductByPrice(valuePrice);//1, 2, 3, 4
                if (proList.Count == 0)
                {
                    ViewBag.Message = "Sorry, we can't find any this product name in system";
                }
                return View(nameof(Index), proList);
            }
        }
    }
}
