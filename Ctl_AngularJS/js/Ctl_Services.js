//değer girme servisi
angCtlTable.service("serviceDegerGirme", function ($http) {  
    this.CtlDegerGirme = function (x, index, k1, k2, k3, k4, k5, k6, k7) {      
        x.editing = !x.editing;
        if (!x.editing) {
            //değerleri girme           
                var post = {
                    method: "POST",
                    url: "CTL_CRUDOperations.asmx/TabloDegerleriniKaydet",
                    data: {
                        kullaniciAdi: x.kullaniciAdi,
                        gun: index+1,
                        deg1: k1,
                        deg2: k2,
                        deg3: k3,
                        deg4: k4,
                        deg5: k5,
                        deg6: k6,
                        deg7: k7
                    }                   
                };

                $http(post).then(function () {
                    //alert("başarılı");
                }, function () {
                    //alert("başarısız");
                });             
        }
            
        return x.editing;
    }
});

