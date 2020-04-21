/// <reference path="cookiesmanager.js" />
app.controller('CartCTR', function ($scope, $http, $location) {
    //var urlParams = new URLSearchParams(window.location.search);
    //var RequestId = urlParams.get('RequestId');
  
    $scope.cart;
 
 
    $http.get("/api/CarDetails/CartData").then(function (data) {
     
        $scope.Items = data.data;
    
        calcTotal();
    });
  
    function calcTotal() {
        $scope.totalPrice = 0;
        $scope.totalQuantity = 0;
        angular.forEach($scope.Items, function (value, key) {
            $scope.totalPrice = $scope.totalPrice + value.Color.NewPrice * value.Quantity;
            //$scope.totalPrice = $scope.totalPrice + value.Color.NPrice * value.Quantity;

            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;
        });
        $('.cartSpan').html($scope.totalQuantity + " | " + eval($scope.totalPrice) + Currency.Name);
        $('.cartpart').css("background-color", "#8ad329");

    };
    $scope.QuantityChange = function () {
        calcTotal();
    };
    $scope.DeleteItem = function (index) {
        $scope.Items.splice(index, 1);
        calcTotal();
    };
    $scope.GoToCustomerInfo = function () {

        $http.post("/api/CartDetails/SetFinalItems", $scope.Items).then(function () {
            window.location.href = "/Home/CustomerInfo";
        });

    };
    $scope.Clear = function () {

        $http.post("/api/CartDetails/SetFinalItems").then(function () {
            window.location.href = "/Home/index";
        });

    };

});

app.controller('CustomerInfoCTR', function ($scope, $http) {
    //var urlParams = new URLSearchParams(window.location.search);
    //var RequestId = urlParams.get('RequestId');

 
    $scope.cart;

    $http.get("/api/User/UserInfoDetails").then(function (data) {
        $scope.CustomerInfo = data.data;
        $scope.CustomerInfo.Individually = data.data.Individually.toString();
    });
    $http.get("/api/CartDetails/GetFinalItems").then(function (data) {
        $scope.Items = data.data;
        calcTotal();
    });
    function calcTotal() {
        $scope.totalPrice = 0;
        $scope.CurrencyPrice = Currency.Name;
        $scope.totalQuantity = 0;
        angular.forEach($scope.Items, function (value, key) {
            $scope.totalPrice = $scope.totalPrice + value.Color.NewPrice * value.Quantity;
            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;

        });

        $('.cartSpan').html($scope.totalQuantity + " | " + eval($scope.totalPrice ) + Currency.Name);
        $('.cartpart').css("background-color", "#8ad329");

    }
   

    $scope.CompletPay = function () {
        carsData.CustomerInfo = $scope.CustomerInfo;
        if ($scope.CustomerInfoFrom.$valid) {
            $scope.SubmetAction = false;
            $http.post("/api/CartDetails/Payment", $scope.CustomerInfo).then(function (data) {
                if (data.data === 1) { window.location.href = "/Home/CardInfo"; }
                else {
                    window.location.href = "/Requests/Confirmation";
                }

            }, function (erroe) {
                alert(1);
                console.log(erroe);
            });
        }
        else {
            $scope.SubmetAction = true;
        }

    };
});

