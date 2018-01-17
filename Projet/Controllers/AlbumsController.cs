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
    public class AlbumsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Albums
        public ActionResult Index()
        {
            var album = db.Album.Include(a => a.Editeur).Include(a => a.Genre);
            return View(album.ToList());
        }
        public ActionResult Pochette(int? id)
        {
            var album = db.Album.Single(g => g.Code_Album == id);
            return File(album.Pochette, "image/jpeg");
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            var data = db.Musicien.Single(g => g.Code_Musicien == id);
            ViewBag.Nom = data.Nom_Musicien;
            ViewBag.Prenom = data.Prenom_Musicien;

            return View(album);
        }
        // GET: Albums/listeAlbums/5
        public ActionResult listeAlbums(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = (from a in db.Album
                         join gen in db.Genre on a.Code_Genre equals gen.Code_Genre
                         join mus in db.Musicien on gen.Code_Genre equals mus.Code_Genre
                         join comp in db.Composer on mus.Code_Musicien equals comp.Code_Musicien
                         where mus.Code_Musicien == id
                         select a).Distinct();
            

            var data = db.Musicien.Single(g => g.Code_Musicien == id);
            ViewBag.Nom = data.Nom_Musicien;
            ViewBag.Prenom = data.Prenom_Musicien;
            ViewBag.Code = data.Code_Musicien;

            return View(album.ToList());

        }
        // GET: Albums/listeAlbumsRecherche

        public ActionResult listeAlbumsRecherche(string alb)
        {
            if (alb == null)
                alb = "";

            var album = (from a in db.Album
                         join gen in db.Genre on a.Code_Genre equals gen.Code_Genre
                         join mus in db.Musicien on gen.Code_Genre equals mus.Code_Genre
                         join comp in db.Composer on mus.Code_Musicien equals comp.Code_Musicien
                         where a.Titre_Album.StartsWith(alb)
                         select a).Distinct();

            return View(album.ToList());

        }

        public ActionResult listeAlbumsFromOeuvres(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = (from oe in db.Oeuvre
                           join comp in db.Composer on oe.Code_Oeuvre equals comp.Code_Oeuvre
                           join mus in db.Musicien on comp.Code_Musicien equals mus.Code_Musicien
                           join pay in db.Pays on mus.Code_Pays equals pay.Code_Pays
                           join edi in db.Editeur on pay.Code_Pays equals edi.Code_Pays
                           join alb in db.Album on edi.Code_Editeur equals alb.Code_Editeur
                           where oe.Code_Oeuvre == id
                           select alb).Distinct();
            var data = db.Album.Single(g => g.Code_Album == id);
            ViewBag.Titre_Album = data.Titre_Album;
            ViewBag.Code = id;
            return View(album);
        }
    }

    }
