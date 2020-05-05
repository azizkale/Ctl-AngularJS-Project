//Tablo varlık kontrolü
angCtlTable.factory("facTabloKontrol", ["$http", function ($http) {
    var fac = {};
    fac.TabloKontrol = function (kullaniciadi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/TabloKontrolu",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

// çtl (grup ve şahsi bir arada) ve başlık listeleme factory si
angCtlTable.factory("facListeleme", ["$http", function ($http) {
    var fac = {};

    //başlık listeleme
    fac.BasliklariListele = function (kullaniciadi, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/CtlBasliklariListele",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi,
                ctlAdi: ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }

    //ctl listeleme
    fac.CtlListele = function (kullaniciadi, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/CtlListele",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi,
                ctlAdi: ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }


    return fac;
}]);

//Grup CTL başlıkları listeleme
angCtlTable.factory("facGrupCtlBaslikListeleme", ["$http", function ($http) {
    var fac = {};
    // GrupCTL başlık listeleme
    fac.GrupCTLBaliklariListele = function (kullaniciadi, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupCtlBasliklariListele",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi,
                grupAdi: ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//Üye Olunan Grup CTL başlıkları listeleme
angCtlTable.factory("facUyeOlunanGrupCtlBaslikListeleme", ["$http", function ($http) {
    var fac = {};
    // GrupCTL başlık listeleme
    fac.UyeOlunanGrupCtlBaslikListeleme = function (kullaniciadi, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/UyeOlunanGrupCtlBasliklariListele",
            method: "GET",
            params: {
                uyeAdi: kullaniciadi,
                grupAdi: ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//değer girme servisi
angCtlTable.service("serviceDegerGirme", function ($http) {
    this.CtlDegerGirme = function (x, index, k1, k2, k3, k4, k5, k6, k7, k8, k9, k10) {
        var kolonlar = [k1, k2, k3, k4, k5, k6, k7, k8, k9, k10];
        x.editing = !x.editing;
        if (!x.editing) {                     
            //değerleri girme   
                var post = {
                    method: "POST",
                    url: "CTL_CRUDOperations.asmx/TabloDegerleriniKaydet",
                    data: {
                        kullaniciAdi: x.kullaniciAdi,
                        ctlAdi: x.ctlAdi,                        
                        deg1: k1,
                        deg2: k2,
                        deg3: k3,
                        deg4: k4,
                        deg5: k5,
                        deg6: k6,
                        deg7: k7,
                        deg8: k8,
                        deg9: k9,
                        deg10: k10,
                        gun: index + 1
                    }                   
            };
                $http(post).then(function () {
                    alert("başarılı");
                }, function () {
                    alert("başarısız");
                });             
        }            
        return x.editing;
    }
});

//yana açılır menünün açılıp kapanmasını sağlar
angCtlTable.service("serviceYanaAcilirMenu", function () {

    this.openNav=function () {
            document.getElementById("YanMenu").style.width = "250px";
            document.getElementById("main").style.marginRight = "250px";
            $("#main").css({ "display": "none" });
        }
    
        this.closeNav= function () {
            document.getElementById("YanMenu").style.width = "0";
            document.getElementById("main").style.marginRight = "0";
            $("#main").css({ "display": "inherit" });
        }
   
});

//yeni dönem açma servisi
angCtlTable.service("serYeniDonemAc", function ($http) {
    this.YeniDonemAc = function (kullaniciAdi, ay, yil, ctladi) {
        var post = {
            method: "POST",
            url: "CTL_CRUDOperations.asmx/YeniDonemOlustur",
            data: {
                kullaniciAdi: kullaniciAdi,
                ay: ay,
                yil: yil,
                ctlAdi: ctladi
            }
        };

        $http(post).then(function () {
            //alert("başarılı");
        }, function () {
            //alert("başarısız");
            });
    }
    
    
});

//mevcut dönem listeleme ve basma factory si
angCtlTable.factory("facMevcutDonemleriListele", ["$http", function ($http) {
    var fac = {};

    //dönem listeleme
    fac.DonemleriLisitele = function (kullaniciadi,ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/MevcutDonemleriListele",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi,
                ctlAdi: ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }  

    //seçili dönemi bas
    fac.SeciliDonemiBas = function (kullaniciAdi,ctladi,ay,yil) {
            //değerleri girme           
            var post = {
                method: "POST",
                url: "CTL_CRUDOperations.asmx/DonemSec",
                data: {
                    kullaniciAdi: kullaniciAdi,
                    ctlAdi: ctladi,
                    ay: ay,
                    yil: yil
                }
            };

        $http(post);
            //.then(function () {
            //    alert("başarılı");
            //}, function () {
            //    alert("başarısız");
            //});
    }

    return fac;
}]);

//yeni CTL oluşturma
angCtlTable.factory("facYeniCtlOlusturma", ["$http","$window","$cookies", function ($http,$window,$cookies) {
    var fac = {};

    var kolonadlari = [];
    var kolonlarturleri = [];
    fac.YeniCtlOlustur = function (sifre, kad, ctlad, kolonlar) {
        angular.forEach(kolonlar, function (val, key) {
            kolonadlari[key] = val.kolon.val();
            kolonlarturleri[key] = val.turu;
        });
        return $http({
            url: "CTL_CRUDOperations.asmx/YeniCTLOlustur",
            method: "POST",
            data: {
                kullaniciAdi: kad,
                sifre: sifre,
                ctlAdi: ctlad,
                kolonTurleri: kolonlarturleri,
                kolonAdlari: kolonadlari
            }
        }).then(function () {
            alert("CTL oluşturma başarılı...");
            $cookies.put("ctlAdiC", ctlad);
            $window.location.reload();

            }, function () {
                alert("CTL oluşturma sırasında bir sorun oluştu!!!");
            });
    }

    return fac;
}]);

// mevcut CTL leri Listeleme ve seçileni basma
angCtlTable.factory("facMevcutCTLleriListeleVeBas", ["$http", function ($http) {
    var fac = {};
    //mevcut CTL leri listeler
    fac.CTLleriListele = function (kullanicidi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/MevcutCtlleriListele",
            method: "GET",
            params: { kullaniciAdi: kullanicidi },
            headers: { "Content-Type": "application/json" }
        });
    }

    //seçilen CTL yi basar
    fac.SeciliListeyiBas = function () {
       
    }

    return fac;
}]);

//mevcut grupctl leri lsiteleme
angCtlTable.factory("facMevcutGRUPCTLleriListeleVeBas", ["$http", function ($http) {
    var fac = {};

    //mevcut CTL leri listeler
    fac.GrupCTLleriListele = function (kullanicidi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/MevcutGrupCtlleriListele",
            method: "GET",
            params: { kullaniciAdi: kullanicidi },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//mevcut uye olunan grupctl leri lsiteleme
angCtlTable.factory("facMevcutUyeOlunanGrupCTLleriListele", ["$http", function ($http) {
    var fac = {};
    //mevcut CTL leri listeler
    fac.UyeOlunanGrupCTLleriListele = function (kullanicidi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/MevcutUyeOlunanGrupCtlleriListele",
            method: "GET",
            params: { katilanAdi: kullanicidi },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//grupCTL seçme(basma)
angCtlTable.factory("facGrupCtlBas", ["$http", function ($http) {
    var fac = {};
    fac.GrupCTLBas = function (kullanicidi, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupCtlBas",
            method: "GET",
            params: {
                yoneticiAdi: kullanicidi,
                ctlAdi:ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//Uye Olunan grupCTL seçme(basma)
angCtlTable.factory("facUyeOlunanGrupCtlyiBas", ["$http", function ($http) {
    var fac = {};
    fac.UyeOlunanGrupCtlyiBas = function (kullanicidi, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/UyeOlunanGrupCtlBas",
            method: "GET",
            params: {
                uyeAdi: kullanicidi,
                ctlAdi: ctladi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//kullanici adı ve şifre güncellememe
angCtlTable.factory("facUpdateAdSifre", ["$http", function ($http) {
    var fac = {};
    
    //değişikliğin uygunluğunu kontrol eder. isim tabloda var mı diye bakar
    fac.UpdateKontrol = function (ad) {
       return $http({
           url: "CTL_CRUDOperations.asmx/UpdateKontrolAdSifre",
            method: "GET",
            params: {
                kullaniciAdi: ad               
           },
           headers: { "Content-Type": "application/json" }
        });        
    }

    //isim ve şifre günceller           
    fac.UpdateAdSifre = function (ad, sifre, cookiead) {
        return $http({
            method: "POST",
            url: "CTL_CRUDOperations.asmx/UpdateAdSifre",
            data: {
                kullaniciAdi: ad,
                sifre: sifre,
                cookiedekiKullaniciAdi: cookiead
            }
        });

    }
    return fac;
}]);

//çtl başlıklarını güncelleme
angCtlTable.factory("facBaslikUpdate", ["$http", function ($http) {

    var fac = {};
    fac.BaslikUpdate = function (baslik,baslikAdi,ad,ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/CtlBaslikUpdate",
            method: "POST",
            data: {
                baslik: baslik,
                baslikAdi: baslikAdi,
                kullaniciAdi: ad,
                ctlAdi: ctladi
            }
        }).then(function () {
                //alert("suuceessfully");
            });
    }     
    return fac;
}]);

//GrupCTL başlıklarını güncelleme
angCtlTable.factory("facGrupCtlBaslikUpdate", ["$http", function ($http) {
    var fac = {};
    fac.GrupCtlBaslikUpdate = function (baslik, baslikAdi, ad, ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupCtlBasliklariUpdate",
            method: "POST",
            data: {
                baslik: baslik,
                baslikAdi: baslikAdi,
                kullaniciAdi: ad,
                ctlAdi: ctladi
            }
        }).then(function () {
            //alert("suuceessfully");
        });
    }
    return fac;
}]);

//ŞAhsi CTLkolon gizleme
angCtlTable.factory("facKolonGizle", ["$http", function ($http) {
    var fac = {};
    fac.KolonGizle = function (baslik, baslikAdi, ad) {
        return $http({
            url: "CTL_CRUDOperations.asmx/CtlBaslikUpdate",
            method: "POST",
            data: {
                baslik: baslik,
                baslikAdi: "###"+baslikAdi+"###",
                kullaniciAdi: ad
            }
        }).then(function () {
            alert("kolon gizlendi");
        });
    }
    return fac;
}]);

//CTL SİLME
angCtlTable.factory("facCtlSil", ["$http", function ($http) {
    var fac = {};
    fac.SahsiCtlSilmeListesiGetir = function (kullaniciadi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/SahsiCTLSilmeListesi",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi
            },
            headers: { "Content-Type": "application/json" }
        });
    }

    fac.SahsiCtlSil = function (kullaniciadi,kullaniciid,ctladi,ay,yil,tur) {
        return $http({
            url: "CTL_CRUDOperations.asmx/SahsiCtlSil",
            method: "POST",
            data: {
                kullaniciAdi: kullaniciadi,
                kullaniciId: kullaniciid,
                ctlAdi: ctladi,
                ay: ay,
                yil: yil,
                tur:tur
            }
        }).then(function () {
                alert("silme işlemi başarıyla gerçekleşti.");
            }, function () {
                alert("silme işlemi sırasında bir hata oluştu.");
            });
    }

    fac.GrupCtlSilmeListesiGetir = function (kullaniciadi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupCTLSilmeListesi",
            method: "GET",
            params: {
                kullaniciAdi: kullaniciadi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    fac.GrupCtlSil = function (kullaniciadi, id, grupadi, dbaslangic, dbitis, tur) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupCtlSil",
            method: "POST",
            data: {
                kullaniciAdi: kullaniciadi,
                id: id,
                grupAdi: grupadi,
                dBaslangic: dbaslangic,
                dBitis: dbitis,
                tur: tur
            }
        }).then(function () {
            alert("silme işlemi başarıyla gerçekleşti.");
        }, function () {
            alert("silme işlemi sırasında bir hata oluştu.");
        });
    }
    return fac;
}]);

//Grup CTL kolon gizle
angCtlTable.factory("facGrupCtlKolonGize", ["$http", function ($http) {
    var fac = {};
    fac.GrupCtllKolonGizle = function (baslik, baslikAdi, ad,ctladi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupCtlBasliklariUpdate",
            method: "POST",
            data: {
                baslik: baslik,
                baslikAdi: "###" + baslikAdi + "###",
                kullaniciAdi: ad,
                ctlAdi: ctladi
            }
        }).then(function () {
            alert("kolon gizlendi");
        });
    }
    return fac;
}]);

//grup açılmadan önce aynı isimde bir grup olup olmadığını kontrol eder
angCtlTable.factory("facGrubBilgileriGetir", ["$http", function ($http) {
    var fac = {};
    fac.MevcutGrupCtlleriGetir = function (gadi,gyoneticisi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrubBilgileriGetir",
            method: "GET",
            params: {
                grupAdi: gadi,
                grupYoneticisi: gyoneticisi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//yeni grup açar
angCtlTable.factory("facYeniGrupAc", ["$http", function ($http) {
    var fac = {};

    var kolonadlari = [];
    var kolonlarturleri = [];
    fac.GrupAc = function (kAdi, gAdi, gSifre, donembaslangic, donembitis, kolonlar) {
        angular.forEach(kolonlar, function (val, key) {
            kolonadlari[key] = val.kolon.val();
            kolonlarturleri[key] = val.turu;
        });
        return $http({
            url: "CTL_CRUDOperations.asmx/YeniGrupAc",
            method: "POST",
            data: {
                kullaniciAdi: kAdi,
                grupAdi: gAdi,
                grupSifresi: gSifre,
                donemBaslangic: donembaslangic,
                donemBitis: donembitis,
                kolonlarTurleri:kolonlarturleri,
                kolonAdlari:kolonadlari
            }
        }).then(function () {
            alert("Grup başarı ile oluşturuldu.");
            }), function () {
                alert("Grup oluşturma sırasında bir sorun oluştu.");
            };
    }
    return fac;
}]);

//grup açıldığında kişisel olarak doldurulabilen grup CTL sini oluşturur.
angCtlTable.factory("facGrubunCtlsiniOlustur", ["$http", "$cookies", "$window", function ($http, $cookies, $window) {
    var fac1 = {};
    var kolonadlari = [];
    var kolonlarturleri = [];
    fac1.GrupCtlsiniOlustur = function (kad, grupad, donembas, donembit, kolonlar) {
        angular.forEach(kolonlar, function (val, key) {
            kolonadlari[key] = val.kolon.val();
            kolonlarturleri[key] = val.turu;
        });
        return $http({
            url: "CTL_CRUDOperations.asmx/GrubunCtlsiniOlustur",
            method: "POST",
            data: {
                grupYoneticisi: kad,
                kullaniciAdi: kad,
                grupAdi: grupad,              
                donemBaslangic: donembas,
                donemBitis: donembit,
                kolonAdlari: kolonadlari
            }
        }).then(function () {
            alert("GrupCTL oluşturma başarılı...");
            $cookies.put("CtlAdiC", grupad);
            $window.location.reload();

        }, function () {
            alert("GrupCTL oluşturma zort!!!");
        });
    }
    return fac1;
}]);

//Bir Gruba Katil
angCtlTable.factory("facBirGrubaKatil", ["$http", function ($http) {
    var fac = {};
    //katılınmak istenen grubun varlığı doğrulanır.
    fac.GrupDogrulama = function (kgrupid,kgrupadi, kgrupsifresi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GrupDogrulama",
            method: "GET",
            params: {
                kGrupId: kgrupid,
                kGrupAdi: kgrupadi,
                kGrupSifresi: kgrupsifresi
            }
        });
    }

    fac.GrubaKatil = function (tabloadi, katilimciadi) {
        return $http({
            url: "CTL_CRUDOperations.asmx/BirGrubaKatil",
            method: "POST",
            data: {
                tabloAdi: tabloadi,              
                katilanAdi: katilimciadi
            }
        });
    }
    return fac;

}]);



//=================GİRİŞ SAYFASI===============================

appGiris.factory("facGiris", ["$http", function ($http) {
    var fac = {};
    fac.GirisFonksiyonu = function (ad, sifre) {
        return $http({
            url: "CTL_CRUDOperations.asmx/GirisFonksiyonu",
            method: "GET",
            params: {
                kullaniciAdi: ad,
                sifre: sifre
            },
            headers: { "Content-Type": "application/json" }
        }); 
    }
    return fac;
}]);

//=================YENİ kULLANICI GİRİŞİ SAYFASI===============================

appYeniKullaniciForm.factory("facMukerrerKontrolu", ["$http", function ($http) {
    var fac = {};
    fac.MukerrerKontrolu = function (ad) {
        return $http({
            url: "CTL_CRUDOperations.asmx/YeniGiristeMukerrerKontrolu",
            method: "GET",
            params: {
                ad:ad
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);