using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;

namespace eStore.Controllers
{
    public class MembersController : Controller
    {
        IMemberRepository memRepository = null; 
        public MembersController() => memRepository = new MemberRepository();
        // GET: MembersController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            var memList = memRepository.GetMembers();
            return View(memList);
        }

        // GET: MembersController/Details/5
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
                    var mem = memRepository.GetMemberByID(id.Value);
                    if (mem == null)
                    {
                        return NotFound();
                    }
                    return View(mem);
                }
            }
        }

        public ActionResult CustomerDetails(int? id) // from here to OrderDetailsController
        {
            if (HttpContext.Session.GetString("user") == null)
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
                    var mem = memRepository.GetMemberByID(id.Value);
                    if (mem == null)
                    {
                        return NotFound();
                    }
                    return View(mem);
                }
            }
        }

        // GET: MembersController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View();
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member mem)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    memRepository.InsertMember(mem);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(mem);
            }
        }

        // GET: MembersController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") == null && HttpContext.Session.GetString("user") == null)
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
                    var mem = memRepository.GetMemberByID(id.Value);
                    if (mem == null)
                    {
                        return NotFound();
                    }
                    return View(mem);
                }
            }
        }

        // POST: MembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member mem)
        {
            if (HttpContext.Session.GetString("admin") == null && HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                try
                {
                    if (id != mem.MemberId)
                    {
                        return NotFound();
                    }
                    if (ModelState.IsValid)
                    {
                        memRepository.UpdateMember(mem);
                        ViewBag.Message = "Updating successfully!";
                    }
                    return View("CustomerDetails", mem);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return RedirectToAction("Privacy", "Home");
                }
            }
        }

        // GET: MembersController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            var mem = memRepository.GetMemberByID(id.Value);
            if (mem == null)
            {
                return NotFound();
            }
            return View(mem);
        }

        // POST: MembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            try
            {
                memRepository.GetMemberByID(id);
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
