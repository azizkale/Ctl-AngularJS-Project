//ana Controller
angCtlTable.controller("myCtrl", function ($rootScope, $cookies) {    

    //gizlenecek menü div lerini gizleme değişkenleri    
    if(true) {
        $rootScope.gizli = true;  //Yeni Dönem Açma Menüsü
        $rootScope.gizli2 = true; //Dönem Listeleme Menüsü
        $rootScope.gizli3 = true; //Kullanıcı Bilgileri Menüsü
        $rootScope.gizli4 = true; //CTL İşlemleri Menüsü
        $rootScope.gizli5 = true; //Yeni Grup Kurma
        $rootScope.gizli6 = true; //GrupCTL Listeleme Menüsü
        $rootScope.gizli7 = true; //CTL Silme Menüsü
        $rootScope.gizli8 = true; //Bir Gruba Katıl Menüsü
        $rootScope.CTLSecimDivi = true;
        $rootScope.sahsiCTL = true; //sahsi CTL yuklendiğinde sahsi CTL baslikları gözükür
        $rootScope.grupCTL = true; //grup CTL yuklendiğinde grup CTL baslikları gözükür
    }

    //Table da görüntülenecek inputun type ına karar veren değişkenler. default false gelir
    if(true) {
        $rootScope.kolifText1 = false;
        $rootScope.kolifText2 = false;
        $rootScope.kolifText3 = false;
        $rootScope.kolifText4 = false;
        $rootScope.kolifText5 = false;
        $rootScope.kolifText6 = false;
        $rootScope.kolifText7 = false;
        $rootScope.kolifText8 = false;
        $rootScope.kolifText9 = false;
        $rootScope.kolifText10 = false;
        $rootScope.kolifCheckBox1 = false;
        $rootScope.kolifCheckBox2 = false;
        $rootScope.kolifCheckBox3 = false;
        $rootScope.kolifCheckBox4 = false;
        $rootScope.kolifCheckBox5 = false;
        $rootScope.kolifCheckBox6 = false;
        $rootScope.kolifCheckBox7 = false;
        $rootScope.kolifCheckBox8 = false;
        $rootScope.kolifCheckBox9 = false;
        $rootScope.kolifCheckBox10 = false;
        
    }   

    //baslik değiştirme işlemleri için iki ayrı tablo gelir(onun kontrolu için)
    $rootScope.hideBaslikDegistirmeSahsi = true;
    $rootScope.hideBaslikDegistirmeGrup = true;

    
});

