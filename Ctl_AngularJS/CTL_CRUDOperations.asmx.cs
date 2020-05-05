using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Ctl_AngularJS
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    

   
    public class CTL_CRUDOperations : System.Web.Services.WebService
    {
        
        [WebMethod]
        public string YeniKullaniciOlustur(string kullaniciAdi, string kullaniciSifre)
        {

            // Kullanıcı için tablo açar ve doldurur
            int deg;
            int deg2 = 0;
            string cmdTableKontrol = "select* from " + kullaniciAdi;
            using (SqlConnection conn = new SqlConnection(ConnectionStrings.connectionString))
            {
                // alttaki ilk try-catch girilmek istenen kullanici adı daha önce var mı diye bakar. yani o isimde bir tablo arar. bulursa catch değil try çalışır yani "deg" int değeri "1" olur. sonra uyarı verilir. eğer girilen kullanıcı adında bir tablo yoksa o zaman kod hata verir ve catch e düşer. ama bu durum istenen durumdur ve bu isimde bir kullanıcı oluşturulur. ikinci try-catch bloğu ise kayıt esnasında bir sorun yaşanırsa diye var. fakat şimdilik catch bloğu boş. kulanıcı isim veya veya şifre boş girilme kontrolu ise ikinci try-catch bloğunun try kısmında en üstte yapılır. çünkü kullanıcı adı veya şifre boş geçme durumlaru bir hata olarak görülmüyor.
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdTableKontrol, conn);                  
                    deg = (Int32)cmd.ExecuteScalar(); // bu kod ilk sütunun ilk satırını getirir.
                    //Response.Write("<script>alert('Bu isimde bir kullanıcı mevcut, lütfen başka bir isim deneyiniz!')</script>");               
                }
                catch (Exception)
                {
                  
                    //Veri tabanında kullanıcı için tablo açar===================================
                    string yeniTabloOlusturmaKodu = "create table " + kullaniciAdi + "(id int not null primary key identity(1,1), kullaniciAdi nvarchar(50),kullaniciId int,yil int, ay int,gun int, CtlKol1 int, CtlKol2 int, CtlKol6 bit, CtlKol7 bit, CtlKol3 int, CtlKol4 int,CtlKol5 int, aktifDonemMi bit)";
                    SqlCommand cmdYeniTabloOlustur = new SqlCommand(yeniTabloOlusturmaKodu, conn);
                    cmdYeniTabloOlustur.ExecuteNonQuery();

                    //Kullanici bilgilerini KullaniciBilgileri tablosuna kaydeder
                    string kullaniciBilgileriniKaydetmeKodu = "INSERT INTO KullaniciBilgileri (KullaniciAdi,Sifre,ip,oturumDurumu,kol1,kol2,kol3,kol4,kol5,kol6,kol7) VALUES('" + kullaniciAdi + "','" + kullaniciSifre + "','" + ZiyaretciIPsi + "',1,'kal1','kal2','kal3','kal4','kal5','kal6','kal7')";
                    SqlCommand cmdKullaniciKayitKomutu = new SqlCommand(kullaniciBilgileriniKaydetmeKodu, conn);
                    cmdKullaniciKayitKomutu.ExecuteNonQuery();

                    //KullaniciBilgileri Tablosundan kullaniciId si alınıp kullanıcının tablosuna basılır
                    SqlConnection con1 = new SqlConnection(ConnectionStrings.connectionString);
                    SqlCommand cmdKullaniciIdGetir = new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='" + kullaniciAdi + "' ", con1);
                    con1.Open();
                    SqlDataAdapter adpData = new SqlDataAdapter(cmdKullaniciIdGetir);
                    DataTable dt = new DataTable();
                    adpData.Fill(dt);                  
                    int kullaniciId = (int)dt.Rows[0][0];
                    con1.Close();
                    //kullanıcı oluşturulduğunda ilk dönem oluşturulur (bulunulan aya göre oluşturulur)
                    int secilenAy = DateTime.Now.Month;

                    switch (secilenAy)
                    {
                        case 1:
                            deg2 = 31;
                            break;
                        case 3:
                            deg2 = 31;
                            break;
                        case 5:
                            deg2 = 31;
                            break;
                        case 7:
                            deg2 = 31;
                            break;
                        case 8:
                            deg2 = 31;
                            break;
                        case 10:
                            deg2 = 31;
                            break;
                        case 12:
                            deg2 = 31;
                            break;
                        case 2:
                            deg2 = 28;
                            break;
                        case 4:
                            deg2 = 30;
                            break;
                        case 6:
                            deg2 = 30;
                            break;
                        case 9:
                            deg2 = 30;
                            break;
                        case 11:
                            deg2 = 30;
                            break;
                    }
                    for (int i = 1; i <= deg2; i++)
                    {
                        string yeniTabloyaVeriGrimeKodu = "INSERT INTO " + kullaniciAdi + "(kullaniciAdi,kullaniciId,yil,ay,gun,CtlKol1,CtlKol2,CtlKol6,CtlKol7,CtlKol3,CtlKol4,CtlKol5,aktifDonemMi)" + "VALUES('" + kullaniciAdi + "'," + kullaniciId + "," + DateTime.Now.Year + ",'" + DateTime.Now.Month + "','" + i + "',0,0,0,0,0,0,0,1)";

                        SqlCommand yeniTabloyuDoldur = new SqlCommand(yeniTabloyaVeriGrimeKodu, conn);
                        yeniTabloyuDoldur.ExecuteNonQuery();
                    }
                    
                }
                finally
                {
                    if (conn.State==ConnectionState.Open)    
                    conn.Close();
                }
                return "";
                

            }
        }
      
        [WebMethod]
        public void CtlBasliklariListele( string KullaniciAdi)
        {
            List<CtlKolonIsimsleri> CtlBasliklarListe = new List<CtlKolonIsimsleri>();

            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
                SqlCommand cmdBaslikalriListele =new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='"+KullaniciAdi+"'", con);
                SqlDataAdapter adpData = new SqlDataAdapter(cmdBaslikalriListele);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    CtlKolonIsimsleri kol = new CtlKolonIsimsleri();
                    kol.baslikKol1 = item["kol1"].ToString();
                    kol.baslikKol2 = item["kol2"].ToString();
                    kol.baslikKol3 = item["kol3"].ToString();
                    kol.baslikKol4 = item["kol4"].ToString();
                    kol.baslikKol5 = item["kol5"].ToString();
                    kol.baslikKol6 = item["kol6"].ToString();
                    kol.baslikKol7 = item["kol7"].ToString();

                    CtlBasliklarListe.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(CtlBasliklarListe));

                con.Close();
            }
        }

        [WebMethod]
        public void CtlListele(string kullaniciAdi)
        {
            List<CTLKolonlar> CtlListe = new List<CTLKolonlar>();
            try
            {
                SqlConnection con5 = new SqlConnection(ConnectionStrings.connectionString);
                con5.Open();

                SqlCommand cmd = new SqlCommand("select * from "+kullaniciAdi+" where aktifDonemMi=1", con5);
                SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    CTLKolonlar kol = new CTLKolonlar();
                    kol.kullaniciAdi = item["kullaniciAdi"].ToString();
                    kol.gun = Convert.ToInt32(item["gun"]);
                    kol.kol1= Convert.ToInt32(item["CtlKol1"]);
                    kol.kol2 = Convert.ToInt32(item["CtlKol2"]);
                    kol.kol3 = Convert.ToInt32(item["CtlKol3"]);
                    kol.kol4 = Convert.ToInt32(item["CtlKol4"]);
                    kol.kol5 = Convert.ToInt32(item["CtlKol5"]);
                    kol.kol6 = Convert.ToBoolean(item["CtlKol6"]);
                    kol.kol7 = Convert.ToBoolean(item["CtlKol7"]);

                    CtlListe.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(CtlListe));
                con5.Close();
            }
            catch (Exception)
            {

                //Response.Write("<script>alert('Açmak istediğiniz dönemi lütfen tekrar kontrol ediniz')</script>");
                //Response.Redirect("GirisSayfasi.aspx");
            }
        }

        [WebMethod]
        public void TabloDegerleriniKaydet(string kullaniciAdi,int deg1, int deg2, int deg3, int deg4, int deg5, bool deg6, bool deg7,int gun)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
                string sqlQuery = " UPDATE " + kullaniciAdi + " SET [CtlKol1] = @k1, [CtlKol2]=@k2, [CtlKol3]=@k3, [CtlKol4]=@k4, [CtlKol5]=@k5, [CtlKol6]=@k6, [CtlKol7]=@k7 where gun="+gun;
                SqlCommand guncelle = new SqlCommand(sqlQuery, con);
                //Add the parameters needed for the SQL query
                guncelle.Parameters.AddWithValue("@k1", deg1);
                guncelle.Parameters.AddWithValue("@k2",deg2);
                guncelle.Parameters.AddWithValue("@k3",deg3);
                guncelle.Parameters.AddWithValue("@k4",deg4);
                guncelle.Parameters.AddWithValue("@k5",deg5);
                guncelle.Parameters.AddWithValue("@k6",deg6);
                guncelle.Parameters.AddWithValue("@k7",deg7);
                guncelle.ExecuteNonQuery();
                con.Close();
            }
            CtlListele(kullaniciAdi);
        }

        string ZiyaretciIPsi;
        public void ipAlmaMetodu()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ZiyaretciIPsi = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ZiyaretciIPsi = HttpContext.Current.Request.UserHostAddress;
            }
        }


    }
}
