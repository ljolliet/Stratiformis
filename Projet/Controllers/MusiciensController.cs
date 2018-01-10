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
            var musicien = db.Musicien.Include(m => m.Genre).Include(m => m.Pays);
            return View(musicien.ToList());
        }
        public ActionResult Photo(int id)
        {
            var music = db.Musicien.Single(g => g.Code_Musicien == id);
            return File(music.Photo, "image/jpeg");
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
        public ActionResult ListeCompositeurs(string compo)
        {      
            
            if(compo == null)            
                compo = "";
              
            var musicien = (from m in db.Musicien
                            where m.Nom_Musicien.StartsWith(compo)
                            join comp in db.Composer
                            on m.Code_Musicien equals comp.Code_Musicien
                            select  m).Distinct();
            return View(musicien.ToList());
        }

   

    }
}