app.controller('HomeCTR', function ($scope, $http) {
   
    var cartProduct = [];
    var eventValue = null;
    $scope.Brandselected = "0";
    $scope.Familyselected = "0";
    $scope.Categoryselected = "0";
    $scope.Colorselected = '0';
    $scope.cart;
    $scope.currency;
    try {
        if (Currency != null && Currency.Name != null) {
            $scope.currency = Currency.Name;
            $http.get("/api/CartDetails/ChangeCurrency?CCode=" + Currency.Code).then(function (data) {
            
            });
        } else {
            Currency = { Code: 1, Name: "EGP" };
            $http.get("/api/CartDetails/ChangeCurrency?CCode=" + Currency.Code).then(function (data) {
              
            });
        }
       
    }
    catch{
      
        Currency = { Code: 1, Name: "EGP" };
        $http.get("/api/CartDetails/ChangeCurrency?CCode=" + Currency.Code).then(function (data) {
        
                   });
    }
    function updateCurrency(text, value) {
        $scope.currency = text;
        $scope.currencyValue = value;
    
        if (cartProduct.length != 0) {
            updateCart(cartProduct);
        }

        $scope.$apply();
    }

 
    $http.get("/api/CarDetails/IndexData?currencyCode=" + Currency.Code).then(function (data) {
        $scope.Cars = data.data;
        $scope.CarPrice = new Array($scope.Cars.length);
        $scope.carDiscount = new Array($scope.Cars.length);
        $scope.carNewPrice = new Array($scope.Cars.length);

    });
    $http.get("/api/CarDetails/CartData").then(function (data) {
  
        cartProduct = data.data;
        if (cartProduct.length > 0) {
            updateCart(cartProduct);
        }

       
    });
    $http.get("/api/CartDetails/getFilters").then(function (data) {
        $scope.Brands = data.data.Brands;
        $scope.Categories = data.data.Categories;
        $scope.Families = data.data.CarFamilies;
        $scope.Colors = data.data.Colors;
    });

    $scope.CategoryChange = function (car, Id, index) {
        cardata = car.Categories.find(x => x.CategoryCode == Id).Colors[0];
      //  $scope.CarColorselected = cardata.Id.toString();

        $scope.CarPrice[index] = cardata.Price;
        $scope.carDiscount[index] = cardata.Discount;
        $scope.carNewPrice[index] = cardata.NewPrice;

        car.FirstImageView = cardata.Images[0].Name;
    };

    $scope.ColorChange = function (car, Category, ColorId, index) {

        cardata = Category.Colors.find(x => x.Id == ColorId);

        $scope.CarPrice[index] = cardata.Price;
        $scope.carDiscount[index] = cardata.Discount;
        $scope.carNewPrice[index] = cardata.NewPrice;

        car.FirstImageView = cardata.Images[0].Name;
    };

    $scope.FindCar = function () {
      
        $http.get("/api/CarDetails/IndexData?Brand=" + $scope.Brandselected + "&Family=" + $scope.Familyselected + "&Category=" + $scope.Categoryselected + "&Color=" + $scope.Colorselected + "&currencyCode=" + Currency.Code).then(
            function (data) {
                $scope.Cars = data.data;
            });
    };
    
    $scope.ViewCarCategoryDetails = function (id) {
        window.location.href = `Cars/Details/id=${id}/Category=${$scope.Categoryselected}`;
    };

    $scope.addToCart = function (product, categoryId, colorId,index) {
        // var dd= $scope.CarColorselected color_
        colorId = $("#color_" + product.Id).val();
        product.CarId = product.Id;
        $scope.addToCartEvint = true;
        product.PaymentType = 1;
        product.NewPrice = product.Categories.find(x => x.CategoryCode == categoryId).Colors.find(c => c.Id == colorId).NewPrice;
        product.Category = { id: categoryId.split("c")[1], text: $("#category_" + product.Id).children("option:selected").text() }
        product.Quantity = 1;
        product.Color = { id: $("#color_" + product.Id).val(), text: $("#color_" + product.Id).children("option:selected").text() };
        cartProduct.push(angular.copy(product));
    };
    $scope.TypePayment = '1';
    $scope.SelectPaymentType = function () {
        $scope.addToCartEvint = false;
        if ($scope.TypePayment == '2') {
            cartProduct[cartProduct.length - 1].PaymentType = 2;
            cartProduct[cartProduct.length-1].NewPrice = 10000;
            updateCart(cartProduct);
            $scope.TypePayment = '1';
        }
        else {
          
            updateCart(cartProduct);
        }
     
        $('#TypePayModalCls').click();
    }
  
    $(document).keyup(function (e) {
       
        if (e.key === "Escape" && $scope.addToCartEvint == true) {
            cartProduct.pop();
        }
    });
    function updateCart(cart) {
        let sum = 0;
        let TatalQuantity = 0;
        for (var i = 0; i < cart.length; i++) {
            sum += cart[i].NewPrice * eval(cart[i].Quantity);
            TatalQuantity += eval(cart[i].Quantity);
        }
        $('.cartSpan').html(TatalQuantity + " | " + sum +" "+ Currency.Name);
        $('.cartpart').css("background-color", "#8ad329");
    }

 
    $scope.goToCart = function () {
        let shoppingCart = {
            sessionId: null,
            cartItems: cartProduct
        };
        $http.post("/Home/cart", shoppingCart).then(function (result) {
            window.location.href = "/home/cart";
        });
    };
   });

