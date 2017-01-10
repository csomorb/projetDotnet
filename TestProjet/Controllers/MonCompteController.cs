using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProjet.Models;

namespace TestProjet.Controllers
{
    [Authorize]
    public class MonCompteController : Controller
    {

        private EcommerceEntities _bdd = new EcommerceEntities();


        // GET: MonCompte
        public ActionResult Index()
        {        
            Client c = getClientByMail(User.Identity.Name);
            ViewBag.nbCoordLivraison = _bdd.Coordonnees_de_livraison.Count(co => co.id_client == c.id);
            ViewBag.listeCoord = _bdd.Coordonnees_de_livraison.Where(co => co.id_client == c.id);
            ViewBag.nbCommande = _bdd.Commande.Count(cm => cm.id_utilisateur == c.id);
            ViewBag.listeCommande = _bdd.Commande.Where(cm => cm.id_utilisateur == c.id).OrderBy(cm => cm.date_commande);
            return View(c);
        }


        // GET: Cahnger mot de passe
        public ActionResult Changepass()
        {
            return View();
        }

        public ActionResult DeleteAcount()
        {
            Client c = getClientByMail(User.Identity.Name);
            return View(c);
        }

        // POST: Changee de mot de pass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            } 
            Client c = getClientByMail(User.Identity.Name);
            if (c != null)
            {
                c.mot_de_passe = model.NewPassword;
                _bdd.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        private Client getClientByMail(string mail)
        {
            return _bdd.Client.Where(c => c.email == mail).First();
        }


    }
}
