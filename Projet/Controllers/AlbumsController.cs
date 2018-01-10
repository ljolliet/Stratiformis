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
        public ActionResult Photo(int? id)
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
            return View(album);
        }
        public ActionResult listeAlbums(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = (from a in db.Album
                         join gen in db.Genre on a.Code_Genre equals gen.Code_Genre
                         join mus in db.Musicien on gen.Code_Genre equals mus.Code_Genre
                         where mus.Code_Musicien == id
                         select a);
            return View(album);

        }

    }
}