//kolon başlıkları ve çeteleyi listeleme (şahsi ve grupCTL aynı controller)
angCtlTable.controller("myCtrlCtlTableListeleme", function ($scope, $rootScope, $cookies, $window, serviceYanaAcilirMenu, facListeleme, facGrupCtlBaslikListeleme, facTabloKontrol, facUyeOlunanGrupCtlBaslikListeleme) {

    //cookie atamaları
    if (true) {
        $rootScope.kullaniciAdi = $cookies.get("kullaniciAdiC");
        $rootScope.kullaniciSifre = $cookies.get("kullaniciSifreC");
        $rootScope.ctlAdi = $cookies.get("ctlAdiC");   
    }    

    //yeni kullanıcı girişinde tablo kontrolü
    facTabloKontrol.TabloKontrol($rootScope.kullaniciAdi).then(function (res) {
        $scope.kullaniciVarmiYokmu = res.data;
        if ($scope.kullaniciVarmiYokmu == 0) {
            //Yeni kullanici girişi yapıldığında ctl yüklenemez ve yeni ctl oluştur menüsü gelir
            $rootScope.CTLSecimDivi = true;
            $rootScope.gizli4 = false;
            $("#pills-yeniCTL").tab("show");
            $("#pills-yeniCTL-tab").addClass("active");
            $("#pills-home-tab").removeClass("active");
        }
    });

    //var olan kullanıcı giriş yaparsa CTL SEÇ manüsü gelir
    if ($cookies.get("ctlAdiC") == "") {
        $rootScope.CTLSecimDivi = false;
    }

    //cookieler boş ise giriş sayfasına yönlendirir
    if ($rootScope.kullaniciAdi != "" && $rootScope.kullaniciSifre != "") {

        //kullanici çetele ve GrupCTL listeleme
        facListeleme.CtlListele($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (response) {
            $rootScope.ctlListe = response.data;          

            if ($rootScope.ctlListe[0].ctlTuru == 'sahsi') {
                $rootScope.hideBaslikDegistirmeSahsi = false;
                // ŞahsiCTL baslik listeleme
                facListeleme.BasliklariListele($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (response) {
                    $rootScope.ctlBasliklarListe = response.data;
                    $rootScope.sahsiCTL = false;
                     //DB den null gelen kolonları gizleme
                    if (true) {
                        if ($rootScope.ctlBasliklarListe[0].baslikKol1 == "")
                            $scope.kolgiz1 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol2 == "")
                            $scope.kolgiz2 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol3 == "")
                            $scope.kolgiz3 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol4 == "")
                            $scope.kolgiz4 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol5 == "")
                            $scope.kolgiz5 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol6 == "")
                            $scope.kolgiz6 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol7 == "")
                            $scope.kolgiz7 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol8 == "")
                            $scope.kolgiz8 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol9 == "")
                            $scope.kolgiz9 = true;
                        if ($rootScope.ctlBasliklarListe[0].baslikKol10 == "")
                            $scope.kolgiz10 = true;
                    }

                    //Db den gelen kolonların type kaydına göre input Text/CheckBox olarak belirlenir
                    if(true) {
                        if ($rootScope.ctlBasliklarListe[0].baslikKol1Type=="int") $rootScope.kolifText1 = true;
                        else $rootScope.kolifCheckBox1 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol2Type == "int") $rootScope.kolifText2 = true;
                        else $rootScope.kolifCheckBox2 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol3Type == "int") $rootScope.kolifText3 = true;
                        else $rootScope.kolifCheckBox3 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol4Type == "int") $rootScope.kolifText4 = true;
                        else $rootScope.kolifCheckBox4 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol5Type == "int") $rootScope.kolifText5 = true;
                        else $rootScope.kolifCheckBox5 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol6Type == "int") $rootScope.kolifText6 = true;
                        else $rootScope.kolifCheckBox6 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol7Type == "int") $rootScope.kolifText7 = true;
                        else $rootScope.kolifCheckBox7 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol8Type == "int") $rootScope.kolifText8 = true;
                        else $rootScope.kolifCheckBox8 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol9Type == "int") $rootScope.kolifText9 = true;
                        else $rootScope.kolifCheckBox9 = true;

                        if ($rootScope.ctlBasliklarListe[0].baslikKol10Type == "int") $rootScope.kolifText10 = true;
                        else $rootScope.kolifCheckBox10 = true;
                        
                    }
                    
                });
            }

            if ($rootScope.ctlListe[0].ctlTuru == 'grup') {
                $rootScope.hideBaslikDegistirmeGrup = false;
                // GrupCTL baslik listeleme
                facGrupCtlBaslikListeleme.GrupCTLBaliklariListele($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (res) {
                    $rootScope.gurpCtlBasiklarListesi = res.data;
                    $rootScope.grupCTL = false;
                    //DB den null gelen kolonları gizleme
                    if (true) {
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol1 == "")
                            $scope.kolgiz1 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol2 == "")
                            $scope.kolgiz2 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol3 == "")
                            $scope.kolgiz3 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol4 == "")
                            $scope.kolgiz4 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol5 == "")
                            $scope.kolgiz5 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol6 == "")
                            $scope.kolgiz6 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol7 == "")
                            $scope.kolgiz7 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol8 == "")
                            $scope.kolgiz8 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol9 == "")
                            $scope.kolgiz9 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol10 == "")
                            $scope.kolgiz10 = true;
                    }

                    //Db den gelen kolonların type kaydına göre input Text/CheckBox olarak belirlenir
                    if (true) {
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType1 == "int") $rootScope.kolifText1 = true;
                        else $rootScope.kolifCheckBox1 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType2 == "int") $rootScope.kolifText2 = true;
                        else $rootScope.kolifCheckBox2 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType3 == "int") $rootScope.kolifText3 = true;
                        else $rootScope.kolifCheckBox3 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType4 == "int") $rootScope.kolifText4 = true;
                        else $rootScope.kolifCheckBox4 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType5 == "int") $rootScope.kolifText5 = true;
                        else $rootScope.kolifCheckBox5 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType6 == "int") $rootScope.kolifText6 = true;
                        else $rootScope.kolifCheckBox6 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType7 == "int") $rootScope.kolifText7 = true;
                        else $rootScope.kolifCheckBox7 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType8 == "int") $rootScope.kolifText8 = true;
                        else $rootScope.kolifCheckBox8 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType9 == "int") $rootScope.kolifText9 = true;
                        else $rootScope.kolifCheckBox9 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType10 == "int") $rootScope.kolifText10 = true;
                        else $rootScope.kolifCheckBox10 = true;

                    }       
                });
            }

            if ($rootScope.ctlListe[0].ctlTuru == 'uyeolunangrup') {
                $rootScope.hideBaslikDegistirmeGrup = false;
                // GrupCTL baslik listeleme
                facUyeOlunanGrupCtlBaslikListeleme.UyeOlunanGrupCtlBaslikListeleme($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (res) {
                    $rootScope.gurpCtlBasiklarListesi = res.data;
                    $rootScope.grupCTL = false;
                    //DB den null gelen kolonları gizleme
                    if (true) {
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol1 == "")
                            $scope.kolgiz1 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol2 == "")
                            $scope.kolgiz2 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol3 == "")
                            $scope.kolgiz3 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol4 == "")
                            $scope.kolgiz4 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol5 == "")
                            $scope.kolgiz5 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol6 == "")
                            $scope.kolgiz6 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol7 == "")
                            $scope.kolgiz7 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol8 == "")
                            $scope.kolgiz8 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol9 == "")
                            $scope.kolgiz9 = true;
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKol10 == "")
                            $scope.kolgiz10 = true;
                    }

                    //Db den gelen kolonların type kaydına göre input Text/CheckBox olarak belirlenir
                    if (true) {
                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType1 == "int") $rootScope.kolifText1 = true;
                        else $rootScope.kolifCheckBox1 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType2 == "int") $rootScope.kolifText2 = true;
                        else $rootScope.kolifCheckBox2 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType3 == "int") $rootScope.kolifText3 = true;
                        else $rootScope.kolifCheckBox3 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType4 == "int") $rootScope.kolifText4 = true;
                        else $rootScope.kolifCheckBox4 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType5 == "int") $rootScope.kolifText5 = true;
                        else $rootScope.kolifCheckBox5 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType6 == "int") $rootScope.kolifText6 = true;
                        else $rootScope.kolifCheckBox6 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType7 == "int") $rootScope.kolifText7 = true;
                        else $rootScope.kolifCheckBox7 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType8 == "int") $rootScope.kolifText8 = true;
                        else $rootScope.kolifCheckBox8 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType9 == "int") $rootScope.kolifText9 = true;
                        else $rootScope.kolifCheckBox9 = true;

                        if ($rootScope.gurpCtlBasiklarListesi[0].gKoltType10 == "int") $rootScope.kolifText10 = true;
                        else $rootScope.kolifCheckBox10 = true;

                    }
                });
            }
        });  
    }
    else {
        $window.location.href = '/SahsiCTL.html';
    } 

    //yana açılır menüyü açar
    $scope.openNav = function () {
        serviceYanaAcilirMenu.openNav();
    }   

});

