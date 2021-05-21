using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;

namespace MvcStokTakip.Controllers
{
    public class musteriController : Controller
    {
        // GET: musteri
        mvcSiteEntities musteriModel = new mvcSiteEntities();
        public ActionResult Index()
        {
            var musteriler = musteriModel.tabloMusteriler.ToList();
            return View(musteriler);
        }
        [HttpGet]
        public ActionResult musteriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult musteriEkle(tabloMusteriler musteriEkleme)
        {
            if (!ModelState.IsValid)
            {
                return View("musteriEkle");
            }
            musteriModel.tabloMusteriler.Add(musteriEkleme);
            musteriModel.SaveChanges();
            return View();
        }
        public ActionResult musteriSil(int id)
        {
            var musteri = musteriModel.tabloMusteriler.Find(id);
            musteriModel.tabloMusteriler.Remove(musteri);
            musteriModel.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult musteriGetir(int id)
        {
            var musteriBilgi = musteriModel.tabloMusteriler.Find(id);
            return View("musteriGetir", musteriBilgi);
        }
        public ActionResult musteriGuncelle(tabloMusteriler p1)
        {
            var musteriGuncelleme = musteriModel.tabloMusteriler.Find(p1.musteriId);
            musteriGuncelleme.musteriAd = p1.musteriAd;
            musteriGuncelleme.musteriSoyad = p1.musteriSoyad;
            musteriModel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}