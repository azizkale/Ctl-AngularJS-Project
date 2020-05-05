using Ctl_AngularJS.Grup_CTL;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
//using System.Net;
//using System.Web.Http.Cors;
//using System.Web.Http;

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
        public void YeniGiristeMukerrerKontrolu(string ad)
        {
            using (SqlConnection con8 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con8.Open();
                //kullanıcı adına table var mı kontrolu
                SqlCommand cmd = new SqlCommand(@"IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table) SELECT 1 ELSE SELECT 0", con8);

                cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = ad;
                int exists = (int)cmd.ExecuteScalar();

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(exists));
                con8.Close();
            }
        }

       
        [WebMethod]
        public void TabloKontrolu(string kullaniciAdi)
        {
            using (SqlConnection con9 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con9.Open();
                //giriş yapan kullanıcı mevcut değilse tablo varlık sorgusu 0 olarak döner
                SqlCommand cmd = new SqlCommand(@"IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table) SELECT 1 ELSE SELECT 0", con9);

                cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = kullaniciAdi;
                int exists = (int)cmd.ExecuteScalar();

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(exists));

                con9.Close();
            }
        }

        [WebMethod]
        public void GirisFonksiyonu(string kullaniciAdi, int sifre)
        {
            using (SqlConnection con12 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con12.Open();
                try
                {
                    //Giriş yapan kullanıcının kayıtlı olup olmadığı sorgulanır.
                    List<string> kullanicibilgisi = new List<string>();
                    SqlCommand cmd = new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='" + kullaniciAdi + "' and Sifre='" + sifre + "'", con12);
                    SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpData.Fill(dt);
                    kullanicibilgisi.Add(dt.Rows[0]["KullaniciAdi"].ToString());
                    kullanicibilgisi.Add(dt.Rows[0]["Sifre"].ToString());                    

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(js.Serialize(kullanicibilgisi));

                    con12.Close();
                }
                catch (Exception)
                {
                    con12.Close();
                }            
            }         
        }       

        [WebMethod]/*grup başlıkları için farklı metot var. çünkü farklı tablolardan çekiliyorlar*/
        public void CtlBasliklariListele( string KullaniciAdi, string ctlAdi)
        {
            List<CtlKolonIsimsleri> CtlBasliklarListe = new List<CtlKolonIsimsleri>();

            using (SqlConnection con7 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con7.Open();
                SqlCommand cmdBaslikalriListele =new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='"+KullaniciAdi+"' and CtlAdi='"+ctlAdi+"'", con7);
                SqlDataAdapter adpData = new SqlDataAdapter(cmdBaslikalriListele);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    CtlKolonIsimsleri kol = new CtlKolonIsimsleri();
                    kol.KullaniciAdi = item["KullaniciAdi"].ToString();
                    kol.ay = Convert.ToInt32(item["ay"]);
                    kol.yil = Convert.ToInt32(item["yil"]);
                    kol.baslikKol1 = item["kol1"].ToString();
                    kol.baslikKol2 = item["kol2"].ToString();
                    kol.baslikKol3 = item["kol3"].ToString();
                    kol.baslikKol4 = item["kol4"].ToString();
                    kol.baslikKol5 = item["kol5"].ToString();
                    kol.baslikKol6 = item["kol6"].ToString();
                    kol.baslikKol7 = item["kol7"].ToString();
                    kol.baslikKol8 = item["kol8"].ToString();
                    kol.baslikKol9 = item["kol9"].ToString();
                    kol.baslikKol10 = item["kol10"].ToString();

                    kol.baslikKol1Type = item["kolt1"].ToString();
                    kol.baslikKol2Type = item["kolt2"].ToString();
                    kol.baslikKol3Type = item["kolt3"].ToString();
                    kol.baslikKol4Type = item["kolt4"].ToString();
                    kol.baslikKol5Type = item["kolt5"].ToString();
                    kol.baslikKol6Type = item["kolt6"].ToString();
                    kol.baslikKol7Type = item["kolt7"].ToString();
                    kol.baslikKol8Type = item["kolt8"].ToString();
                    kol.baslikKol9Type = item["kolt9"].ToString();
                    kol.baslikKol10Type = item["kolt10"].ToString();

                    CtlBasliklarListe.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(CtlBasliklarListe));

                con7.Close();
            }
        }

        [WebMethod]
        public void GrupCtlBasliklariListele(string KullaniciAdi, string grupAdi)
        {
            List<GrupCTLBaslikIsimleri> CtlBasliklarListe = new List<GrupCTLBaslikIsimleri>();

            using (SqlConnection con11 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con11.Open();
                SqlCommand cmdGrupCtlBaslikalriListele = new SqlCommand("select * from GrupBilgileri where grupYoneticisi='" + KullaniciAdi + "' and grupAdi='" + grupAdi + "'", con11);
                SqlDataAdapter adpData = new SqlDataAdapter(cmdGrupCtlBaslikalriListele);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    GrupCTLBaslikIsimleri kol = new GrupCTLBaslikIsimleri();
                    kol.gKol1 = item["gKol1"].ToString();
                    kol.gKol2 = item["gKol2"].ToString();
                    kol.gKol3 = item["gKol3"].ToString();
                    kol.gKol4 = item["gKol4"].ToString();
                    kol.gKol5 = item["gKol5"].ToString();
                    kol.gKol6 = item["gKol6"].ToString();
                    kol.gKol7 = item["gKol7"].ToString();
                    kol.gKol8 = item["gKol8"].ToString();
                    kol.gKol9 = item["gKol9"].ToString();
                    kol.gKol10 = item["gKol10"].ToString();

                    kol.gKoltType1 = item["gKolt1"].ToString();
                    kol.gKoltType2 = item["gKolt2"].ToString();
                    kol.gKoltType3 = item["gKolt3"].ToString();
                    kol.gKoltType4 = item["gKolt4"].ToString();
                    kol.gKoltType5 = item["gKolt5"].ToString();
                    kol.gKoltType6 = item["gKolt6"].ToString();
                    kol.gKoltType7 = item["gKolt7"].ToString();
                    kol.gKoltType8 = item["gKolt8"].ToString();
                    kol.gKoltType9 = item["gKolt9"].ToString();
                    kol.gKoltType10 = item["gKolt10"].ToString();

                    kol.grupAdi = item["grupAdi"].ToString();
                    Convert.ToDateTime(item["donemBaslangic"]).ToString("dd/MM/yyyy");
                    kol.donemBaslangic = Convert.ToDateTime(item["donemBaslangic"]).ToString("dd/MM/yy");
                    kol.donemBitis = Convert.ToDateTime(item["donemBitis"]).ToString("dd/MM/yy");

                    CtlBasliklarListe.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(CtlBasliklarListe));

                con11.Close();
            }
        }

        [WebMethod]
        public void UyeOlunanGrupCtlBasliklariListele(string uyeAdi, string grupAdi)
        {
            List<GrupCTLBaslikIsimleri> UyeOlunanGrupCtlBasliklarListe = new List<GrupCTLBaslikIsimleri>();

            using (SqlConnection con20 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con20.Open();
                SqlCommand cmdUyeOlunanGrupCtlBaslikalriListele = new SqlCommand("select * from GrupKatilimciBilgileri where katilanAdi='" + uyeAdi + "' and grupAdi='" + grupAdi + "'", con20);
                SqlDataAdapter adpData7 = new SqlDataAdapter(cmdUyeOlunanGrupCtlBaslikalriListele);
                DataTable dt7 = new DataTable();
                adpData7.Fill(dt7);

                foreach (DataRow item in dt7.Rows)
                {
                    GrupCTLBaslikIsimleri kol = new GrupCTLBaslikIsimleri();
                    kol.gKol1 = item["gKol1"].ToString();
                    kol.gKol2 = item["gKol2"].ToString();
                    kol.gKol3 = item["gKol3"].ToString();
                    kol.gKol4 = item["gKol4"].ToString();
                    kol.gKol5 = item["gKol5"].ToString();
                    kol.gKol6 = item["gKol6"].ToString();
                    kol.gKol7 = item["gKol7"].ToString();
                    kol.gKol8 = item["gKol8"].ToString();
                    kol.gKol9 = item["gKol9"].ToString();
                    kol.gKol10 = item["gKol10"].ToString();

                    kol.gKoltType1 = item["gKolt1"].ToString();
                    kol.gKoltType2 = item["gKolt2"].ToString();
                    kol.gKoltType3 = item["gKolt3"].ToString();
                    kol.gKoltType4 = item["gKolt4"].ToString();
                    kol.gKoltType5 = item["gKolt5"].ToString();
                    kol.gKoltType6 = item["gKolt6"].ToString();
                    kol.gKoltType7 = item["gKolt7"].ToString();
                    kol.gKoltType8 = item["gKolt8"].ToString();
                    kol.gKoltType9 = item["gKolt9"].ToString();
                    kol.gKoltType10 = item["gKolt10"].ToString();

                    kol.grupAdi = item["grupAdi"].ToString();
                    Convert.ToDateTime(item["donemBaslangic"]).ToString("dd/MM/yyyy");
                    kol.donemBaslangic = Convert.ToDateTime(item["donemBaslangic"]).ToString("dd/MM/yy");
                    kol.donemBitis = Convert.ToDateTime(item["donemBitis"]).ToString("dd/MM/yy");

                    UyeOlunanGrupCtlBasliklarListe.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(UyeOlunanGrupCtlBasliklarListe));

                con20.Close();
            }
        }
        
        string[] types;
        [WebMethod]
        public void CtlListele(string kullaniciAdi, string ctlAdi)
        {
            List<CTLKolonlar> CtlListe = new List<CTLKolonlar>();

            SqlConnection con5 = new SqlConnection(ConnectionStrings.connectionString);
                con5.Open();

            //Şahsi CTL kolon type ları gelir
            SqlCommand cmdSahsiBaslikTypes = new SqlCommand("select kolt1,kolt2,kolt3,kolt4,kolt5,kolt6,kolt7,kolt8,kolt9,kolt10 from KullaniciBilgileri where KullaniciAdi='" + kullaniciAdi + "' and CtlAdi='" + ctlAdi + "'", con5);
            SqlDataAdapter adpSAhsiDataBasliklar = new SqlDataAdapter(cmdSahsiBaslikTypes);
            DataTable dtSAhsiBasliklar = new DataTable();
            adpSAhsiDataBasliklar.Fill(dtSAhsiBasliklar);

            //Grup CTL kolon type ları gelir
            SqlCommand cmdSGrupBaslikTypes = new SqlCommand("select gKolt1,gKolt2,gKolt3,gKolt4,gKolt5,gKolt6,gKolt7,gKolt8,gKolt9,gKolt10 from GrupBilgileri where grupYoneticisi='" + kullaniciAdi + "' and grupAdi='" + ctlAdi + "'", con5);
            SqlDataAdapter adpDataGrupBasliklar = new SqlDataAdapter(cmdSGrupBaslikTypes);
            DataTable dtGrupBasliklar = new DataTable();
            adpDataGrupBasliklar.Fill(dtGrupBasliklar);

            //kolon type larını dizi yapar
            if (ctlAdi != "")
            {
                types = new string[10];
                for (int i = 0; i < 10; i++)
                {
                    if (dtSAhsiBasliklar.Rows.Count != 0 )
                        types[i] = dtSAhsiBasliklar.Rows[0][i].ToString(); //listelenecek CTL şahsiCtl ise doldurulur

                    if (dtGrupBasliklar.Rows.Count != 0)
                        types[i] = dtGrupBasliklar.Rows[0][i].ToString();  //listelenecek CTL grupCtl ise doldurulur
                }
            }
           
            //kolon değerleri gelir
            SqlCommand cmd = new SqlCommand("select * from " + kullaniciAdi + " where aktifDonemMi=1 and ctlAdi='" + ctlAdi + "'", con5);
                SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    CTLKolonlar kol = new CTLKolonlar();
                    kol.kullaniciAdi = item["kullaniciAdi"].ToString();
                    kol.kullaniciId = Convert.ToInt32(item["kullaniciId"]);
                    kol.ctlAdi = item["ctlAdi"].ToString();
                    kol.gun = Convert.ToInt32(item["gun"]);
                    kol.ay = Convert.ToInt32(item["ay"]);
                    kol.yil = Convert.ToInt32(item["yil"]);
                    kol.ctlTuru = item["ctlTuru"].ToString();

                //geçici kolon dizisi - dynamic type olan esas kolonlar bu dizi aracışığı ile table da gözükecek gerçek typlarını alır.
                object[] kolonlar = { kol.kol1, kol.kol2, kol.kol3, kol.kol4, kol.kol5, kol.kol6, kol.kol7, kol.kol8, kol.kol9, kol.kol10 };

                for (int i = 0; i < kolonlar.Length; i++)
                {
                    if (types[i] == "int")
                        kolonlar[i] = Convert.ToInt32(item["CtlKol" + (i + 1)]);
                    else if (types[i] == "bit")
                    {
                        if ((int)item["CtlKol" + (i + 1)] == 1)
                            kolonlar[i] = true;
                        if ((int)item["CtlKol" + (i + 1)] == 0)
                            kolonlar[i] = false;
                    }
                    else if (types[i] == "")
                        kolonlar[i] = null;
                }

                kol.kol1 = kolonlar[0];
                kol.kol2 = kolonlar[1];
                kol.kol3 = kolonlar[2];
                kol.kol4 = kolonlar[3];
                kol.kol5 = kolonlar[4];
                kol.kol6 = kolonlar[5];
                kol.kol7 = kolonlar[6];
                kol.kol8 = kolonlar[7];
                kol.kol9 = kolonlar[8];
                kol.kol10 = kolonlar[9];             

                CtlListe.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(CtlListe));
                con5.Close();           
        }

        public int deg;
        public int deg2;
        [WebMethod]
        public void YeniDonemOlustur(string kullaniciAdi, int ay, int yil, string ctlAdi)
        {
            // kullanıcı adına olan tabloda açılmak istenen dönem ay ve güne göre kontrol edilir. eğer mevcutsa (Int32)cmd.ExecuteScalar() değeri sıfırdan farklı olarak döner ve if kontrolü ile bu dönemin açılması engellenir. eğer açılmak istenen dönem yok ise (Int32)cmd.ExecuteScalar() komutu hata veriyor ve catch bloğuna düşüyor. Catch bloğunda istenen dönemi açma kod ları var. yani istenen bir hata ile elde edilmiş oluyor.

            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                try
                {
                    deg2 = 0;
                    SqlCommand cmd = new SqlCommand("select * from " + kullaniciAdi + " where ctlAdi="+ctlAdi+" and ay=" + ay + " and yil=" + yil + "", con);
                    con.Open();
                    deg2 = (Int32)cmd.ExecuteScalar();
                    if (deg2 != 0)
                    {
                        //string dtr = "<script>alert('Bu dönem zaten mevcut!!!!!!')</script>";
                    }

                }
                catch (Exception)
                {

                    //seçilen aya göre gün sayısını belirler
                    switch (ay.ToString())
                    {
                        case "1":
                            deg = 31;
                            break;
                        case "3":
                            deg = 31;
                            break;
                        case "5":
                            deg = 31;
                            break;
                        case "7":
                            deg = 31;
                            break;
                        case "8":
                            deg = 31;
                            break;
                        case "10":
                            deg = 31;
                            break;
                        case "12":
                            deg = 31;
                            break;
                        case "2":
                            deg = 28;
                            break;
                        case "4":
                            deg = 30;
                            break;
                        case "6":
                            deg = 30;
                            break;
                        case "9":
                            deg = 30;
                            break;
                        case "11":
                            deg = 30;
                            break;
                    }

                    //Açılan Dönem KulllaniciBilgileri Tablosuna kayıt edilir
                    //önce ilgili CTL nin Bilgileri KullaniciBilgilerinden getirlir
                    SqlCommand cmdCTLBilgileriGetir = new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='"+kullaniciAdi+"' and CtlAdi='"+ctlAdi+"'",con);
                    SqlDataAdapter adpctl = new SqlDataAdapter(cmdCTLBilgileriGetir);
                    DataTable dtctl = new DataTable();
                    adpctl.Fill(dtctl);

                    //Yeni Dönem kullanicı Bilgilerine kaydedilir
                    SqlCommand cmdYeniDonemKByeKayit = new SqlCommand("insert into KullaniciBilgileri (KullaniciAdi,Sifre,ip,CtlAdi,ay,yil,ctlTuru,kol1,kolt1,kol2,kolt2,kol3,kolt3,kol4,kolt4,kol5,kolt5,kol6,kolt6,kol7,kolt7,kol8,kolt8,kol9,kolt9,kol10,kolt10) values('"+kullaniciAdi+ "','"+dtctl.Rows[0]["Sifre"].ToString()+ "','" + dtctl.Rows[0]["ip"].ToString() + "','" + ctlAdi + "','"+ay+"','"+yil+"','sahsi','"+ dtctl.Rows[0]["kol1"].ToString() + "','" + dtctl.Rows[0]["kolt1"].ToString() + "','" + dtctl.Rows[0]["kol2"].ToString() + "','" + dtctl.Rows[0]["kolt2"].ToString() + "','" + dtctl.Rows[0]["kol3"].ToString() + "','" + dtctl.Rows[0]["kolt3"].ToString() + "','" + dtctl.Rows[0]["kol4"].ToString() + "','" + dtctl.Rows[0]["kolt4"].ToString() + "','" + dtctl.Rows[0]["kol5"].ToString() + "','" + dtctl.Rows[0]["kolt5"].ToString() + "','" + dtctl.Rows[0]["kol6"].ToString() + "','" + dtctl.Rows[0]["kolt6"].ToString() + "','" + dtctl.Rows[0]["kol7"].ToString() + "','" + dtctl.Rows[0]["kolt7"].ToString() + "','" + dtctl.Rows[0]["kol8"].ToString() + "','" + dtctl.Rows[0]["kolt8"].ToString() + "','" + dtctl.Rows[0]["kol9"].ToString() + "','" + dtctl.Rows[0]["kolt9"].ToString() + "','" + dtctl.Rows[0]["kol10"].ToString() + "','" + dtctl.Rows[0]["kolt10"].ToString() + "')", con);
                    cmdYeniDonemKByeKayit.ExecuteNonQuery();


                    //tüm dönemler pasif yapılır
                    string donemPasifEt = " UPDATE " + kullaniciAdi + " SET aktifDonemMi = 0";
                    SqlCommand guncelle2 = new SqlCommand(donemPasifEt, con);
                    guncelle2.ExecuteNonQuery();                    

                    //KullaniciBilgileri Tablosundan kullaniciId si alınıp kullanıcının tablosuna basılır
                    SqlConnection con1 = new SqlConnection(ConnectionStrings.connectionString);
                    SqlCommand cmdKullaniciIdGetir = new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='" + kullaniciAdi + "' and CtlAdi='"+ctlAdi+"' and ay="+ay+" and yil="+yil+" ", con1);
                    con1.Open();
                    SqlDataAdapter adpData = new SqlDataAdapter(cmdKullaniciIdGetir);
                    DataTable dt = new DataTable();
                    adpData.Fill(dt);
                    int kullaniciId = (int)dt.Rows[0][0];
                    con1.Close();

                    //yeni dönemi açar, veri tabanına ekler, açılan dönemi aktif yapar
                    for (int i = 1; i <= deg; i++)
                    {
                        string yeniDonemAcSqlCmnd = "INSERT INTO " + kullaniciAdi + "(kullaniciAdi,ctlAdi,kullaniciId,yil,ay,gun,CtlKol1,CtlKol2,CtlKol6,CtlKol7,CtlKol3,CtlKol4,CtlKol5,aktifDonemMi,ctlTuru)" + "VALUES('" + kullaniciAdi + "','"+ctlAdi+"'," + kullaniciId + "," + yil + ",'" + ay + "','" + i + "',0,0,0,0,0,0,0,1,'sahsi')";
                        SqlCommand cmdYeniDonem = new SqlCommand(yeniDonemAcSqlCmnd, con);
                        cmdYeniDonem.ExecuteNonQuery();
                    }

                }
                finally
                {
                    con.Close();
                }
            }
        }

        [WebMethod]
        public void TabloDegerleriniKaydet(string kullaniciAdi, string ctlAdi, object deg1, object deg2, object deg3, object deg4, object deg5, object deg6, object deg7, object deg8, object deg9, object deg10, object gun)
        {
            object[] kol ={deg1, deg2, deg3, deg4, deg5, deg6, deg7, deg8, deg9, deg10};

            for (int i = 0; i < kol.Length; i++)
            {
                if (kol[i] != null)
                {
                    string srt = kol[i].ToString();
                    if (srt == "True") kol[i] = 1;
                    else if (srt == "False") kol[i] = 0;
                    else kol[i] = Convert.ToInt32(srt);

                }
                else
                    kol[i] = DBNull.Value;
            }

            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
               
                SqlCommand guncelle = new SqlCommand("UPDATE " + kullaniciAdi + " SET[CtlKol1] = @k1, [CtlKol2] = @k2, [CtlKol3] = @k3, [CtlKol4] = @k4, [CtlKol5] = @k5, [CtlKol6] = @k6, [CtlKol7] = @k7, [CtlKol8] = @k8, [CtlKol9] = @k9, [CtlKol10] = @k10 where aktifDonemMi = 1 and ctlAdi = '" + ctlAdi + "' and gun = " + gun, con);


                guncelle.Parameters.AddWithValue("@k1", kol[0]);
                guncelle.Parameters.AddWithValue("@k2", kol[1]);
                guncelle.Parameters.AddWithValue("@k3", kol[2]);
                guncelle.Parameters.AddWithValue("@k4", kol[3]);
                guncelle.Parameters.AddWithValue("@k5", kol[4]);
                guncelle.Parameters.AddWithValue("@k6", kol[5]);
                guncelle.Parameters.AddWithValue("@k7", kol[6]);
                guncelle.Parameters.AddWithValue("@k8", kol[7]);
                guncelle.Parameters.AddWithValue("@k9", kol[8]);
                guncelle.Parameters.AddWithValue("@k10", kol[9]);
                guncelle.ExecuteNonQuery();
                con.Close();
            }
        }
            
        [WebMethod]
        public void MevcutDonemleriListele(string kullaniciAdi, string ctlAdi)
        {
            List<CTLKolonlar> sahsiDonemlerListe = new List<CTLKolonlar>();
            try
            {
                SqlConnection con = new SqlConnection(ConnectionStrings.connectionString);
                con.Open();

                //kullanıcıya ait tablodan ay ve yıl arka planda bir tabloya alınır
                SqlCommand cmd = new SqlCommand("select distinct ay,yil from "+kullaniciAdi+" where ctlAdi='"+ctlAdi+"' and ctlTuru='sahsi'", con);
                SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpData.Fill(dt);
  
                //şahsi ctl ye ait dönemler listelenir
                foreach (DataRow item in dt.Rows)
                {
                    CTLKolonlar kol = new CTLKolonlar();

                    kol.ay = Convert.ToInt32(item["ay"]);
                    kol.yil = Convert.ToInt32(item["yil"]);
                   
                    sahsiDonemlerListe.Add(kol);
                }
              
                JavaScriptSerializer js = new JavaScriptSerializer();                
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(sahsiDonemlerListe));

                con.Close();

            }
            catch (Exception)
            {
                //Response.Redirect("GirisSayfasi.aspx");
            }
        }

        [WebMethod]
        public void DonemSec(string kullaniciAdi,string ctlAdi, int ay, int yil)
        {
            SqlConnection con14 = new SqlConnection(ConnectionStrings.connectionString);
            con14.Open();            
            
            //tüm dönemler pasif yapılır
            string donemPasifEt = " UPDATE " + kullaniciAdi + " SET aktifDonemMi = 0 where ctlAdi='"+ctlAdi+"'";
            SqlCommand guncelle2 = new SqlCommand(donemPasifEt, con14);
            guncelle2.ExecuteNonQuery();

            // seçili dönemi aktifDonem olarak günceller
            string donemAktifEt = "UPDATE " + kullaniciAdi + " SET aktifDonemMi = 1 where ctlAdi='"+ctlAdi+"' and ay = @ay and yil = @yil";
            SqlCommand guncelle = new SqlCommand(donemAktifEt, con14);
            guncelle.Parameters.AddWithValue("@ay", ay);
            guncelle.Parameters.AddWithValue("@yil", yil);
            guncelle.ExecuteNonQuery();

            con14.Close();
        }

        [WebMethod]
        public void GrupCtlBas(string yoneticiAdi, string ctlAdi)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();

                //ctlAdi na ait tüm dönemler pasif yapılır (kullanıcı tablosunda)(grup olduğu için zaten tek dönem var)
                string donemPasifEt = " UPDATE " + yoneticiAdi + " SET aktifDonemMi = 0 where ctlAdi='" + ctlAdi + "'";
                SqlCommand guncelle = new SqlCommand(donemPasifEt, con);
                guncelle.ExecuteNonQuery();

                //kullanıcı tablosu ve GrupBilgileri Tabloları birleştirilir(aktif edilecek CTL yi seçmek için)          
                SqlCommand cmdTabloBirlerstir = new SqlCommand("SELECT * FROM "+yoneticiAdi+" INNER JOIN GrupBilgileri ON "+yoneticiAdi+".kullaniciId = GrupBilgileri.id where "+yoneticiAdi+".ctlAdi = '"+ctlAdi+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmdTabloBirlerstir);
                DataTable dt11 = new DataTable();
                da.Fill(dt11);

                string ctl = dt11.Rows[0]["grupAdi"].ToString();

                //ilgili grupCTL aktif yapılır (ay yıl olarak 0 olanlar seçilir, grupların ay yılı olmadığı için)
                SqlCommand aktifEt = new SqlCommand("update "+yoneticiAdi+" set aktifDonemMi=1 where ctlAdi='"+ctl+"'",con);
                aktifEt.ExecuteNonQuery();                

                con.Close();
            }
        }

        [WebMethod]
        public void UyeOlunanGrupCtlBas(string uyeAdi, string ctlAdi)
        {
            using (SqlConnection con19 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con19.Open();
                //ctlAdi na ait tüm dönemler pasif yapılır (kullanıcı tablosunda)(grup olduğu için zaten tek dönem var)
                string donemPasifEt = " UPDATE " + uyeAdi + " SET aktifDonemMi = 0 where ctlAdi='" + ctlAdi + "'";
                SqlCommand cmdGuncelle = new SqlCommand(donemPasifEt, con19);
                cmdGuncelle.ExecuteNonQuery();

                //kullanıcı tablosu ve GrupKatilimciBilgileri Tabloları birleştirilir(aktif edilecek CTL yi seçmek için)
                SqlCommand cmdTabloBirlerstirr = new SqlCommand("SELECT * FROM " + uyeAdi + " INNER JOIN GrupKatilimciBilgileri ON " + uyeAdi + ".kullaniciId = GrupKatilimciBilgileri.katildigiGrupId where " + uyeAdi + ".ctlAdi = '" + ctlAdi + "'", con19);
                SqlDataAdapter da2 = new SqlDataAdapter(cmdTabloBirlerstirr);
                DataTable dt12 = new DataTable();
                da2.Fill(dt12);

                string ctl = dt12.Rows[0]["ctlAdi"].ToString();

                //ilgili grupCTL aktif yapılır (ay yıl olarak 0 olanlar seçilir, grupların ay yılı olmadığı için)
                SqlCommand aktifEt = new SqlCommand("update " + uyeAdi + " set aktifDonemMi=1 where ctlAdi='" + ctl + "'", con19);
                aktifEt.ExecuteNonQuery();

                con19.Close();
            }
        }

        [WebMethod]
        public void UpdateKontrolAdSifre(string kullaniciAdi)
        {
            using (SqlConnection con9 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con9.Open();

                //girilen ismin mevcut olup olmadığını kontrol eder
                List<string> kontrol = new List<string>();
                SqlCommand cmd = new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='" + kullaniciAdi + "'", con9);
                SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    kontrol.Add((item["KullaniciAdi"]).ToString());
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(kontrol));

                con9.Close();
              
            }
        }

        [WebMethod]
        public void UpdateAdSifre(string kullaniciAdi, string cookiedekiKullaniciAdi, int sifre)
        {
            using (SqlConnection con9 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con9.Open();
                //KullaniciBilgileri Tablosunda KullaniciAdi nı ve Şifresini günceller
                string kullaniciBilgileriGuncelleme = "UPDATE KullaniciBilgileri SET [KullaniciAdi] = @ka, [Sifre]=@sifre where KullaniciAdi='" + cookiedekiKullaniciAdi + "'";
                SqlCommand cmdKullaniciBilgileriGuncelle = new SqlCommand(kullaniciBilgileriGuncelleme, con9);
                cmdKullaniciBilgileriGuncelle.Parameters.AddWithValue("@ka", kullaniciAdi);
                cmdKullaniciBilgileriGuncelle.Parameters.AddWithValue("@sifre", sifre);
                cmdKullaniciBilgileriGuncelle.ExecuteNonQuery();

                //Kullanıcı adına olan tablodan kullaniciAdi nı günceller
                string KullaniciAdiniKullanicininTablosundaGuncelleme = "UPDATE " + cookiedekiKullaniciAdi + " SET [kullaniciAdi] = @ka2 where kullaniciAdi='" + cookiedekiKullaniciAdi + "'";
                SqlCommand cmdKullaniciAdiniKullanicininTablosundaGuncelleme = new SqlCommand(KullaniciAdiniKullanicininTablosundaGuncelleme, con9);
                cmdKullaniciAdiniKullanicininTablosundaGuncelleme.Parameters.AddWithValue("@ka2", kullaniciAdi);
                cmdKullaniciAdiniKullanicininTablosundaGuncelleme.ExecuteNonQuery();

                //Kullnaıcını adına olan Tablonun adını günceller
                string tabloAdiDegistir = "EXEC sp_rename " + cookiedekiKullaniciAdi + "," + kullaniciAdi + "";
                SqlCommand cmdTabloAdiDegistir = new SqlCommand(tabloAdiDegistir, con9);
                cmdTabloAdiDegistir.ExecuteNonQuery();

                con9.Close();
            }
        }

        [WebMethod]
        public void CtlBaslikUpdate(string baslik, string baslikAdi,string kullaniciAdi,string ctlAdi)
        {
            using (SqlConnection con9 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con9.Open();
                //Kalem Başlıkları güncelleme====================================
                string sqlQueryBaslikGuncelleme = "UPDATE KullaniciBilgileri SET ["+baslik+"] = @k1 where KullaniciAdi='" + kullaniciAdi + "' and CtlAdi='"+ctlAdi+"'";
                SqlCommand guncelle2 = new SqlCommand(sqlQueryBaslikGuncelleme, con9);
                guncelle2.Parameters.AddWithValue("@k1", baslikAdi);                
                guncelle2.ExecuteNonQuery();
                con9.Close();
            }
        }

        [WebMethod]
        public void GrupCtlBasliklariUpdate(string baslik, string baslikAdi, string kullaniciAdi,string ctlAdi)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
                //Kalem Başlıkları güncelleme====================================
                SqlCommand guncelle2 = new SqlCommand("UPDATE GrupBilgileri SET [" + baslik + "] = @k1 where grupAdi='" + ctlAdi + "' and grupYoneticisi='"+kullaniciAdi+"'", con);
                guncelle2.Parameters.AddWithValue("@k1", baslikAdi);
                guncelle2.ExecuteNonQuery();
                con.Close();
            }
        }

        [WebMethod]
        public void YeniCTLOlustur(string kullaniciAdi,string sifre, string ctlAdi,string[] kolonTurleri, string[] kolonAdlari)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings.connectionString))
            {
                conn.Open();
                //kullanıcı adına table var mı kontrolu
                SqlCommand cmd = new SqlCommand(@"IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table) SELECT 1 ELSE SELECT 0", conn);

                cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = kullaniciAdi;
                int exists = (int)cmd.ExecuteScalar();

                //kullanıcı adına tablo yoksa oluşturur
                if (exists == 0)
                {
                    string yeniTabloOlusturmaKodu = "create table " + kullaniciAdi + "(id int not null primary key identity(1,1), kullaniciAdi nvarchar(50),ctlAdi nvarchar(50),kullaniciId int,yil int, ay int,gun int, CtlKol1 int, CtlKol2 int, CtlKol3 int, CtlKol4 int, CtlKol5 int,CtlKol6 int, CtlKol7 int,CtlKol8 int,CtlKol9 int,CtlKol10 int, aktifDonemMi bit,ctlTuru nvarchar(25))";
                    SqlCommand cmdYeniTabloOlustur = new SqlCommand(yeniTabloOlusturmaKodu, conn);
                    cmdYeniTabloOlustur.ExecuteNonQuery();
                }
                                             

                //CTL yi KullaniciBilgileri tablosuna kaydeder
                string ctlKaydetmeKodu = "INSERT INTO KullaniciBilgileri (KullaniciAdi,Sifre,ip,CtlAdi,ay,yil,ctlTuru) VALUES('" + kullaniciAdi + "','" + sifre + "','" + ZiyaretciIPsi + "','" + ctlAdi + "',"+(int)DateTime.Now.Month+","+(int)DateTime.Now.Year+",'sahsi')";
                SqlCommand cmdCtlKayitKomutu = new SqlCommand(ctlKaydetmeKodu, conn);
                cmdCtlKayitKomutu.ExecuteNonQuery();

                    //ctl nin klonlarını ekler(KullaniciBilgileri ne)
                for (int i = 0; i < kolonAdlari.Length; i++)
                {
                    //kolon adını kaydeder
                    SqlCommand cmdKolonAdlariniVerme = new SqlCommand("update KullaniciBilgileri set kol" + (i+1) + "='" + kolonAdlari[i] + "' where KullaniciAdi='" + kullaniciAdi + "' and ctlAdi='"+ctlAdi+"' ", conn);
                    cmdKolonAdlariniVerme.ExecuteNonQuery();
                    //kolon türünü kaydeder
                    SqlCommand cmdKolonTuruKayit = new SqlCommand("update KullaniciBilgileri set kolt" + (i+1) + "='" + kolonTurleri[i] + "' where KullaniciAdi='" + kullaniciAdi + "' and ctlAdi='" + ctlAdi + "' ", conn);
                    cmdKolonTuruKayit.ExecuteNonQuery();
                }              

                //tüm dönemler pasif yapılır
                string donemPasifEt = " UPDATE " + kullaniciAdi + " SET aktifDonemMi = 0";
                SqlCommand guncelle2 = new SqlCommand(donemPasifEt, conn);
                guncelle2.ExecuteNonQuery();

                //KullaniciBilgileri Tablosundan kullaniciId si alınıp kullanıcının tablosuna basılır
                SqlConnection con1 = new SqlConnection(ConnectionStrings.connectionString);
                SqlCommand cmdKullaniciIdGetir = new SqlCommand("select * from KullaniciBilgileri where KullaniciAdi='" + kullaniciAdi + "' and CtlAdi='"+ctlAdi+"' and ay="+(int)DateTime.Now.Month+" and yil="+(int)DateTime.Now.Year+" ", con1);
                con1.Open();
                SqlDataAdapter adpData = new SqlDataAdapter(cmdKullaniciIdGetir);
                DataTable dt = new DataTable();
                adpData.Fill(dt);
                int kullaniciId = (int)dt.Rows[0][0];
                con1.Close();

                //Ctl oluşturulduğunda ilk dönem oluşturulur (bulunulan aya göre oluşturulur)
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
                for (int j = 1; j <= deg2; j++)
                {
                    SqlCommand yeniTabloyuDoldur = new SqlCommand("INSERT INTO " + kullaniciAdi + "(kullaniciAdi,ctlAdi,kullaniciId,yil,ay,gun,aktifDonemMi,ctlTuru) VALUES('" + kullaniciAdi + "','" + ctlAdi + "'," + kullaniciId + "," + DateTime.Now.Year + ",'" + DateTime.Now.Month + "','" + j + "',1,'sahsi')", conn);
                    yeniTabloyuDoldur.ExecuteNonQuery();

                    for (int m = 0; m < kolonAdlari.Length; m++)
                    {
                        string kolonAdi = "CtlKol" + (m+1);
                        SqlCommand yeniTablonunKolonlari = new SqlCommand("update "+kullaniciAdi+ " set " + kolonAdi + " = 0 where ctlAdi='"+ctlAdi+"'",conn);
                        yeniTablonunKolonlari.ExecuteNonQuery();
                    }
                  
                }
                conn.Close();
            }
           
        }

        [WebMethod]
        public void MevcutCtlleriListele(string kullaniciAdi)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings.connectionString))
            {
                conn.Open();
                //kullanıcının kişisel Ctl isimlerini getirir
                SqlCommand cmdMevcutCtlleriGetir = new SqlCommand("select distinct CtlAdi from KullaniciBilgileri where KullaniciAdi='"+kullaniciAdi+"'", conn);
                SqlDataAdapter adpData = new SqlDataAdapter(cmdMevcutCtlleriGetir);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                List<string> ctllistesi = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    ctllistesi.Add(item["CtlAdi"].ToString());
                }
                
                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(ctllistesi));

                conn.Close();
            }
        }

        [WebMethod]
        public void MevcutGrupCtlleriListele(string kullaniciAdi)
        {
            using (SqlConnection con=new SqlConnection(ConnectionStrings.connectionString) )
            {
                con.Open();
                List<string> ctlliste = new List<string>();

                //kullanıcının Grup Ctl isimlerini getirir
                SqlCommand cmdMevcutGrupCtlleri = new SqlCommand("select * from GrupBilgileri where grupYoneticisi='" + kullaniciAdi + "'", con);
                SqlDataAdapter adpData2 = new SqlDataAdapter(cmdMevcutGrupCtlleri);
                DataTable dt2 = new DataTable();
                adpData2.Fill(dt2);
                foreach (DataRow item in dt2.Rows)
                {
                    ctlliste.Add(item["grupAdi"].ToString());
                }              

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(ctlliste));

                con.Close();
            }
        }

        [WebMethod]
        public void MevcutUyeOlunanGrupCtlleriListele(string katilanAdi)
        {
            using (SqlConnection conn1 = new SqlConnection(ConnectionStrings.connectionString))
            {
                conn1.Open();
                List<string> uyeolunanctlliste = new List<string>();

                //kullanıcının üye olunanGrup Ctl isimlerini getirir
                SqlCommand cmdMevcutUyeOlunanGrupCtlleri = new SqlCommand("select * from GrupKatilimciBilgileri where katilanAdi='" + katilanAdi + "'", conn1);
                SqlDataAdapter adpData3 = new SqlDataAdapter(cmdMevcutUyeOlunanGrupCtlleri);
                DataTable dt3 = new DataTable();
                adpData3.Fill(dt3);
                foreach (DataRow item in dt3.Rows)
                {
                    uyeolunanctlliste.Add(item["katildigiGrupAdi"].ToString());
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(uyeolunanctlliste));

                conn1.Close();
            }               
        }

        [WebMethod]
        public void GrubBilgileriGetir(string grupAdi, string grupYoneticisi)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
                List<object> anaList = new List<object>();
                
                if (grupAdi!="")
                {
                    SqlCommand cmd = new SqlCommand("select grupAdi,id from GrupBilgileri where grupYoneticisi='" + grupYoneticisi + "'", con);
                    SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpData.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        List<string> kontrol = new List<string>();
                        kontrol.Add((item["grupAdi"]).ToString());
                        kontrol.Add((item["id"]).ToString());

                        anaList.Add(kontrol);
                    }
                }

                //grup adının boş gelmesi grupların hepsini listelemek için. yan menüde gruplarım buttonu altındadır
                if (grupAdi=="")
                {
                    SqlCommand cmd = new SqlCommand("select grupAdi,id from GrupBilgileri where grupYoneticisi='" + grupYoneticisi + "'", con);
                    SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpData.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        List<string> kontrol = new List<string>();
                        kontrol.Add((item["grupAdi"]).ToString());
                        kontrol.Add((item["id"]).ToString());

                        anaList.Add(kontrol);
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(anaList));

                con.Close();
            }
        }
        
        string ZiyaretciIPsi;
        int id;
        [WebMethod]
        public void YeniGrupAc(string kullaniciAdi, string grupAdi, string grupSifresi, string donemBaslangic,string donemBitis,string[] kolonlarTurleri,string[] kolonAdlari)
        { 
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ZiyaretciIPsi = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ZiyaretciIPsi = HttpContext.Current.Request.UserHostAddress;
            }
            
            using (SqlConnection conn = new SqlConnection(ConnectionStrings.connectionString))
            {
                try
                {
                    conn.Open();
                    //Grup bilgilerini GrupBilgileri tablosuna kaydeder(kolon haRİCİ BİLGİLER)
                    SqlCommand cmdGrubuGrupBilgileriTablosunaKaydet = new SqlCommand("INSERT INTO GrupBilgileri(grupAdi,grupSifre,grupYoneticisi,donemBaslangic,donemBitis,ip,GrupUyeSayisi,grupAktifDonem) VALUES('" + grupAdi + "','" + grupSifresi + "','" + kullaniciAdi + "','" + Convert.ToDateTime(donemBaslangic).ToString("dd/MM/yyyy") + "','" + Convert.ToDateTime(donemBitis).ToString("dd/MM/yyyy") + "','" + ZiyaretciIPsi + "',1,1)", conn);
                    cmdGrubuGrupBilgileriTablosunaKaydet.ExecuteNonQuery();
                  
                    //kolon bilgileri(ad ve type lar)
                    for (int i = 1; i <= kolonAdlari.Length; i++)
                    {
                        SqlCommand cmdKolonAdlariniKayit = new SqlCommand("update GrupBilgileri set gKol" + i + "='" + kolonAdlari[i-1] + "' where grupAdi='" + grupAdi + "' ", conn);
                        cmdKolonAdlariniKayit.ExecuteNonQuery();

                        SqlCommand cmdKolonTypeKayit = new SqlCommand("update GrupBilgileri set gKolt" + i + "='" + kolonlarTurleri[i-1] + "' where grupAdi='" + grupAdi + "' ", conn);
                        cmdKolonTypeKayit.ExecuteNonQuery();
                    }

                    //açılan grubun grup id sini GrupBilgileri tablosundan alır
                    SqlCommand cmdacilanGrubunGrupIdsiniAl = new SqlCommand("select * from GrupBilgileri where grupYoneticisi='"+kullaniciAdi+"' and grupAdi= '" + grupAdi + "'", conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdacilanGrubunGrupIdsiniAl);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    id = (int)dt.Rows[0][0];

                    //Grup adına tablo oluşturulur
                    SqlCommand cmdYeniGrupTabloOlustur = new SqlCommand("create table G_" +id+"_"+ grupAdi + "(id int not null primary key identity(1,1),uye nvarchar(25), statu nvarchar(10),uyeAktifMi bit,grupId int,donemBaslangic nvarchar(40), donemBitis nvarchar(40))", conn);
                    cmdYeniGrupTabloOlustur.ExecuteNonQuery();

                    //tablonun kolonları değişkenleri belirlenerek oluşturulur
                    for (int j = 0; j < kolonAdlari.Length; j++)
                    {
                        SqlCommand cmdGrupAdinaTabloyaKolonlariEkle = new SqlCommand("Alter Table G_" + id + "_" + grupAdi + " add " + kolonAdlari[j] + " "+kolonlarTurleri[j]+" ", conn);
                        cmdGrupAdinaTabloyaKolonlariEkle.ExecuteNonQuery();
                    }                  

                    //Grubu açanı açtığı gruba katar
                    SqlCommand cmdgrubuAcaniGrubaKaydet = new SqlCommand("insert into G_" + id + "_" + grupAdi + "(uye,statu,uyeAktifMi,grupId,donemBaslangic,donemBitis) values('" + kullaniciAdi + "','yonetici',1," + id + ",'" + donemBaslangic + "','" + donemBitis+ "')", conn);
                    cmdgrubuAcaniGrubaKaydet.ExecuteNonQuery();

                    //Grubu açanın kalemleri 0 olarak kayd edilir
                    foreach (string item in kolonAdlari)
                    {
                        SqlCommand cmdGrubuAcaninKalemleriniDoldurma = new SqlCommand("update G_" + id + "_" + grupAdi + " set "+item+"=0 ", conn);
                        cmdGrubuAcaninKalemleriniDoldurma.ExecuteNonQuery();
                    }

                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
        }

        [WebMethod] /*şimdilik atıl!!!!!!!!!!!!!!!*/
        public void GrubunCtlsiniOlustur(string grupYoneticisi, string kullaniciAdi, string grupAdi, string donemBaslangic, string donemBitis, string[] kolonAdlari)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
                //tüm dönemler pasif yapılır
                SqlCommand guncelle2 = new SqlCommand(" UPDATE " + kullaniciAdi + " SET aktifDonemMi = 0", con);
                guncelle2.ExecuteNonQuery();

                //GrupBilgileri Tablosundan kullaniciId si alınır
                SqlCommand cmdKullaniciIdGetir = new SqlCommand("select * from GrupBilgileri where grupAdi='"+ grupAdi + "' and grupYoneticisi='" + grupYoneticisi + "' ", con);
                SqlDataAdapter adpData3 = new SqlDataAdapter(cmdKullaniciIdGetir);
                DataTable dt3 = new DataTable();
                adpData3.Fill(dt3);
                int kullaniciId = (int)dt3.Rows[0][0];

                DateTime baslangic = Convert.ToDateTime(donemBaslangic);
                DateTime bitis = Convert.ToDateTime(donemBitis);

                int tarihFarki = ((int)((bitis - baslangic).TotalDays))+1;

                //GrupCtl oluşturulur
                //(kolon harici bilgiler)
                for (int i = 1; i <= tarihFarki; i++)
                {
                    SqlCommand yeniGrupCtlsiniDoldur = new SqlCommand("INSERT INTO " + kullaniciAdi + "(kullaniciAdi,ctlAdi,kullaniciId,yil,ay,gun,aktifDonemMi,ctlTuru)" + "VALUES('" + kullaniciAdi + "','" + grupAdi + "'," + kullaniciId + ",'-','-','" + i + "',1,'grup')", con);
                    yeniGrupCtlsiniDoldur.ExecuteNonQuery();
                }

                //kolonlar işlenir
                //for (int j = 1; j <= tarihFarki; j++)
                //{
                    for (int i = 1; i <= kolonAdlari.Length; i++)

                    {
                        SqlCommand cmdKolonlariDoldur = new SqlCommand("update " + kullaniciAdi + " set [CtlKol" + i + "] = 0 where ctlAdi='"+grupAdi+"' ", con);
                        cmdKolonlariDoldur.ExecuteNonQuery();
                    }
                //}

                con.Close();
            }
        }
       
        [WebMethod]
        public void GrupDogrulama(string kGrupId, string kGrupAdi, string kGrupSifresi)
        {
            using (SqlConnection con21 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con21.Open();               
                SqlCommand cmdGrupDogrulama = new SqlCommand("select * from GrupBilgileri where id="+kGrupId+" and grupAdi='" + kGrupAdi + "' and grupSifre='" + kGrupSifresi + "'", con21);
               cmdGrupDogrulama.ExecuteScalar(); //girilen isimde grup yoksa 0 döner

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(Convert.ToInt32(cmdGrupDogrulama.ExecuteScalar())));
                con21.Close();
            }
        }

        [WebMethod]
        public int BirGrubaKatil(string tabloAdi, string katilanAdi)
        {
            int deg; //üye kaydı varsa uyarı versin diye
            using (SqlConnection con20 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con20.Open();
                SqlCommand cmdUyeVarlikKontrol = new SqlCommand("select * from "+tabloAdi+" where uye='"+katilanAdi+"'" , con20);
                //gelen uye daha önce varmı yok mu kontrolü için
                deg = Convert.ToInt32(cmdUyeVarlikKontrol.ExecuteScalar());              
               
                if (Convert.ToInt32(cmdUyeVarlikKontrol.ExecuteScalar()) == 0)
                {
                    //kayıt yapılacaksa grupID,dönem baş. son. tarihleri ve kolon değerleri için
                    SqlCommand cmdGrupTemelBilgileri = new SqlCommand("select * from " + tabloAdi + "", con20);
                    SqlDataAdapter adpData5 = new SqlDataAdapter(cmdGrupTemelBilgileri);
                    DataTable dt5 = new DataTable();
                    adpData5.Fill(dt5);

                    SqlCommand cmdKatilanToGrupTablosu = new SqlCommand("insert into " + tabloAdi + " (uye,statu,uyeAktifMi,grupId,donemBaslangic,donemBitis) values('" + katilanAdi + "','uye',0,"+dt5.Rows[0]["grupId"]+",'"+dt5.Rows[0]["donemBaslangic"]+ "','" + dt5.Rows[0]["donemBitis"] + "')", con20);
                    cmdKatilanToGrupTablosu.ExecuteNonQuery();
                }
                con20.Close();
            }
            return deg;
        }
        
        [WebMethod]
        public void SahsiCTLSilmeListesi(string kullaniciAdi)
        {
            using (SqlConnection con19 = new SqlConnection(ConnectionStrings.connectionString))
            {                
                con19.Open();               
                SqlCommand cmdSahsiCtlSilmeListesi = new SqlCommand("select distinct kullaniciId,kullaniciAdi,ctlAdi,yil,ay,ctlTuru from " + kullaniciAdi + " where ctlTuru='sahsi'", con19);
                SqlDataAdapter adpData = new SqlDataAdapter(cmdSahsiCtlSilmeListesi);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                List<CtlKolonIsimsleri> liste = new List<CtlKolonIsimsleri>();
                foreach (DataRow item in dt.Rows)
                {
                    CtlKolonIsimsleri kol = new CtlKolonIsimsleri();
                    kol.kullaniciId = Convert.ToInt32(item["kullaniciId"]);
                    kol.KullaniciAdi = item["kullaniciAdi"].ToString();
                    kol.CtlAdi = item["ctlAdi"].ToString();
                    kol.yil = Convert.ToInt32(item["yil"].ToString());
                    kol.ay = Convert.ToInt32(item["ay"].ToString());
                    kol.ctlTuru = item["ctlTuru"].ToString();

                    liste.Add(kol);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(liste));                
               
                con19.Close();
            }
        }

        [WebMethod]
        public void SahsiCtlSil(string kullaniciAdi, int kullaniciId,string ctlAdi, int ay, int yil, string tur)
        {
            using (SqlConnection con15 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con15.Open();
                //ctl kullanıcı adına olan tablodan silinir
                SqlCommand cmdSahsiCtlSil = new SqlCommand("delete from "+kullaniciAdi+" where ctlAdi='"+ctlAdi+ "' and kullaniciId=" + kullaniciId + " and ay="+ay+" and yil="+yil+" and ctlTuru='"+tur+"'", con15);
                cmdSahsiCtlSil.ExecuteNonQuery();
                //ctl Kullanici Bilgileri tablosundan silinir
                SqlCommand cmdSahsiCtlKBdenSil = new SqlCommand("delete from KullaniciBilgileri where KullaniciAdi='"+kullaniciAdi+"' and ctlAdi='"+ctlAdi+"' and ay="+ay+" and yil="+yil+" and ctlTuru='"+tur+"' ", con15);
                cmdSahsiCtlKBdenSil.ExecuteNonQuery();
                con15.Close();
            }
        }

        [WebMethod]
        public void GrupCTLSilmeListesi(string kullaniciAdi)
        {           
                using (SqlConnection con16 = new SqlConnection(ConnectionStrings.connectionString))
                {
                    con16.Open();
              
                    //kullanıcı tablosundan bilgiler gelir
                    SqlCommand cmdGrupCtlSilmeListesi = new SqlCommand("select distinct kullaniciId,kullaniciAdi,ctlAdi,donemBaslangic,donemBitis,ctlTuru from " + kullaniciAdi + " kt join GrupBilgileri gb on kt.kullaniciId=gb.id", con16);
                    SqlDataAdapter adpData = new SqlDataAdapter(cmdGrupCtlSilmeListesi);
                    DataTable dt = new DataTable();
                    adpData.Fill(dt);

                    List<GrupCTLBaslikIsimleri> liste = new List<GrupCTLBaslikIsimleri>();
                    foreach (DataRow item in dt.Rows)
                    {
                        GrupCTLBaslikIsimleri kol = new GrupCTLBaslikIsimleri();
                        kol.id = Convert.ToInt32(item["kullaniciId"]);
                        kol.grupYoneticisi = item["kullaniciAdi"].ToString();
                        kol.grupAdi = item["ctlAdi"].ToString();
                        kol.donemBaslangic = Convert.ToDateTime(item["donemBaslangic"]).ToString("dd/MM/yyy");
                        kol.donemBitis = Convert.ToDateTime(item["donemBitis"]).ToString("dd/MM/yyyy");
                        kol.ctlTuru = item["ctlTuru"].ToString();

                        liste.Add(kol);
                    }

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(js.Serialize(liste));
                
                    con16.Close();                
            }
            
        }

        [WebMethod]
        public void GrupCtlSil(string kullaniciAdi, int id, string grupAdi, string dBaslangic, string dBitis, string tur)
        {
            using (SqlConnection con18 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con18.Open();
                //ctl kullanıcı adına olan tablodan silinir
                SqlCommand cmdGrupCtlSil = new SqlCommand("delete from " + kullaniciAdi + " where ctlAdi='" + grupAdi + "' and kullaniciId=" + id + " and ctlTuru='" + tur + "'", con18);
                cmdGrupCtlSil.ExecuteNonQuery();

                //ctl GrupBilgileri tablosundan silinir
                SqlCommand cmdSahsiCtlKBdenSil = new SqlCommand("delete from GrupBilgileri where grupYoneticisi='" + kullaniciAdi + "' and grupAdi='" + grupAdi + "' and donemBaslangic='" + dBaslangic + "' and donemBitis='" + dBitis + "' and id="+id+" ", con18);
                cmdSahsiCtlKBdenSil.ExecuteNonQuery();

                //grup table ı DB den silinir
                SqlCommand cmdGrupTabloSil = new SqlCommand("DROP TABLE dbo.G_"+id+"_"+grupAdi+"",con18);
                cmdGrupTabloSil.ExecuteNonQuery();
                con18.Close();
            }
        }

        //string ZiyaretciIPsi;
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
