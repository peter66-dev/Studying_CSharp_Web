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
        public bool IsLogin { get; set; } = false;
        IMemberRepository memRepository = null;
        public MembersController() => memRepository = new MemberRepository();
        // GET: MembersController
        public ActionResult Index()
        {
            var memList = memRepository.GetMembers();
            return View(memList);
        }

        // GET: MembersController/Details/5
        public ActionResult Details(int? id)
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

        // GET: MembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member mem)
        {
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

        // POST: MembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member mem)
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
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MembersController/Delete/5
        public ActionResult Delete(int? id)
        {
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
