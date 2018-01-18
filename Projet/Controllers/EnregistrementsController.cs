using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet;

namespace Projet.Controllers
{
    public class EnregistrementsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Enregistrements
        public ActionResult Index()
        {
            return View(db.Enregistrement.ToList());
        }
        public ActionResult Extrait(int? id)
        {
            var enregistrement = db.Enregistrement.Single(g => g.Code_Morceau == id);
            return File(enregistrement.Extrait, "mp3");
        }
        public ActionResult listeEnregistrementsFromAlbum(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enregistrement = (from enr in db.Enregistrement
                                  join comp in db.Composition_Disque on enr.Code_Morceau equals comp.Code_Morceau
                                  join dis in db.Disque on comp.Code_Disque equals dis.Code_Disque
                                  join alb in db.Album on dis.Code_Album equals alb.Code_Album
                          where alb.Code_Album == id
                          select enr).Distinct();
          //  var data = db.Album.Single(g => g.Code_Album == id);
           // ViewBag.Titre_Album = data.Titre_Album;
            ViewBag.Code = id; 
            return View(enregistrement);
        }

        public ActionResult listeEnregistrementsFromOeuvres(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enregistrement = (from enr in db.Enregistrement
                                  join comp in db.Composition_Oeuvre on enr.Code_Composition  equals comp.Code_Composition
                                  join oe in db.Oeuvre on comp.Code_Oeuvre equals oe.Code_Oeuvre                                  
                                  where oe.Code_Oeuvre == id
                                  select enr).Distinct();
            //  var data = db.Oeuvre.Single(g => g.Code_Oeuvre == id);
            //ViewBag.Titre_Album = data.Titre_Oeuvre;
            ViewBag.Code = id;
            return View(enregistrement);
        }
        public ActionResult panier()
        {
            //if (!Request.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Account");
            //}



            if (Session["Panier"] == null)
            {
                Session["Panier"] = new List<int>();
            }

            List<int> mylist = new List<int>((List<int>)Session["Panier"]);

            List<Enregistrement> panier = new List<Enregistrement>();

            foreach (var item in mylist)
            {
                panier.Add(db.Enregistrement.Find(item));

            }

            return View(panier);
        }
        [Route("ajoutPanier/{id,code,fromAlbum}")]
        public ActionResult ajoutPanier(int? id, int code, bool fromAlbum)
        {
            if (Session["Panier"] == null)
            {
                Session["Panier"] = new List<int>();
            }

            var items = (List<int>)Session["Panier"];
            items.Add(id.Value);
            Session["Panier"] = items;
            ViewBag.Panier = Session["Panier"];

            string chemin;

            if (fromAlbum)
            {
                chemin = "ListeEnregistrementsFromAlbum/" + code;
            }
            else
            {
                chemin = "ListeEnregistrementsFromOeuvres/" + code;
            }

            return RedirectToAction(chemin);
        }
    }
}
