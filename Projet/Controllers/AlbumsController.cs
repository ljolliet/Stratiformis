﻿using System;
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
        public ActionResult Extrait(int? id)
        {
            var enregistrement = db.Enregistrement.Single(g => g.Code_Morceau == id);
            return File(enregistrement.Extrait, "mp3");
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

            return View(album.ToList());

        }
        // GET: Albums/listeAlbumsRecherche/5

        public ActionResult listeAlbumsRecherche(string alb)
        {
            if (alb == null)
                alb = "";

            var album = (from a in db.Album
                         join gen in db.Genre on a.Code_Genre equals gen.Code_Genre
                         join mus in db.Musicien on gen.Code_Genre equals mus.Code_Genre
                         where a.Titre_Album.StartsWith(alb)
                         select a);

            return View(album.ToList());

        }

    }
}
