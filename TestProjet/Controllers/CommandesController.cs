using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TestProjet.Models;

namespace TestProjet.Controllers
{
    public class CommandesController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Commandes
        public ActionResult Index()
        {
            return View(db.Commande.ToList());
        }

        public ActionResult Etape1()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Etape2");
            }
            ViewBag.ReturnUrl = "/Commandes/Etape2";
            return View();

        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Etape2()
        {
            Client c = getClientByMail(User.Identity.Name);
            if (db.Coordonnees_de_livraison.Count(co => co.id_client == c.id) == 0)
            {
                return RedirectToAction("Use", "Coordonnees_de_livraison");
            }
            ViewBag.listeCoord = db.Coordonnees_de_livraison.Where(co => co.id_client == c.id);
            return View();

        }

        [HttpPost]
        public ActionResult Etape2(Commande model)
        {
            return View();
            // a faire
        }

        // GET: Commandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // GET: Commandes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commandes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_utilisateur,etat,date_commande,date_livraison,date_expedition,id_livraison,fret,id_methode_livraison")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Commande.Add(commande);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commande);
        }

        // GET: Commandes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_utilisateur,etat,date_commande,date_livraison,date_expedition,id_livraison,fret,id_methode_livraison")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commande);
        }


        private Client nouveauClient(RegisterViewModel model)
        {
            Client c = new Client();
            c.email = model.Email;
            c.nom = model.Nom;
            c.prenom = model.Prenom;
            c.mot_de_passe = model.Password;
            c.genre = model.Genre;
            return c;
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (modelIsValid(model))
            {
                Client nveauClient = nouveauClient(model);
                db.Client.Add(nveauClient);
                if (db.SaveChanges() == 1)
                {
                   

                    var ident = new ClaimsIdentity(
                  new[] { 
                          // adding following 2 claim just for supporting default antiforgery provider
                          new Claim(ClaimTypes.NameIdentifier, model.Email),
                          new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                          new Claim(ClaimTypes.Name,model.Email),
                   },
                   DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(
                       new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Etape2");

                }
            }
            return View(model);
        }

        public bool modelIsValid(RegisterViewModel model)
        {
            foreach (Client c in db.Client)
            {
                if (c.email == model.Email)
                {
                    return false;
                }
            }
            if (model.Password == null || model.Prenom == null || model.Nom == null)
            {
                return false;
            }
            return true;
        }

        // GET: Commandes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        private Client getClientByMail(string mail)
        {
            return db.Client.Where(c => c.email == mail).First();
        }


        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commande commande = db.Commande.Find(id);
            db.Commande.Remove(commande);
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
