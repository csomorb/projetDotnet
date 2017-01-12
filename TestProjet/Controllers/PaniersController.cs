﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestProjet.Models;
using System.Collections;


namespace TestProjet.Controllers

{
    public class PaniersController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();


        // GET: Paniers
        public ActionResult Index()
        {
         //   Produit prod = new Produit();
         //   ViewBag.monPanier = db.Panier.ToList();
         //   ViewBag.monProduit = db.Produit.ToList();
            //   ViewBag.monProduit2 = db.Produit.All();
            Client c = getClientByMail(User.Identity.Name);
       /*     ViewBag.contenuDuPanier = db.Panier.Where(p => p.id_client == c.id).Join(db.Produit, panier => panier.id_produit, produit => produit.id , (panier, produit ) => new {
                             produitID  = produit.id ,
                             poduitNom = produit.nom_produit ,
                             panierID = panier.id } ).ToList();*/
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
                resultat.Add(monItem);
                prixTotal = prixTotal + int.Parse(item.produitPrix);
            }

            /*   Array tab
               foreach (var item in bbb)

                   expando.Add( item.panierID.ToString(), item);*/
            ViewBag.prixPanier = prixTotal;
            ViewBag.contenuDuPanier = resultat;

            /*var a = (from p in db.Produit join pa in db.Panier on p.id equals pa.id_produit where pa.id_client == c.id
                     select p
                );*/
            return View(db.Panier.ToList());
        }

        // GET: Paniers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = db.Panier.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            return View(panier);
        }

        // GET: Paniers/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: Paniers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int prod_id, int prod_qte)
        {
            Panier panier = new Panier();
            //on modifie l'id_client avec celui du client enregistré
            Client c = getClientByMail(User.Identity.Name);
            int id_client = c.id;
            panier.id_client = id_client;
            panier.id_produit = prod_id;
            panier.quantite = prod_qte;

            if (ModelState.IsValid)
            {
                db.Panier.Add(panier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(panier);
        }

        // GET: Paniers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = db.Panier.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            return View(panier);
        }

        // POST: Paniers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_client,id_produit,quantite")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(panier);
        }

        // GET: Paniers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = db.Panier.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            return View(panier);
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Panier panier = db.Panier.Find(id);
            db.Panier.Remove(panier);
            db.SaveChanges();
            return RedirectToAction("Index");
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