using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet;

namespace Projet.Views.Home
{
    public class EnregistrementsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Enregistrements
        public ActionResult Index()
        {
            return View(db.Enregistrement.ToList());
        }

        // GET: Enregistrements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            if (enregistrement == null)
            {
                return HttpNotFound();
            }
            return View(enregistrement);
        }

        // GET: Enregistrements/listeEnregistrements/5
        public ActionResult listeEnregistrements(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enregistrements = (from en in db.Enregistrement
                                  join compo in db.Composition_Disque on en.Code_Morceau equals compo.Code_Morceau
                                  join dis in db.Disque on compo.Code_Disque equals dis.Code_Disque
                                  join a in db.Album on dis.Code_Album equals a.Code_Album
                                  where a.Code_Album == id
                                  select en);
            var data = db.Album.Single(g => g.Code_Album == id);
            ViewBag.Titre_Album = data.Titre_Album;

            return View(enregistrements);
        }

    }
}
