﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    
    
        public class HomeController : Controller
        {
            private static ModelDBContext db = new ModelDBContext();
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";

            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Index2()
        {
            ViewBag.IdOrari = new SelectList(db.Orari, "IdOrari", "OrariGrotte");
            ViewBag.IdGrotte = new SelectList(db.Grotte, "IdGrotte", "Nome");
           
            ViewBag.Title = "Home Page";
      
            ViewBag.Carrello = Session["Carrello"];
            return View(db.Grotte.ToList());
        }
        public ActionResult AddToCart( int IdGrotte, int Quantita, int IdOrari)
        {
            Grotte g = db.Grotte.Find(IdGrotte);
            Orari i = db.Orari.Find(IdOrari);
         
            Cart cartItem = new Cart(Quantita, g.Nome, g.Prezzo, IdGrotte,IdOrari, i.OrariGrotte);
            List<Cart> carrello = Session["Carrello"] as List<Cart> ?? new List<Cart>();
            carrello.Add(cartItem);
            Session["Carrello"] = carrello;
            return RedirectToAction("Index2", "Home");
        }
        public ActionResult Remove(int id)
        {
            List<Cart> carrello = Session["Carrello"] as List<Cart>;
            carrello.RemoveAt(id);
            Session["Carrello"] = carrello;
            return RedirectToAction("Index2", "Home");
        }
        public ActionResult ChiSiamo()
        {
            return View();
        }
        

    }
}
