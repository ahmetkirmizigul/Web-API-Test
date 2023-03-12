using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using _20210123_d1_WebApi.ClassList;
using _20210123_d1_WebApi.Models;

namespace _20210123_d1_WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        Entities db = new Entities();

        [HttpGet]
        public List<Urunler> List()
        {
            return db.Products.Select(x => new Urunler
            {
                Id = x.ProductID,
                Ad = x.ProductName,
                Fiyat = x.UnitPrice,
                Kategori = x.Categories.CategoryName,
                Stok = x.UnitsInStock,
                Tedarikci = x.Suppliers.CompanyName
            }).ToList();
        }

        [HttpPost]
        public IHttpActionResult Ekle(Products urun)
        {
            db.Products.Add(urun);
            try
            {
                db.SaveChanges();
                return Ok(new ResultMessage()
                { type = true, Message = "Ürün kayıt işlemi başarı ile tamamlanmıştır." });
            }
            catch (Exception)
            {
                return Ok(new ResultMessage()
                { type = false, Message = "Ürün kayıt işlemi esnasında bir hata meydana geldi." });
            }
        }

        [HttpGet]
        public Urunler Detay(int id)
        {

            Urunler veri = null;

            var urun = db.Products.Find(id);
            if (urun != null)
            {
                veri = new Urunler();
                veri.Id = urun.ProductID;
                veri.Ad = urun.ProductName;
                veri.Fiyat = urun.UnitPrice;
                veri.Kategori = urun.CategoryID.ToString();
                veri.Stok = urun.UnitsInStock;
                veri.Tedarikci = urun.SupplierID.ToString();
            }

            return veri;
        }

        [HttpPost]
        public IHttpActionResult Duzenle(Products urun)
        {
            var dbUrun = db.Products.Find(urun.ProductID);
            if (dbUrun != null)
            {
                dbUrun.ProductName = urun.ProductName;
                dbUrun.CategoryID = urun.CategoryID;
                dbUrun.SupplierID = urun.SupplierID;
                dbUrun.UnitPrice = urun.UnitPrice;
                dbUrun.UnitsInStock = urun.UnitsInStock;

                try
                {
                    db.SaveChanges();
                    return Ok(new ResultMessage()
                        { type = true, Message = "Ürün güncelleme işlemi başarı ile tamamlanmıştır." });
                }
                catch (Exception e)
                {
                    return Ok(new ResultMessage()
                        { type = false, Message = "Ürün güncelleme işlemi esnasında bir hata meydana geldi." });
                }
            }
            else
            {
                return Ok(new ResultMessage()
                    { type = false, Message = "Belirtilen ürüne ait güncelleme işlemi yapılamadı." });
            }
        }

        [HttpGet]
        public IHttpActionResult Sil(int id)
        {
            var urun = db.Products.Find(id);

            try
            {
                db.Products.Remove(urun);
                db.SaveChanges();
                return Ok(new ResultMessage()
                    { type = true, Message = "Ürün silme işlemi başarı ile tamamlanmıştır." });
            }
            catch (Exception e)
            {
                return Ok(new ResultMessage()
                    { type = true, Message = "Ürün silme işlemi esnasında bir hata meydana geldi." });
            }
        }
    }
}
