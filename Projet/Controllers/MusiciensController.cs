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
    public class MusiciensController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Musiciens
        public ActionResult Index()
        {
            var musicien = db.Musicien.Include(m => m.Genre).Include(m => m.Instrument).Include(m => m.Pays);
            return View(musicien.ToList());
        }
        public ActionResult InitialDetails()
        {
            var musicien = db.Musicien.Include(m => m.Genre).Include(m => m.Instrument).Include(m => m.Pays).Where(m=> m.Nom_Musicien.StartsWith("B") );
            return View(musicien.ToList());
        }

        // GET: Musiciens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musicien musicien = db.Musicien.Find(id);
            if (musicien == null)
            {
                return HttpNotFound();
            }
            return View(musicien);
        }
        //GET: ListeCompositeurs
        public ActionResult ListeCompositeurs()
        {
            var musicien = db.Musicien.Include(m => m.Genre).Include(m => m.Instrument).Include(m => m.Pays).
                Join(db.Composer, 
                mus => mus.Code_Musicien, 
                comp => comp.Code_Musicien, 
                (mus, comp) => new { Musicien = mus, Composer = comp });

            return View(musicien.ToList());
        }

    }
}
