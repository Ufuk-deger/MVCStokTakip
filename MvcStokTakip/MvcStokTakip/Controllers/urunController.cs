using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;

namespace MvcStokTakip.Controllers
{
    public class urunController : Controller
    {
        // GET: urun
        mvcSiteEntities urunlerModel = new mvcSiteEntities();
        public ActionResult Index()
        {
            var degerler = urunlerModel.tabloUrunler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult urunEkle()
        {
            List<SelectListItem> comboBoxDoldurma = (from i in urunlerModel.tabloKategoriler.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.kategoriAd,
                                                         Value = i.kategoriId.ToString()
                                                     }).ToList();
            ViewBag.cmbDoldur = comboBoxDoldurma;
            return View();
        }
        [HttpPost]
        public ActionResult urunEkle(tabloUrunler urunEkleme)
        {
            var kategori = urunlerModel.tabloKategoriler.Where(x => x.kategoriId == urunEkleme.tabloKategoriler.kategoriId).FirstOrDefault();
            urunEkleme.tabloKategoriler = kategori;
            urunlerModel.tabloUrunler.Add(urunEkleme);
            urunlerModel.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult urunSil(int id)
        {
            var urun = urunlerModel.tabloUrunler.Find(id);
            urunlerModel.tabloUrunler.Remove(urun);
            urunlerModel.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult urunGetir(int id)
        {
            var urun = urunlerModel.tabloUrunler.Find(id);
            List<SelectListItem> comboBoxDoldurma = (from i in urunlerModel.tabloKategoriler.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.kategoriAd,
                                                         Value = i.kategoriId.ToString()
                                                     }).ToList();
            ViewBag.cmbDoldur = comboBoxDoldurma;
            return View("urunGetir", urun);
        }
        public ActionResult urunGuncelle(tabloUrunler p1)
        {
            var urunG = urunlerModel.tabloUrunler.Find(p1.urunId);
            urunG.urunAd = p1.urunAd;
            //urunG.urunKategori = p1.urunKategori;
            urunG.stok = p1.stok;
            urunG.marka = p1.marka;
            urunG.fiyat = p1.fiyat;
            var kategori = urunlerModel.tabloKategoriler.Where(x => x.kategoriId == p1.tabloKategoriler.kategoriId).FirstOrDefault();
            urunG.urunKategori = kategori.kategoriId;
            urunlerModel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}