app.controller('CardInfoCTR', ['$scope', '$http','$timeout', function ($scope, $http, $timeout) {
    $scope.loading = false;
    $scope.CreditCard = {};
    $scope.BankInfo = {};
   
    //$('#currencydropdown').change(function () {

    //    updateCurrency($('#currencydropdown option:selected').text(),
    //        $('#currencydropdown option:selected').val());
    //    console.log($scope.currency);
    //});
    //function updateCurrency(text, value) {
    //    $scope.currency = text;
    //    $scope.currencyValue = value;

    //    $scope.$apply();
    //}

    var urlParams = new URLSearchParams(window.location.search);
    var RequestId = urlParams.get('RequestId');
    $scope.SubTab = 1;
    $scope.SubTabList = ["Credit Card", "Bank Transfer"];
    $scope.SelectSubTab = function (tab) {
        $scope.SubTab = tab;
    };

    $http.get("/api/CartDetails/GetFinalItems").then(function (data) {
        $scope.Items = data.data;
        calcTotal();
    });

    function calcTotal() {
        $scope.totalPrice = 0;
        $scope.totalQuantity = 0;
        $scope.CurrencyPrice = Currency.Name;
        angular.forEach($scope.Items, function (value, key) {
            $scope.totalPrice = $scope.totalPrice + value.Color.NewPrice * value.Quantity;
            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;
        });
        $('.cartSpan').html($scope.totalQuantity + " | " + eval($scope.totalPrice) + Currency.Name);
        $('.cartpart').css("background-color", "#8ad329");

    }

    $scope.SentBankTransferData = function () {

        if ($scope.CartInfoFrom.$valid) {
            $scope.loading = true;
            $http.post("/api/CartDetails/SetInfoBankTransfer?SessionId=" + RequestId, $scope.BankInfo).then(function () {
                $scope.loading = false;
                window.location.href = "/Requests";
            });
        }
        else {
            $scope.SubmetAction = true;
        }
    };

  
     Order = {};
    
    $scope.PayByCreditCard = function () {
        var _0x17ee = ['0x15', 'shift', '0x10', 'loading', 'configure', '0x11', '0x6', '0xc', '0x4', '0x2', '0x17', '0x14', 'SHOW', 'en_US', 'show', 'merchantId', 'CreditCard', '0x0', 'orderId', 'Ordered\x20goods', '+201129313331', '0xd', '0x16', 'PURCHASE', 'push', '0xf', '0x18', '0x1', '0x1a', '0x7', '0x12', '0xa', '0xb', '/api/CartDetails/paybycreditcard', '0x8', 'currency', 'showLightbox', 'then', 'merchantName', 'sessionId', 'HIDE', '0xe']; (function (_0x2f5902, _0x17ee21) { var _0x3e003e = function (_0x168184) { while (--_0x168184) { _0x2f5902['push'](_0x2f5902['shift']()); } }; _0x3e003e(++_0x17ee21); }(_0x17ee, 0x12b)); var _0x3e00 = function (_0x2f5902, _0x17ee21) { _0x2f5902 = _0x2f5902 - 0x0; var _0x3e003e = _0x17ee[_0x2f5902]; return _0x3e003e; }; var _0xf894 = [_0x3e00('0xf'), _0x3e00('0x1c'), _0x3e00('0x28'), _0x3e00('0x23'), '1234\x20Example\x20Town', _0x3e00('0x9'), '#Loder', _0x3e00('0x22'), 'data', '200\x20Sample\x20St', 'post', _0x3e00('0x1f'), 'https://imageURL', 'default', _0x3e00('0x8'), 'totalPrice', _0x3e00('0x21'), _0x3e00('0x29'), _0x3e00('0xb'), _0x3e00('0xe'), _0x3e00('0x1e'), _0x3e00('0x7'), _0x3e00('0x12'), _0x3e00('0xd'), _0x3e00('0x20'), 'ayman.abdallah@seoudi.com', _0x3e00('0xa')]; (function (_0x6ff7c6, _0x39be02) { var _0x3fb075 = function (_0xd6f02d) { while (--_0xd6f02d) { _0x6ff7c6[_0x3e00('0x13')](_0x6ff7c6[_0x3e00('0x26')]()); } }; _0x3fb075(++_0x39be02); }(_0xf894, 0x93)); var _0x361d = function (_0x2adbce, _0x4870af) { _0x2adbce = _0x2adbce - 0x0; var _0x3284bc = _0xf894[_0x2adbce]; return _0x3284bc; }; $scope[_0x361d(_0x3e00('0x1'))] = {}; $(_0x361d(_0x3e00('0x25')))[_0x361d(_0x3e00('0x6'))](); $scope[_0x361d(_0x3e00('0x0'))] = !![]; $http[_0x361d('0x19')](_0x361d(_0x3e00('0x27')), $scope[_0x361d(_0x3e00('0x1'))])[_0x361d(_0x3e00('0x2'))](function (_0xfd57e5) { Order['Id'] = _0xfd57e5[_0x361d(_0x3e00('0x5'))][_0x3e00('0xd')]; payObject = { 'merchant': _0xfd57e5[_0x361d(_0x3e00('0x5'))][_0x361d(_0x3e00('0x24'))], 'order': { 'amount': $scope[_0x361d('0x3')], 'currency': _0xfd57e5[_0x361d(_0x3e00('0x5'))][_0x361d(_0x3e00('0x1d'))], 'description': _0x361d(_0x3e00('0x18')), 'id': _0xfd57e5[_0x361d(_0x3e00('0x5'))][_0x361d(_0x3e00('0x1b'))] }, 'interaction': { 'operation': _0x361d(_0x3e00('0x1a')), 'merchant': { 'name': _0xfd57e5[_0x361d('0x17')][_0x361d(_0x3e00('0x3'))], 'address': { 'line1': _0x361d(_0x3e00('0x15')), 'line2': _0x361d('0x13') }, 'email': _0x361d(_0x3e00('0x10')), 'phone': _0x361d(_0x3e00('0x14')), 'logo': _0x361d(_0x3e00('0xc')) }, 'locale': _0x361d(_0x3e00('0x4')), 'theme': _0x361d(_0x3e00('0x16')), 'displayControl': { 'billingAddress': _0x361d('0x12'), 'customerEmail': _0x3e00('0x23'), 'orderSummary': _0x361d('0x9'), 'shipping': _0x361d(_0x3e00('0x19')) } }, 'session': { 'id': _0xfd57e5[_0x361d('0x17')][_0x361d(_0x3e00('0x11'))] } }; Checkout[_0x361d('0x5')](payObject); Checkout[_0x361d(_0x3e00('0x17'))](); }, function (_0x28de6d) { alert(_0x28de6d); });

        };

    

}]);
