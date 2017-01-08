using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestProjet.Models;

namespace TestProjet.Controllers
{
    public class Coordonnees_de_livraisonController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Coordonnees_de_livraison
        public ActionResult Index()
        {
            Client c = getClientByMail(User.Identity.Name);
            return View(db.Coordonnees_de_livraison.Where(co => co.id_client == c.id));
        }

        private Client getClientByMail(string mail)
        {
            return db.Client.Where(c => c.email == mail).First();
        }

        // GET: Coordonnees_de_livraison/Create
        public ActionResult Create()
        {
            Coordonnees_de_livraison co = new Coordonnees_de_livraison();
            co.email = User.Identity.Name;
            Client c = getClientByMail(User.Identity.Name);
            co.id_client = c.id;
            return View(co);
        }

        // POST: Coordonnees_de_livraison/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_client,email,numero_telephone,pays,ville,adresse,code_postale")] Coordonnees_de_livraison coordonnees_de_livraison)
        {
            if (ModelState.IsValid)
            {
                Client c = getClientByMail(User.Identity.Name);
                coordonnees_de_livraison.id_client = c.id;
                db.Coordonnees_de_livraison.Add(coordonnees_de_livraison);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coordonnees_de_livraison);
        }

        // GET: Coordonnees_de_livraison/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordonnees_de_livraison coordonnees_de_livraison = db.Coordonnees_de_livraison.Find(id);
            if (coordonnees_de_livraison == null)
            {
                return HttpNotFound();
            }
            return View(coordonnees_de_livraison);
        }

        // POST: Coordonnees_de_livraison/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_client,email,numero_telephone,pays,ville,adresse,code_postale")] Coordonnees_de_livraison coordonnees_de_livraison)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordonnees_de_livraison).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coordonnees_de_livraison);
        }

        // GET: Coordonnees_de_livraison/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordonnees_de_livraison coordonnees_de_livraison = db.Coordonnees_de_livraison.Find(id);
            if (coordonnees_de_livraison == null)
            {
                return HttpNotFound();
            }
            return View(coordonnees_de_livraison);
        }

        // POST: Coordonnees_de_livraison/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coordonnees_de_livraison coordonnees_de_livraison = db.Coordonnees_de_livraison.Find(id);
            db.Coordonnees_de_livraison.Remove(coordonnees_de_livraison);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
