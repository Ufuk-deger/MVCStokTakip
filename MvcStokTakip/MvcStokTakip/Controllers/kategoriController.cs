using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokTakip.Controllers
{
    public class kategoriController : Controller
    {
        // GET: kategori
        mvcSiteEntities modelEntity = new mvcSiteEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var kategoriler = modelEntity.tabloKategoriler.ToList();
            var degerler = modelEntity.tabloKategoriler.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult yeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult yeniKategori(tabloKategoriler kategoriEkle)
        {
            if (!ModelState.IsValid)
            {
                return View("yeniKategori");
            }
            modelEntity.tabloKategoriler.Add(kategoriEkle);
            modelEntity.SaveChanges();
            return View();
        }
        public ActionResult kategoriSil(int id)
        {
            var kategori = modelEntity.tabloKategoriler.Find(id);
            modelEntity.tabloKategoriler.Remove(kategori);
            modelEntity.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult kategoriGuncelle(int id)
        {
            var ktgGun = modelEntity.tabloKategoriler.Find(id);
            return View("kategoriGuncelle", ktgGun);
        }
        public ActionResult Guncelle(tabloKategoriler p1)
        {
            var ktg = modelEntity.tabloKategoriler.Find(p1.kategoriId);
            ktg.kategoriAd = p1.kategoriAd;
            modelEntity.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}