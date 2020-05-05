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

namespace Ctl_AngularJS.Grup_CTL
{
    /// <summary>
    /// Summary description for GrupWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class GrupWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public void GrupBaslikListele(string grupAdi, string grupYoneticisi)
        {
            GrupCTLBaslikIsimleri basliklar = new GrupCTLBaslikIsimleri();
            using (SqlConnection con = new SqlConnection(ConnectionStrings.connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from GrupBilgileri where grupYoneticisi='"+grupYoneticisi+"' and grupAdi='" + grupAdi + "'", con);
                SqlDataAdapter adpData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpData.Fill(dt);

                List<GrupCTLBaslikIsimleri> listeGrupBasliklar = new List<GrupCTLBaslikIsimleri>();
                foreach (DataRow item in dt.Rows)
                {
                    basliklar.id = Convert.ToInt32(item["id"]);
                    basliklar.grupAdi = item["grupAdi"].ToString();
                    basliklar.grupSifre = item["grupSifre"].ToString();
                    basliklar.grupYoneticisi = item["grupYoneticisi"].ToString();
                    basliklar.donemBaslangic = item["donemBaslangic"].ToString();
                    basliklar.donemBitis = item["donemBitis"].ToString();
                    basliklar.ip =item["ip"].ToString();
                    basliklar.grupUyeSayisi = Convert.ToInt32(item["grupUyeSayisi"]);
                    basliklar.grupAktifDonem = Convert.ToBoolean(item["grupAktifDonem"]);
                    basliklar.gKol1 = item["gKol1"].ToString();
                    basliklar.gKol2 = item["gKol2"].ToString();
                    basliklar.gKol3 = item["gKol3"].ToString();
                    basliklar.gKol4 = item["gKol4"].ToString();
                    basliklar.gKol5 = item["gKol5"].ToString();
                    basliklar.gKol6 = item["gKol6"].ToString();
                    basliklar.gKol7 = item["gKol7"].ToString();
                    basliklar.gKol8 = item["gKol8"].ToString();
                    basliklar.gKol9 = item["gKol9"].ToString();
                    basliklar.gKol10 = item["gKol10"].ToString();

                    listeGrupBasliklar.Add(basliklar);
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(listeGrupBasliklar));

                con.Close();
            }
        }

        [WebMethod]
        public void GrupCtlListele(string grupAdi, string grupYoneticisi)
        {
            List<object> listeCtl = new List<object>();
            using (SqlConnection con1 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con1.Open();
                //ilgili grup CTL nin önce id si alınır
                SqlCommand cmd1 = new SqlCommand("select * from GrupBilgileri where grupYoneticisi='" + grupYoneticisi + "' and grupAdi='" + grupAdi + "'", con1);
                SqlDataAdapter adpData1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adpData1.Fill(dt1);
                int id = (int)dt1.Rows[0][0];

                //id ye bağlı isimli grupCtl tablosu çağrılır
                SqlCommand cmd2 = new SqlCommand("select * from G_" +id + "_" + grupAdi+" where uyeAktifMi=1" ,con1);
                SqlDataAdapter adpData2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                adpData2.Fill(dt2);

                foreach (DataRow item in dt2.Rows)
                {
                    List<object> kol = new List<object>();

                    kol.Add(Convert.ToInt32(item["id"]));
                    kol.Add(item["uye"].ToString());
                    kol.Add(item["statu"].ToString());
                    kol.Add(Convert.ToBoolean(item["uyeAktifMi"]));
                    kol.Add(Convert.ToInt32(item["grupId"]));
                    kol.Add(item["donemBaslangic"].ToString());
                    kol.Add(item["donemBitis"].ToString());

                    for (int i = 7; i < item.ItemArray.Length; i++)
                    {
                        if (item[i] != DBNull.Value)
                            kol.Add(Convert.ToInt32(item[i]));
                        else
                            kol.Add(0);
                    }

                    listeCtl.Add(kol);
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(listeCtl));

                con1.Close();
            }           
        }

        [WebMethod]
        public void GrupUyeleriniListele(string grupAdi, string kullaniciAdi,string id)
        {
            using (SqlConnection con2 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con2.Open();               
                SqlCommand cmdGrupUyeleriniGetir = new SqlCommand("select * from G_"+id+"_"+grupAdi,con2);
                SqlDataAdapter adpData2 = new SqlDataAdapter(cmdGrupUyeleriniGetir);
                DataTable dt2 = new DataTable();
                adpData2.Fill(dt2);

                List<object> anaList = new List<object>();
                foreach (DataRow item in dt2.Rows)
                {
                    GrupCtlUyeler uye = new GrupCtlUyeler();
                    uye.id= Convert.ToInt32(item["id"]);
                    uye.uye = item["uye"].ToString();
                    uye.statu = item["statu"].ToString();
                    uye.uyeAktifMi=Convert.ToBoolean(item["uyeAktifMi"]);
                    uye.grupId = Convert.ToInt32(item["grupId"]);
                    uye.donemBaslangic = item["donemBaslangic"].ToString();
                    uye.donemBitis = item["donemBitis"].ToString();

                    anaList.Add(uye);
                    
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(js.Serialize(anaList));
                con2.Close();
            }
        }

        [WebMethod]
        public void GrupUyeleriRedKabul(int id, string karar,string grupAdi, int grupId, string uyeAdi, string yoneticiAdi)
        {
            using (SqlConnection con3 = new SqlConnection(ConnectionStrings.connectionString))
            {
                con3.Open();
                switch (karar)
                {
                    case "kabul":
                        //üye ilgili grupCTL table ınde aktif edilir
                        SqlCommand cmdKabul = new SqlCommand("update G_" + grupId + "_" + grupAdi + " set uyeAktifMi=1 where id=" + id, con3);
                        cmdKabul.ExecuteNonQuery();
                        //grubun üye sayısı 1 artırılır
                        SqlCommand cmdgrupUyeSayisiniBirArtir = new SqlCommand("update GrupBilgileri set grupUyeSayisi=grupUyeSayisi+1 where id=" + grupId, con3);
                        cmdgrupUyeSayisiniBirArtir.ExecuteNonQuery();

                        //uyenin oluşturulan grupCTL bilgileri GrupKatilimciBilgileri ne kaydedilir
                        SqlCommand cmdUyeninGrupCtltoGrupKatilimciBilgileri = new SqlCommand("insert into GrupKatilimciBilgileri (katilanAdi, katildigiGrupAdi, katildigiGrubunYoneticisi, katildigiGrupId, gKol1, gKolt1, gKol2, gKolt2, gKol3, gKolt3, gKol4, gKolt4, gKol5, gKolt5, gKol6, gKolt6, gKol7, gKolt7, gKol8, gKolt8, gKol9, gKolt9, gKol10, gKolt10) values('" + uyeAdi + "','G_" + grupId + "_" + grupAdi + "','" + yoneticiAdi + "'," + grupId + ",'','','','','','','','','','','','','','','','','','','','')", con3);
                        cmdUyeninGrupCtltoGrupKatilimciBilgileri.ExecuteNonQuery();
                        //kolon kayıtları
                        //GrupBilgilerinden kolon bilgileri gelir
                        SqlCommand cmdİlgiliGrubunKolonlariniGetir = new SqlCommand("select gKol1, gKolt1, gKol2, gKolt2, gKol3, gKolt3, gKol4, gKolt4, gKol5, gKolt5, gKol6, gKolt6, gKol7, gKolt7, gKol8, gKolt8, gKol9, gKolt9, gKol10, gKolt10 from GrupBilgileri where id=" + grupId + "", con3);
                        SqlDataAdapter adpData3 = new SqlDataAdapter(cmdİlgiliGrubunKolonlariniGetir);
                        DataTable dt3 = new DataTable();
                        adpData3.Fill(dt3);
                        //GrupKatilimciBilgileri ndeki ilgili yerlere kaydedilir
                        SqlCommand cmdGrupKatilimciBilgileriKolonKayitlari = new SqlCommand("update GrupKatilimciBilgileri set [gKol1]=@p1, [gKolt1]=@p2, [gKol2]=@p3, [gKolt2]=@p4, [gKol3]=@p5, [gKolt3]=@p6, [gKol4]=@p7, [gKolt4]=@p8, [gKol5]=@p9, [gKolt5]=@p10, [gKol6]=@p11, [gKolt6]=@p12, [gKol7]=@p13, [gKolt7]=@p14, [gKol8]=@p15, [gKolt8]=@p16, [gKol9]=@p17, [gKolt9]=@p18, [gKol10]=@p19, [gKolt10]=@p20 where katildigiGrupId=" + grupId + "", con3);

                        for (int i = 1; i <= dt3.Columns.Count; i++)
                        {
                            string par = "@p" + i;
                            cmdGrupKatilimciBilgileriKolonKayitlari.Parameters.AddWithValue(par, dt3.Rows[0][i - 1]);
                        }
                        cmdGrupKatilimciBilgileriKolonKayitlari.ExecuteNonQuery();

                        //uyenin gruba ilişkin ayrıntılı CTL si şsahsiCTL sine yöneticinin CTL sinden duplicate yapılır
                        SqlCommand cmdDuplicateGrupCTLfromYoneticitoUye = new SqlCommand("INSERT INTO " + uyeAdi + " (kullaniciAdi,ctlAdi,kullaniciId,yil,ay,gun,CtlKol1,CtlKol2,CtlKol3,CtlKol4,CtlKol5,CtlKol6,CtlKol7,CtlKol8,CtlKol9,CtlKol10, ctlTuru) SELECT '" + uyeAdi + "', ctlAdi, kullaniciId, yil, ay, gun, CtlKol1, CtlKol2, CtlKol3, CtlKol4, CtlKol5, CtlKol6, CtlKol7, CtlKol8, CtlKol9, CtlKol10, ctlTuru FROM " + yoneticiAdi + " WHERE kullaniciId = " + grupId + "", con3);
                        cmdDuplicateGrupCTLfromYoneticitoUye.ExecuteNonQuery();
                        //duplicate edilen rakamlar sıfırlanır,null ise null kalır(çünkü yoneticiye aitler)
                        SqlCommand cmdYeniCtlRakakmlariSifirla = new SqlCommand("update " + uyeAdi + " set [ctlAdi]=@ctladi,[ctlTuru]=@ctlturu,[CtlKol1]=@k1, [CtlKol2]=@k2, [CtlKol3]=@k3, [CtlKol4]=@k4, [CtlKol5]=@k5, [CtlKol6]=@k6, [CtlKol7]=@k7, [CtlKol8]=@k8, [CtlKol9]=@k9, [CtlKol10]=@k10 where kullaniciId=" + grupId + "", con3);
                        cmdYeniCtlRakakmlariSifirla.Parameters.AddWithValue("@ctladi", "G_"+grupId+"_"+grupAdi);
                        cmdYeniCtlRakakmlariSifirla.Parameters.AddWithValue("@ctlturu", "uyeolunangrup");

                        for (int i = 1; i <= 10; i++)
                        {
                            string parr = "@k" +i;
                            if (dt3.Rows[0]["gKol"+i]!=DBNull.Value)
                               cmdYeniCtlRakakmlariSifirla.Parameters.AddWithValue(parr, 0);
                            else
                                cmdYeniCtlRakakmlariSifirla.Parameters.AddWithValue(parr, DBNull.Value);
                        }
                        cmdYeniCtlRakakmlariSifirla.ExecuteNonQuery();
                        break;

                    case "red":
                        SqlCommand cmdRed = new SqlCommand("delete from G_" + grupId +"_"+ grupAdi + " where id="+id+"", con3);
                        cmdRed.ExecuteNonQuery();
                        break;
                }                    
                con3.Close();
            }
        }        
    }
}
