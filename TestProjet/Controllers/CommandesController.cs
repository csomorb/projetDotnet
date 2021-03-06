﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections;
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

        public int prixPanier()
        {
            Client c = getClientByMail(User.Identity.Name);
            var listepa = db.Panier.Where(p => p.id_client == c.id).Join(db.Produit, panier => panier.id_produit, produit => produit.id, (panier, produit) => new {
                produitPrix = produit.prix,
                panierQTE = panier.quantite,
            });
            int prix = 0;
            foreach (var pa in listepa)
            {
                prix = prix +  ( int.Parse(pa.produitPrix) * pa.panierQTE);
            }
            return prix;

        }

        public ActionResult Etape1()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Etape2");
            }
            ViewBag.montant = prixPanier();
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
            ViewBag.montant = prixPanier();
            ViewBag.listeCoord = db.Coordonnees_de_livraison.Where(co => co.id_client == c.id);
            return View();

        }

        [HttpPost]
        public ActionResult Etape2(Commande model)
        {
            return RedirectToAction("Etape3","Commandes", new { id_livraison = model.id_livraison } );
        }

        public ActionResult Etape3(int? id_livraison)
        {
            if (id_livraison == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = new Commande();
            commande.id_livraison = (int)id_livraison;
            ViewBag.montant = prixPanier();
            ViewBag.mode_livraison = db.Methode_de_livraison.ToList();
            return View(commande);
        }


        [HttpPost]
        public ActionResult Etape3(Commande model)
        {
            
            return Etape4(model);
        }

        public ActionResult Etape4(Commande model)
        {
            ViewBag.montant = prixPanier();
            return View("Etape4",model);
        }

        public ActionResult Confirmation(Commande model)
        {
            Client c = getClientByMail(User.Identity.Name);

            var bbb = db.Panier.Where(p => p.id_client == c.id).Join(db.Produit, panier => panier.id_produit, produit => produit.id, (panier, produit) => new {
                produitID = produit.id,
                poduitNom = produit.nom_produit,
                produitImage = produit.image,
                produitPrix = produit.prix,
                panierQTE = panier.quantite,
                panierID = panier.id
            });

            int prixTotal = 0;

            ArrayList resultat = new ArrayList();
            foreach (var item in bbb)
            {
                ArrayList monItem = new ArrayList();
                monItem.Add(item.panierID);
                monItem.Add(item.poduitNom);
                monItem.Add(item.produitID);
                monItem.Add(item.produitImage);
                monItem.Add(item.produitPrix);
                monItem.Add(item.panierQTE);
                int res_tmp = item.panierQTE * int.Parse(item.produitPrix);
                monItem.Add(res_tmp);
                resultat.Add(monItem);
                prixTotal = prixTotal + int.Parse(item.produitPrix);
            }
           
            ViewBag.contenuDuPanier = resultat;


            model.id_utilisateur = c.id;
            model.date_commande = DateTime.Now.ToString();
            model.date_expedition = "";
            model.date_livraison = "";
            model.etat = "Envoyé";
            model.fret = "A calculer"; // a calculer :  le prix de la commande
            double fret = 0.0;
            switch(model.id_methode_livraison)
            {
                case 1 : fret = 5 ;
                    prixTotal += 5;
                    break;
                case 2 : fret = 10 ;
                    prixTotal += 10;
                    break;
                case 3: fret = 8;
                    prixTotal += 8;
                    break;
                default:
                    prixTotal += 7;
                    fret = 7;
                    break; 
            } 
            db.Commande.Add(model);
            db.SaveChanges();
            ViewBag.prixPanier = prixTotal;
            // pour chaque produit du panier
            foreach (var pan in db.Panier.Where(p => p.id_client == c.id))
            {
                Produit pro = db.Produit.Find(pan.id_produit);
                // diminuer la quantite en stock pour chaque produit achete
                pro.stock = pro.stock - pan.quantite;
                // ajout du détail de la commande
                Detail_commande det = new Detail_commande();
                det.quantite = pan.quantite;
                det.id_produit = pan.id_produit;
                det.prix_unit = pro.prix;
                fret += det.quantite * double.Parse(det.prix_unit);
                det.id_commande = model.id;
                // ajout du détaille de commande à la bdd
                db.Detail_commande.Add(det);
                // retirer les éléments du panier
                db.Panier.Remove(pan);
                // sauvegarder les changements
                //db.SaveChanges();
            }
            model.fret = fret.ToString();
            db.SaveChanges();
            ViewBag.client = c;
            ViewBag.adresse = db.Coordonnees_de_livraison.Find(model.id_livraison);
            return View(model);
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
            Client c = getClientByMail(User.Identity.Name);


            var ccc = db.Detail_commande.Where(co => co.id_commande == commande.id).Join(db.Produit, detail_commande => detail_commande.id_produit, produit => produit.id,
                (detail_commande, produit) => new
                {
                    produitID = produit.id,
                    poduitNom = produit.nom_produit,
                    produitImage = produit.image,
                    produitPrix = produit.prix,
                    panierQTE = detail_commande.quantite,
                    panierID = detail_commande.id_commande
                });

            int prixTotal = 0;

            ArrayList resultat = new ArrayList();
            foreach (var item in ccc)
            {
                ArrayList monItem = new ArrayList();
                monItem.Add(item.panierID);
                monItem.Add(item.poduitNom);
                monItem.Add(item.produitID);
                monItem.Add(item.produitImage);
                monItem.Add(item.produitPrix);
                monItem.Add(item.panierQTE);
                int res_tmp = item.panierQTE * int.Parse(item.produitPrix);
                monItem.Add(res_tmp);
                resultat.Add(monItem);
                prixTotal = prixTotal + int.Parse(item.produitPrix);
            }

            ViewBag.contenuDuPanier = resultat;

            ViewBag.client = c;
            ViewBag.adresse = db.Coordonnees_de_livraison.Find(commande.id_livraison);

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

        private Client getClientByMail(string mail)
        {
            return db.Client.Where(c => c.email == mail).First();
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
