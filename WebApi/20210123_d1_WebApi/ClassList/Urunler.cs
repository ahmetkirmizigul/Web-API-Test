using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20210123_d1_WebApi.ClassList
{
    public class Urunler
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Tedarikci { get; set; }
        public string Kategori { get; set; }
        public decimal? Fiyat { get; set; }
        public short? Stok { get; set; }
    }
}