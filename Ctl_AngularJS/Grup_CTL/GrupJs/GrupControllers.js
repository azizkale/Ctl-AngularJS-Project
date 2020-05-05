//Listele (Başlık ve CTL)
angGrup.controller("ctrlTableListeleme", function ($scope, $rootScope, $cookies, facTableListeleme, facYanaAcilirMenu) {
    $scope.kolonGizleDeg = [];  
   
    facTableListeleme.GrupBasliklariListele($cookies.get("grupAdiC"), $cookies.get("kullaniciAdiC")).then(function (res) {
        $rootScope.grupBasliklar = res.data;

        //boş gelen fazladan isimsiz başlıklar kapatılır
        angular.forEach($rootScope.grupBasliklar, function (val, key) {
            for (var i = 1; i <= 10; i++) {
                var kolon = "gKol" + i;
              
                if (val[kolon] == "") {
                    $scope.kolonGizleDeg[i] = false; 
                }
                else
                    $scope.kolonGizleDeg[i] = true; 
            }
        });
    });

    facTableListeleme.GrupCtlListele($cookies.get("grupAdiC"), $cookies.get("kullaniciAdiC")).then(function (res) {
        $rootScope.grupCtl = res.data;
    });
    
});

//Yana Açılır Menü
angGrup.controller("myCtrlYanaAcilirMenu", function ($scope, $rootScope, $cookies, $window, facYanaAcilirMenu) {
    //yana açılır menüyü açar  
    $rootScope.openNav = function () {
        facYanaAcilirMenu.openNav();
    }

    //yana açılır menüyü kapar
    $scope.closeNav = function () {
        facYanaAcilirMenu.closeNav();
    }

    //MenüDivleri default gizli gelir
    if (true) {
        $rootScope.gizleUyelerListesi = true;
        $rootScope.gizleXXX = true;
    }

    //MenüDivleriGizleme
    $rootScope.MenuDivleriGizleme = function (id) {
        switch (id) {
            case "idUyeListesi":
                $rootScope.gizleUyelerListesi = true;
                break;
            case "idGizleXXX":
                $rootScope.gizleXXX = true;
                break;
        }
    }

    //Menüdivleri Gösterme
    $rootScope.MenuDivleriGosterme = function (id) {
        $rootScope.MenuDivleriGizleme(); //açık menüler kapatılır
        facYanaAcilirMenu.closeNav(); //yana açılır menü kapatılır
        //tıklanan menü açılır
        switch (id) {
            case "idUyeListesi":
                $rootScope.gizleUyelerListesi = false;
                break;
            case "idGizleXXX":
                $rootScope.gizleXXX = false;
                break;
        }
    }

    //ÇIKIŞ butonu (cookieleri siler)
    $scope.clickCikis = function () {
        $cookies.remove("kullaniciAdiC");
        $cookies.remove("kullaniciSifreC");
        $cookies.remove("ctlAdiC");
        $cookies.remove("grupAdiC");
        $window.location.href = '/CtlGiris.html';
    }
});

//Uyeler Listesi
angGrup.controller("ctrlUyelerListesi", function ($scope, $rootScope, $cookies, facGrupUyeleriniListele,facUyeRedKabul) {
    facGrupUyeleriniListele.GrupUyeleriniListele($cookies.get("grupAdiC"), $cookies.get("kullaniciAdiC"), $cookies.get("grupIdC")).then(function (res) {
        $scope.grupUyeleriListesi = res.data;
    });
    //kuyruktaki üyeyi KABUL
    $scope.uyeKabul = function (id, uyeadi, yoneticiadi) {
        facUyeRedKabul.UyeRedKabul(id, 'kabul', $cookies.get("grupAdiC"), $cookies.get("grupIdC"), uyeadi, yoneticiadi);
        for (var i = 0; i < 2; i++) {
            facGrupUyeleriniListele.GrupUyeleriniListele($cookies.get("grupAdiC"), $cookies.get("kullaniciAdiC"), $cookies.get("grupIdC")).then(function (res) {
                $scope.grupUyeleriListesi = res.data;
            });
        }       
    }
    //kuyruktaki üywyi RED
    $scope.uyeRed = function (id, uyeadi, yoneticiadi) {
        facUyeRedKabul.UyeRedKabul(id, 'red', $cookies.get("grupAdiC"), $cookies.get("grupIdC"), uyeadi, yoneticiadi);
        for (var i = 0; i < 2; i++) {
            facGrupUyeleriniListele.GrupUyeleriniListele($cookies.get("grupAdiC"), $cookies.get("kullaniciAdiC"), $cookies.get("grupIdC")).then(function (res) {
                $scope.grupUyeleriListesi = res.data;
            });
        } 
    }
});

//XXX
angGrup.controller("ctrlXXX", function ($scope, $rootScope) {   
   
});