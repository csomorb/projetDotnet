using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProjet.Models;

namespace TestProjet.Controllers
{
    public class CategorieController : Controller
    {

        private EcommerceEntities _db = new EcommerceEntities();

        // GET: Categorie
        public ActionResult Index()
        {
            return View(_db.Categorie.ToList());
        }

        // GET: Categorie/Details/5
        // créer la vue pour les détails
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categorie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categorie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Categorie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
