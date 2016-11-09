using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Lagersystem.Repositories;
using MVC_Lagersystem.Models;

namespace MVC_Lagersystem.Controllers
{
    public class StoreController : Controller
    {
        private StoreRepository db;

        public StoreController()
        {
            db = new StoreRepository();

            ViewBag.SearchInput = "";
            ViewBag.Reverse = "false";
        }

        public ActionResult Index(string q = "", string s = "Name", bool r = false)
        {
            ViewBag.Reverse = !r;
            ViewBag.SearchInput = q;

            if (q.Length > 0)
            {
                var items = db.GetItemBySearch(q, s, r);

                if (items.Count() > 0)
                    return View(items);
            }

            return View(db.GetAllItems(s, r));
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string q = "", string s = "Name", bool r = false)
        {
            ViewBag.Reverse = !r;
            ViewBag.SearchInput = q;

            var items = db.GetItemBySearch(q, s, r);

            if (items.Count() > 0)
                return View(items);

            return View(db.GetAllItems());
        }

        public ActionResult Details(int ? id)
        {
            if (id != null)
                return View(db.GetItemById(id.Value));

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed([Bind(Include = "Name,Price,Shelf,Description")] StockItem item)
        {
            if (ModelState.IsValid)
                db.AddItem(item);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ? id)
        {
            if (id != null)
                return View(db.GetItemById(id.Value));

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed([Bind(Include = "Name,Price,Shelf,Description")] StockItem item)
        {
            if (ModelState.IsValid)
                db.EditItem(item);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ? id)
        {
            if (id != null)
                return View(db.GetItemById(id.Value));

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.RemoveItem(id);
            return RedirectToAction("Index");
        }
    }
}
