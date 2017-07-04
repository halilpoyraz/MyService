using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MyService
{
    /// <summary>
    /// Summary description for ProductionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductionService : System.Web.Services.WebService
    {
        public Kullanici user = new Kullanici();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        NorthwindDataContext nw = new NorthwindDataContext();

        [WebMethod]
        [SoapHeader("user")]
        public List<Urun> GetAllProducts()
        {
            if (user.KullaniciAdi=="Halil" && user.Parola=="123")
            {
                return nw.Products.Select(x => new Urun { UrunId = x.ProductID, UrunAdi = x.ProductName, Fiyat = x.UnitPrice, Stok = x.UnitsInStock }).ToList();
            }
            else
            {
                throw new NullReferenceException("Kullanıcı Adı ve Parola Doğrulanamadı !");
            }
            
        }

        [WebMethod]
        public List<Urun> GetProductsWithCategoryName(string KategoriAdi)
        {
            int katid = nw.Categories.FirstOrDefault(x => x.CategoryName == KategoriAdi).CategoryID;
            return nw.Products.Where(urun => urun.CategoryID == katid).Select(x => new Urun { UrunId = x.ProductID, UrunAdi = x.ProductName, Fiyat = x.UnitPrice, Stok = x.UnitsInStock }).ToList();
        }
    }

    public class Kullanici : SoapHeader
    {
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
    }
    public class Urun
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal? Fiyat { get; set; }
        public short? Stok { get; set; }
    }
}
