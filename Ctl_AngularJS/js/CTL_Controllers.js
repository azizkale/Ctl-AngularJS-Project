//kolon başlıkları ve çeteleyi listeleme
angCtlTable.controller("myCtrlCtlTableListeleme", function ($scope, $http, $cookies) {
    $scope.kullaniciAdiCookie = $cookies.put("kullaniciAdiC", "aziz");
    $scope.kullaniciAdi = $cookies.get("kullaniciAdiC");

    //başlık listeleme
    $http({
        url: "CTL_CRUDOperations.asmx/CtlBasliklariListele",
        method: "GET",
        params: { kullaniciAdi: $scope.kullaniciAdi }
    }).then(function (response) {
        $scope.ctlBasliklarListe = response.data;
    });
    //kullanici çetele listeleme
    $http({
        url: "CTL_CRUDOperations.asmx/CtlListele",
        method: "GET",
        params: { kullaniciAdi: $scope.kullaniciAdi }
    }).then(function (response) {
        $scope.ctlListe = response.data;      
        $scope.kol = $scope.ctlListe[1];
        });       


   
});

//değer girme
angCtlTable.controller("myCtrlDegerGirme", function ($scope, serviceDegerGirme) {
    $scope.deg = $scope.$parent.kol;
    $scope.CtlDegerGirme = function (x, index, k1,k2,k3,k4,k5,k6,k7) {       
        serviceDegerGirme.CtlDegerGirme(x, index, k1, k2, k3, k4, k5, k6, k7);
    }
   
});