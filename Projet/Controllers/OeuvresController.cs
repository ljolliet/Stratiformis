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
    public class OeuvresController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Oeuvres
        public ActionResult Index()
        {
            var oeuvre = db.Oeuvre.Include(o => o.Type_Morceaux);
            return View(oeuvre.ToList());
        }

        // GET: Oeuvres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oeuvre oeuvre = db.Oeuvre.Find(id);
            if (oeuvre == null)
            {
                return HttpNotFound();
            }

            var data = (from o in db.Oeuvre
                         join comp in db.Composer on o.Code_Oeuvre equals comp.Code_Oeuvre
                         join mus in db.Musicien on comp.Code_Musicien equals mus.Code_Musicien
                         where o.Code_Oeuvre == id
                         select mus).Single();

            ViewBag.Nom = data.Nom_Musicien;
            ViewBag.Prenom = data.Prenom_Musicien;

            return View(oeuvre);
        }
        public ActionResult ListeOeuvresFromCompositeur(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var oeuvre = (from o in db.Oeuvre
                          join comp in db.Composer on o.Code_Oeuvre equals comp.Code_Oeuvre
                          join mus in db.Musicien on comp.Code_Musicien equals mus.Code_Musicien
                          where mus.Code_Musicien == id
                          select o).Distinct();

            var data = db.Musicien.Single(g => g.Code_Musicien == id);
            ViewBag.Nom = data.Nom_Musicien;
            ViewBag.Prenom = data.Prenom_Musicien;
            ViewBag.Code = data.Code_Musicien;

            return View(oeuvre.ToList());
        }

        public ActionResult ListeOeuvresRecherche(string data)
        {
            if (data == null)
                data = "";

            var oeuvre = (from o in db.Oeuvre
                          join comp in db.Composer on o.Code_Oeuvre equals comp.Code_Oeuvre
                          join mus in db.Musicien on comp.Code_Musicien equals mus.Code_Musicien
                          where o.Titre_Oeuvre.StartsWith(data)
                          select o).Include(o => o.Type_Morceaux);
            return View(oeuvre.ToList());
        }

        public ActionResult ListeOeuvresFromAlbum(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var oeuvres = (from oe in db.Oeuvre
                                  join comp in db.Composer on oe.Code_Oeuvre equals comp.Code_Oeuvre
                                  join mus in db.Musicien on comp.Code_Musicien  equals mus.Code_Musicien
                                  join gen in db.Genre on mus.Code_Genre equals gen.Code_Genre
                                  join alb in db.Album on gen.Code_Genre equals alb.Code_Genre
                                  where alb.Code_Album == id
                                  select oe).Distinct();
            var data = db.Album.Single(g => g.Code_Album== id);
            ViewBag.Titre_Album = data.Titre_Album;
            ViewBag.Code = id;
            return View(oeuvres);
        }
    }

        

      
}