//değer girme
angCtlTable.controller("myCtrlDegerGirme", function ($scope, $rootScope, serviceDegerGirme, facListeleme) {
    $scope.CtlDegerGirme = function (x, index, k1, k2, k3, k4, k5, k6, k7, k8, k9, k10) {
        serviceDegerGirme.CtlDegerGirme(x, index, k1, k2, k3, k4, k5, k6, k7, k8, k9, k10);
    }    
});

//yana açılır menüyü kapatır
angCtlTable.controller("myCtrlYanaAcilirMenu", function ($scope, $cookies, $window, serviceYanaAcilirMenu, $rootScope, facTabloKontrol) {
    $scope.closeNav = function () {
        serviceYanaAcilirMenu.closeNav();
    }

    $scope.clickMenuYeniDonemAc = function () {
        $rootScope.gizli = false;
        $rootScope.gizli2 = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli4 = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = true;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    $scope.clickDonemSecGoster = function () {
        $rootScope.gizli2 = false;
        $rootScope.gizli = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli4 = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = true;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    $scope.clickMenuHesabim = function () {
        $rootScope.gizli3 = false;
        $rootScope.gizli2 = true;
        $rootScope.gizli = true;
        $rootScope.gizli4 = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = true;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    $scope.clickMenuCtlIslemmleri = function () {
        $rootScope.gizli4 = false;
        $rootScope.gizli2 = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = true;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    //Yeni Grup Kurma    
    $scope.clickMenuGrupKur = function () {
        $rootScope.gizli = true;
        $rootScope.gizli2 = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli4 = true;
        facTabloKontrol.TabloKontrol($rootScope.kullaniciAdi).then(function (res) {
            $scope.kullaniciVarmiYokmu = res.data;
            if ($scope.kullaniciVarmiYokmu == 0) {
                //Yeni kullanici girişi yapıldığında kullanıcı şahsi CTL oluşturmazsa grup kurma menüsü açılmaz
                $rootScope.gizli5 = true;
                alert("Grup Kurabilmeniz için en az 1 Şahsi CTL niz olmalı.");
            }
            else
                $rootScope.gizli5 = false;
        });       
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = true;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    //Gruplarımı listele
    //Yeni Grup Kurma
    $scope.clickMenuGrupListemiGetir = function () {
        $rootScope.gizli = true;
        $rootScope.gizli2 = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli4 = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = false;
        $rootScope.gizli7 = true;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    //CTL Silme
    $scope.clickMenuCTLSilme = function () {
        $rootScope.gizli = true;
        $rootScope.gizli2 = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli4 = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = false;
        $rootScope.gizli8 = true;
        serviceYanaAcilirMenu.closeNav();
    }

    //Bir Gruba Katıl
    $scope.clickMenuBirGrubaKatil = function () {
        $rootScope.gizli = true;
        $rootScope.gizli2 = true;
        $rootScope.gizli3 = true;
        $rootScope.gizli4 = true;
        $rootScope.gizli5 = true;
        $rootScope.gizli6 = true;
        $rootScope.gizli7 = true;
        facTabloKontrol.TabloKontrol($rootScope.kullaniciAdi).then(function (res) {
            $scope.kullaniciVarmiYokmu = res.data;
            if ($scope.kullaniciVarmiYokmu == 0) {
                //Yeni kullanici girişi yapıldığında kullanıcı şahsi CTL oluşturmazsa gruba katıl menüsü açılmaz
                $rootScope.gizli8 = true;
                alert("Bir gruba katılabilemek için en az 1 Şahsi CTL niz olmalı.");
            }
            else
                $rootScope.gizli8 = false;
        });
        serviceYanaAcilirMenu.closeNav();
    }

    //cookieleri siler
    $scope.clickCikis = function () {
        $cookies.remove("kullaniciAdiC");
        $cookies.remove("kullaniciSifreC");
        $cookies.remove("ctlAdiC");
        $cookies.remove("grupAdiC");
        $window.location.href = '/CtlGiris.html';
    }

   

});

//Yeni Dönem Açma(mükerrer kontrolü, boş değer kontrolü)
angCtlTable.controller("ctrlMenuYeniDonemAcma", function ($scope, $rootScope, serYeniDonemAc, $window, $cookies) {
    $scope.diziAylar = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    $scope.diziYillar = [2020, 2021, 2022];
    
    $scope.clickYeniDonemAc = function (kullaniciAdi, ay, yil) {

        $rootScope.mevcutDonemlerDizisi = []; 
        angular.forEach($rootScope.mevcutDonemListesi, function (val, key)//mevcut olan aylar mükerrer engeli için getirilir
        {
            $rootScope.mevcutDonemlerDizisi[key] = val.ay + "/" + val.yil;
        });
        if ($rootScope.mevcutDonemlerDizisi.indexOf(ay + "/" + yil) == -1) {//açılmak istenen dönem mevcut değilse

            if ($rootScope.ctlListe[0].ctlTuru == "sahsi")//dönem açma sadece şahsi cTL de olur
            {
                if (ay != null && yil != null)// değer girilmek zorunda
                {
                    serYeniDonemAc.YeniDonemAc(kullaniciAdi, ay, yil, $cookies.get("ctlAdiC"));
                    $rootScope.gizli = true;
                    $window.location.reload();
                }
                else
                    alert("Girdiğiniz değerleri kontrol edip tekrar deneyiniz.");
            }
            else
                alert("Yeni dönem açma işlemi Bireysel CTL ler için geçerlidir.");
        }
        else
            alert(ay+"/"+yil+" dönemi zaten mevcut.");
    }
    
    $scope.clickMenuGizle = function () {
        $rootScope.gizli = true;
    }
});

//Mevcut Dönemleri Listeleme ve Açma controller ı
angCtlTable.controller("ctrlMenuDonemSec", function ($scope, $rootScope, $cookies, $window, facMevcutDonemleriListele) {

    //MEVCUT DÖNEMLERİ LİSTELER
    facMevcutDonemleriListele.DonemleriLisitele($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (response) {
        $rootScope.mevcutDonemListesi = response.data;
    });

    //seçilen dönemi basar
    $scope.clickSeciliDonemiAc = function (index, kullaniciadi, tur) {
        if (tur=="sahsictl") {
            var ay = $rootScope.mevcutDonemListesi[index]["ay"];
            var yil = $rootScope.mevcutDonemListesi[index]["yil"];
            facMevcutDonemleriListele.SeciliDonemiBas(kullaniciadi, $cookies.get("ctlAdiC"), ay, yil);           
        }
        
        $window.location.reload();       
    }
    $scope.clickMenuGizle = function () {
        $rootScope.gizli2 = true;
    }
    
});

//yeni CTL oluşturma
angCtlTable.controller("ctlOlusturma", function ($scope, $rootScope, $cookies, $window, facYeniCtlOlusturma, facMevcutCTLleriListeleVeBas, facListeleme) {

    $scope.kolonlar = []; //gelen kolon adlarının id lerini tutar
    var i = 1;    
    //text özellikli kolon oluşturmak üzere isim verme input[text] i açar
    $scope.kalemEkleText = function () {
        if (i <= 10) { // en fazla 10 kolon açılabilir
            $("#tableYeniSahsiCtlOlusturma").append("<tr><td>kolon-" + i + " adı:</td><td><input type='text' id='kol" + i + "' ng-class='int' ng-model='sahsiKol" + i + "' name='nameKol" + i + "' ng-maxlength='5'/></td></tr >");
            $scope.kolonlar[i - 1] = { kolon: $("#kol" + i + ""), turu: $("#kol" + i + "").attr("ng-class") };
            i++;
        }
        else {
            alert("Maksimum 10 kolon açabilirsiniz!");
        }
    };

    //ceheckbox içerikli kolon oluşturmak isim verme input[text] i açar
    $scope.kalemEkleCheckBox = function () {
        if (i <= 10) {
            $("#tableYeniSahsiCtlOlusturma").append("<tr><td>kolon-" + i + " adı:</td><td><input type='text' id='kol" + i + "' ng-class='bit' ng-model='sahsiKol" + i + "' name='nameKol" + i + "' ng-maxlength='5'/></td></tr >");
            $scope.kolonlar[i - 1] = { kolon: $("#kol" + i + ""), turu: $("#kol" + i + "").attr("ng-class") };
            i++;
        }
        else {
            alert("Maksimum 10 kolon açabilirsiniz!");
        }
    }; 

    $scope.clickYeniCTL = function (kad, ctlad) {
        //mükerrer isimde CTL oluşumunu önleme kontrolü
            //mevcut CTL ler getirilir
        facMevcutCTLleriListeleVeBas.CTLleriListele($cookies.get("kullaniciAdiC")).then(function (response) {
            $rootScope.mevcutCTLlerListesi = response.data;

            //index e göre var olup olmadığına bakılır, yoksa CTL oluşturulur
            if ($rootScope.mevcutCTLlerListesi.indexOf(ctlad) == -1) {
                //yeni CTL oluşturma
                facYeniCtlOlusturma.YeniCtlOlustur($cookies.get("kullaniciSifreC"), kad, ctlad, $scope.kolonlar);
                //baslik listeleme
                facListeleme.BasliklariListele($scope.kullaniciAdi, $cookies.get("ctlAdiC")).then(function (response) {
                    $rootScope.ctlBasliklarListe = response.data;
                });
                //kullanici çetele listeleme
                facListeleme.CtlListele($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (response) {
                    $rootScope.ctlListe = response.data;
                });
            }
            else alert("Bu isimde bir CTL'niz mevcuttur. Lütfen başka bir ile tekrar deneyiniz."); 
        });
              
    }
});

//Mevcut CTL leri ve grupCTL leri Listele ve basma me controller
angCtlTable.controller("MevcutCTLleriListele", function ($scope, $rootScope, $cookies, $window, facMevcutDonemleriListele, facMevcutCTLleriListeleVeBas, facMevcutGRUPCTLleriListeleVeBas, facGrupCtlBas, facMevcutUyeOlunanGrupCTLleriListele, facUyeOlunanGrupCtlyiBas) {

    //sayfa yüklenirken mevcut CTL ler yüklenir.
    facMevcutCTLleriListeleVeBas.CTLleriListele($cookies.get("kullaniciAdiC")).then(function (response) {
        $scope.mevcutCTLlerListesi = response.data;
    });

    //sayfa yüklenirken mevcut GrupCTL ler yüklenir.
    facMevcutGRUPCTLleriListeleVeBas.GrupCTLleriListele($cookies.get("kullaniciAdiC")).then(function (response) {
        $scope.mevcutGrupCTLlerListesi = response.data;
    });

    //sayfa yüklenirken mevcut Uye Olunan GrupCTL ler yüklenir.
    facMevcutUyeOlunanGrupCTLleriListele.UyeOlunanGrupCTLleriListele($cookies.get("kullaniciAdiC")).then(function (response) {
        $scope.mevcutUyeOlunanGrupCTLlerListesi = response.data;
    });


    $rootScope.clickSeciliCTLyiAc = function (index, kullaniciadi, ctladi, ctlturu) {      

        //önce CTL secimi yapılır
        $scope.closeNav = function () {
            serviceYanaAcilirMenu.closeNav();
        }   //yan menü kapatılır
        $cookies.remove("ctlAdiC");        //cookies den ctlAdi değiştirilir
        $cookies.put("ctlAdiC", ctladi);   //yeni ctl adi cookies e kaydedilir

        if (ctlturu=="sahsi") {
            // sonra CTL ye ait MEVCUT DÖNEMLERİ LİSTELER
            facMevcutDonemleriListele.DonemleriLisitele(kullaniciadi, ctladi).then(function (response) {
                $rootScope.mevcutDonemListesi = response.data;
            });

            //CTL seçiminden sonra ctl ye ait dönemler menüsüne geçiş
            $rootScope.gizli4 = true;
            $rootScope.CTLSecimDivi = true;
            $rootScope.gizli2 = false;

        }
        if (ctlturu == "grup") {
            facGrupCtlBas.GrupCTLBas(kullaniciadi, ctladi);
            $window.location.reload();           
        }
        if (ctlturu == "uyeolunangrup") {
            facUyeOlunanGrupCtlyiBas.UyeOlunanGrupCtlyiBas(kullaniciadi, ctladi);
            $window.location.reload();
        }

        //ctl ismi taşıyan değişkenler sıfırlanır(mükerrer engeli için)
        $rootScope.mevcutDonemListesi = null;
    }

});

//Hesabım controller ı
angCtlTable.controller("ctrlMenuHesabim", function ($scope, $rootScope, $cookies, $window, facUpdateAdSifre) {

    $scope.clickKullaniciBilgileri = function (ad, sifre) {

        facUpdateAdSifre.UpdateKontrol(ad).then(function (response) {
            $rootScope.deg = response.data;

            //kontrol sonucu boş dönerse değişiklik gerçekleştirilir
            if ($rootScope.deg == "") {

                //değişiklik bilgilerini gönderir
                facUpdateAdSifre.UpdateAdSifre(ad, sifre, $cookies.get("kullaniciAdiC"));
                //cookie leri günceller
                var now = new Date(),
                    // this will set the expiration to 12 months
                    exp = new Date(now.getFullYear() + 1, now.getMonth(), now.getDate());
                $cookies.put("kullaniciAdiC", ad, { expires: exp });
                $cookies.put("kullaniciSifreC", sifre, { expires: exp });

                $window.location.reload();
                $scope.clickMenuGizle = function () {
                    $rootScope.gizli3 = true;
                }
            }
            else {
                alert("Başka bir isim ile tekrar deneyiniz11.");
            }
        });       
    }

    $scope.clickMenuGizle = function () {
        $rootScope.gizli3 = true;
    }

    //inputun boş bırakılmasını önler(input validation)
    $scope.required = true;

});

//CTL İşlemleri controller ı
angCtlTable.controller("ctrlMenuCtlIslemleri", function ($scope, $rootScope, $cookies, facListeleme, $window, facBaslikUpdate, facGrupCtlBaslikUpdate) {

    facListeleme.BasliklariListele($cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC")).then(function (response) {
        //başlıkları yükler
        $scope.ctlBasliklarListe = response.data;
    });

    //başlık ismi değiştirme fonk u
    var deg; //fonksiyonu edit modunda tetiklesin diye(ikinci tıkta).
    $scope.baslikDuzenle = function (x, index, tur) {

        deg = !deg;// bu olmayınca value if deki value çalışmıyor

        //düzenle/kaydet buttonu ayarı(aynı anda sadece bir kaydet butonu olmasını sağlar)
        //diğerleri düzzenle moduna geçer, tıklanan kaydet moduna geçer
        x.editing1 = x.editing2 = x.editing3 = x.editing4 = x.editing5 = x.editing6 = x.editing7 = x.editing8 = x.editing9 = x.editing10;
        switch (index) {
            case 1: x.editing1 = !x.editing1;
                break;
            case 2: x.editing2 = !x.editing2;
                break;
            case 3: x.editing3 = !x.editing3;
                break;
            case 4: x.editing4 = !x.editing4;
                break;
            case 5: x.editing5 = !x.editing5;
                break;
            case 6: x.editing6 = !x.editing6;
                break;
            case 7: x.editing7 = !x.editing7;
                break;
            case 8: x.editing8 = !x.editing8;
                break;
            case 9: x.editing9 = !x.editing9;
                break;
            case 10: x.editing10 = !x.editing10;
                break;
        }

        if (!deg) {
            if (tur == "sahsi") {
                var ctlKolonAdi = "baslikKol" + index; //ctlBaslikListe sindeki ilgili baslik adı
                var inputName = "input" + index; // input un name si(deüer girilen input)
                var mssqlKolonAdi = "kol" + index;

                x[ctlKolonAdi] = document.getElementsByName(inputName)[0].value;
                facBaslikUpdate.BaslikUpdate(mssqlKolonAdi, x[ctlKolonAdi], $cookies.get("kullaniciAdiC"), $cookies.get("ctlAdiC"));
            }

            if (tur == "grup") {
                var ctlKolonAdi2 = "gKol" + index; //ctlBaslikListe sindeki ilgili baslik adı
                var inputName2 = "input" + index; // input un name si(deüer girilen input)
                var mssqlKolonAdi2 = "gKol" + index;

                x[ctlKolonAdi2] = document.getElementsByName(inputName2)[0].value;
                facGrupCtlBaslikUpdate.GrupCtlBaslikUpdate(mssqlKolonAdi2, x[ctlKolonAdi2], $cookies.get("kullaniciAdiC"), x["grupAdi"]);
            }

            //kayıt işleminden sonra buttonlar tekrar düzenle moduna geçer
            x.editing1 = x.editing2 = x.editing3 = x.editing4 = x.editing5 = x.editing6 = x.editing7 = x.editing8 = x.editing9 = x.editing10;
        }

    }
    
   

    $scope.BaslikUpdateUygula = function () {
        $window.location.reload();
    }
    

    $scope.clickMenuGizle = function () {
        $rootScope.gizli4 = true;
    }

});

//Yeni Grup Açma controller
angCtlTable.controller("ctrlYeniGrupKur", function ($scope, $rootScope, facYeniGrupAc, $cookies, $window, facGrubBilgileriGetir, facGrubunCtlsiniOlustur) {

    $rootScope.kolonAdlari = []; //gelen kolon adlarının id lerini tutar
    var i = 1;    
    //text özellikli kolon oluşturmak üzere isim verme input[text] i açar
    $scope.kalemEkleText = function () {
        if (i <= 10) { // en fazla 10 kolon açılabilir
            $("#tableYeniGrupBasliklari").append("<tr><td>kolon-" + i + " adı:</td><td><input type='text' id='kol" + i + "' ng-class='int' ng-model='grupKol" + i + "' name='nameKol" + i + "' ng-maxlength='5'/></td></tr >");
            $rootScope.kolonAdlari[i - 1] = { kolon: $("#kol" + i + ""), turu: $("#kol" + i + "").attr("ng-class")};
            i++;
        }
        else {
            alert("Maksimum 10 kolon açabilirsiniz!");
        }            
    };

    //ceheckbox içerikli kolon oluşturmak isim verme input[text] i açar
    $scope.kalemEkleCheckBox = function () {
        if (i <= 10) {
            $("#tableYeniGrupBasliklari").append("<tr><td>kolon-" + i + " adı:</td><td><input type='text' id='kol" + i + "' ng-class='bit' ng-model='grupKol" + i + "' name='nameKol" + i + "' ng-maxlength='5'/></td></tr >");
            $rootScope.kolonAdlari[i - 1] = { kolon: $("#kol" + i + ""), turu: $("#kol" + i + "").attr("ng-class") };
            i++;
        }
        else {
            alert("Maksimum 10 kolon açabilirsiniz!");
        }            
    }; 
    

    $scope.click = function () {       
    }

    $scope.clickYeniGrup = function (kAdi, gAdi, gSifre, donembas, donembit) {     
        
        const ikiTarihArasiGunSayisi = (donembit - donembas) / (24 * 60 * 60 * 1000) + 1;

        //bitiş taarihi başlangıç tarihinden küçükse ve iki tarih arası 92 günden küçükse
        if (donembas.getTime() <= donembit.getTime() && ikiTarihArasiGunSayisi <= 92) {
              facGrubBilgileriGetir.MevcutGrupCtlleriGetir(gAdi, kAdi).then(function (res) {
                $rootScope.mevcutGrupCtlleri = res.data;

                //bu isimde CTL yoksa:yeni grubu oluşturur
                if ($rootScope.mevcutGrupCtlleri.indexOf(gAdi) == -1) { //mükerrer kontrolü
                    //yeni grubu oluşturur
                    facYeniGrupAc.GrupAc(kAdi, gAdi, gSifre, donembas, donembit, $rootScope.kolonAdlari);

                    //grubun CTL si oluşturulur
                    facGrubunCtlsiniOlustur.GrupCtlsiniOlustur(kAdi, gAdi, donembas, donembit, $rootScope.kolonAdlari);

                    //cookie ye atama yapılır
                    var now = new Date(),
                        // this will set the expiration to 12 months
                        exp = new Date(now.getFullYear() + 1, now.getMonth(), now.getDate());
                    $cookies.put("grupAdiC", gAdi, { expires: exp });
                    $cookies.put("ctlAdiC", gAdi, { expires: exp });
                }
                //bu isimde CTL varsa:
                else {
                    alert("bu isimde bir CTL niz mevcut. Lütfen başka bir isimle tekrar deneyiniz.");
                }
            });
        }
        else {
            alert("Geçersiz tarih aralığı");
        }
        $rootScope.mevcutGrupCtlleri = null;
        //$window.location.reload();
      
    }
    $scope.clickMenuGizle = function () {
        $rootScope.gizli5 = true;
    }
});

//mevcut gruplarımı listele, ve seçilen gruba git
angCtlTable.controller("ctrlMenuGrupCtlleriListele", function ($scope, $rootScope, $cookies,$window, facGrubBilgileriGetir) {
    //eksik parametre olmasın diye grup adı boş gönderilir
    //boş grup adı asmx de if-else ile kontrol edilir
    facGrubBilgileriGetir.MevcutGrupCtlleriGetir("",$cookies.get("kullaniciAdiC")).then(function (res) {
        $rootScope.grupListem = res.data;
    });

    $scope.clickSeciliGrubaGit = function (index) {
        var now = new Date(),
            // this will set the expiration to 12 months
            exp = new Date(now.getFullYear() + 1, now.getMonth(), now.getDate());
        $cookies.put("grupAdiC", $rootScope.grupListem[index][0], { expires: exp });
        $cookies.put("grupIdC", $rootScope.grupListem[index][1], { expires: exp });
        $window.location.href = 'Grup_CTL/Ctl_Grup.html';        
    }

    $scope.clickMenuGizle = function () {
        $rootScope.gizli6 = true;
    }
});

//CTL Sİlme(şahsi ve grup)
angCtlTable.controller("ctrlSahsiveGrupCTLSilme", function ($scope, $rootScope, $cookies, facCtlSil) {
    //Silme menüsüne şahsi CTL ler yüklenir
    facCtlSil.SahsiCtlSilmeListesiGetir($cookies.get("kullaniciAdiC")).then(function (res) {
        $scope.sahsiCTLSilmeListesi = res.data;
    });
    
    //şahsi CTL silme butonu
    $scope.clickSahsiCtlSil = function (kullaniciid, ctladi, yil, ay, tur) {

        if ($scope.sahsiCTLSilmeListesi.length > 1) { //2 den az CTL varsa şahsiCTL silme işlemi yapılamaz
            if (confirm(ctladi + " adlı " + ay + "/" + yil + " dönemine ait " + tur + " CTL 'nizi silmek istediğinizden emin misiniz?")) {
                facCtlSil.SahsiCtlSil($cookies.get("kullaniciAdiC"), kullaniciid, ctladi, ay, yil, tur);
                for (var i = 0; i < 2; i++) {
                    facCtlSil.SahsiCtlSilmeListesiGetir($cookies.get("kullaniciAdiC")).then(function (res) {
                        $scope.sahsiCTLSilmeListesi = res.data;
                    });
                }
            }
        }
        else
            alert("Silme işlemi için en az 2 CTL niz olmalı!");
    }

    //Silme menüsüne Grup CTL ler yüklenir
    facCtlSil.GrupCtlSilmeListesiGetir($cookies.get("kullaniciAdiC")).then(function (res) {
        $scope.grupCTLSilmeListesi = res.data;
    });

     //grup CTL silme butonu
    $scope.clickGrupCtlSil = function (id, grupadi, dbaslangic, dbitis, tur) {
        if (confirm(grupadi + " adlı " + tur + " CTL 'nizi silmek istediğinizden emin misiniz?")) {
            facCtlSil.GrupCtlSil($cookies.get("kullaniciAdiC"), id, grupadi, dbaslangic, dbitis, tur);
            for (var i = 0; i < 2; i++) {
                facCtlSil.GrupCtlSilmeListesiGetir($cookies.get("kullaniciAdiC")).then(function (res) {
                    $scope.grupCTLSilmeListesi = res.data;
                });
            }
        } 
    }
           
   
    
    
  
    //menüyü kapatma
    $scope.clickMenuGizle = function () {
        $rootScope.gizli7 = true;
    }
});

//Gruplara Üye kaydı
angCtlTable.controller("ctrBirGrubaKatil", function ($scope, $rootScope, $cookies, facBirGrubaKatil) {
    $scope.clickBirGrubaKatil = function (kgrupid, kgrupadi, kgrupsifresi) {
        var tabloAdi = "G_" + kgrupid + "_" + kgrupadi;

        //katılınmak istenen grubun varlığı kontrol edilir
        facBirGrubaKatil.GrupDogrulama(kgrupid, kgrupadi, kgrupsifresi).then(function (res) {
            $scope.deg = res.data; //girilen grup yoksa 0 döndürür, varsa katılım sağlanır
            if ($scope.deg != 0) {
                facBirGrubaKatil.GrubaKatil(tabloAdi, $cookies.get("kullaniciAdiC")).then(function (res) {
                    $scope.deg2 = res.data;
                    if ($scope.deg2.d == 0) {
                        alert("kaydınız başarı ile alındı. Yönetici onayı bekleniyor.");
                        $rootScope.gizli8 = true; 
                    }
                    else
                        alert("Bu gruba zaten kaydınız bulunmakta.");
                });
            }
            else {
                alert("Bilgileri kontrol edip tekrar deneyiniz.");
            }       
        }, function () {
            alert("Kayıt sırasında bir sorun oluştu.");
        });
    }


    //menüyü kapatma
    $scope.clickMenuGizle = function () {
        $rootScope.gizli8 = true;
    }
});

//=================GİRİŞ SAYFASI===============================

//giriş sayfası Form controller ı
//sayfaya giriş formundan girişlerde kullanıcının var olup olmadığını kontrol eder. varsa sayfasına yönlendirir; ismini ve şifresini cookielere kayeder.
appGiris.controller("ctrlGirisForm", function ($scope, $window, $cookies, facGiris) {
    $scope.deg = {};
    $scope.btnGiris_Click = function (ad, sifre) {
        facGiris.GirisFonksiyonu(ad, sifre).then(function (response) {
            $scope.deg = response.data;
            if ($scope.deg == "") {
                alert("Giriş bilgilerinizi kontrol edip tekrar deneyiniz.");
            }
            else {
                var now = new Date(),
                    // this will set the expiration to 12 months
                    exp = new Date(now.getFullYear() + 1, now.getMonth(), now.getDate());
                $cookies.put("kullaniciAdiC", ad, { expires: exp });
                $cookies.put("kullaniciSifreC", sifre, { expires: exp });
                $cookies.put("ctlAdiC", "", { expires: exp });
                $window.location.href = '/Sahsi_CTL.html';
            }
        }, function () {
            alert("giriş başarısız");
        });
    }
});


//=================YENİ kULLANICI GİRİŞİ SAYFASI===============================
appYeniKullaniciForm.controller("myCtrlYeniKullaniciForm", function ($scope, $http, $window, $cookies, facMukerrerKontrolu) {

    $scope.btnYeniKullaniciGiris_Click = function (kullaniciadi, sifre) {
        facMukerrerKontrolu.MukerrerKontrolu(kullaniciadi).then(function (response) {
            $scope.varmiyokmu = response.data;
            
            if ($scope.varmiyokmu == 0) {
                //yeni kullanıcı oluşşturma post u
                var post = {
                    method: "POST",
                    url: "CTL_CRUDOperations.asmx/YeniKullaniciOlustur",
                    data: { kullaniciAdi: $scope.txtKullaniciAdi, kullaniciSifre: $scope.txtSifre },
                    headers: { "Content-Type": "application/json" }
                };
                $http(post).then(function () {
                    $scope.deg = "kayıt başarılı";
                    $scope.kullaniciAdiCookie = $cookies.put("kullaniciAdiC", kullaniciadi);
                    $window.location.href = '/Sahsi_CTL.html';
                }, function () {
                    $scope.deg = "kayıt sırasında bir sorun oluştu";

                });
                $scope.kullaniciAdiCookie = $cookies.put("kullaniciAdiC", kullaniciadi);
                $scope.kullaniciSifreCookie = $cookies.put("kullaniciSifreC", sifre)
                $window.location.href = '/Sahsi_CTL.html';
            }
            else
                alert("Bu isim kullanılmaktadır. Lütfen başka bir kullanıcı adı ile tekrar deneyiniz.");
        });

    }
});


