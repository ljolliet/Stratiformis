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
    }

      
}
