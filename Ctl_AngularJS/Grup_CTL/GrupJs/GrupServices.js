angGrup.factory("facTableListeleme", ["$http", function ($http) {
    var fac = {};
    //başlık listeleme
    fac.GrupBasliklariListele = function (grupadi, grupyoneticisi) {
        return $http({
            url: "GrupWebService.asmx/GrupBaslikListele",
            method: "GET",
            params: {
                grupAdi: grupadi,
                grupYoneticisi: grupyoneticisi
            },
            headers: { "Content-Type": "application/json" }
        });
    }

    //CTL listeleme
    fac.GrupCtlListele = function (grupadi, grupyoneticisi) {
        return $http({
            url: "GrupWebService.asmx/GrupCtlListele",
            method: "GET",
            params: {
                grupAdi: grupadi,
                grupYoneticisi: grupyoneticisi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//yana açılır menü
angGrup.factory("facYanaAcilirMenu", [ function () {
    var fac = {};
    fac.openNav = function () {
        document.getElementById("YanMenu").style.width = "250px";
        document.getElementById("main").style.marginRight = "250px";
        $("#main").css({ "display": "none" });
    }

    fac.closeNav = function () {
        document.getElementById("YanMenu").style.width = "0";
        document.getElementById("main").style.marginRight = "0";
        $("#main").css({ "display": "inherit" });
    }
    return fac;
}]);

//Grup uyeleri listeleme
angGrup.factory("facGrupUyeleriniListele", ["$http", function ($http) {
    var fac = {};
    fac.GrupUyeleriniListele = function (grupadi,kullaniciadi,id) {
        return $http({
            url: "GrupWebService.asmx/GrupUyeleriniListele",
            method: "GET",
            params: {
                grupAdi: grupadi,
                kullaniciAdi: kullaniciadi,
                id:id
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);

//Grup Uye RED - KABUL
angGrup.factory("facUyeRedKabul", ["$http", function ($http) {
    var fac = {};
    fac.UyeRedKabul = function (id,karar,grupadi,grupid,uyeadi,yoneticiadi) {
        return $http({
            url: "GrupWebService.asmx/GrupUyeleriRedKabul",
            method: "POST",
            data: {
                id: id,
                karar: karar,
                grupAdi: grupadi,
                grupId: grupid,
                uyeAdi: uyeadi,
                yoneticiAdi: yoneticiadi
            },
            headers: { "Content-Type": "application/json" }
        });
    }
    return fac;
}]);