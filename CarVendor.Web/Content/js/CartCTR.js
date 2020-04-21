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

        var _0xf894 = ['+201129313331', '/api/CartDetails/paybycreditcard', 'loading', 'HIDE', '1234\x20Example\x20Town', 'show', '#Loder', 'sessionId', 'data', '200\x20Sample\x20St', 'post', 'showLightbox', 'https://imageURL', 'default', 'en_US', 'totalPrice', 'merchantName', 'configure', 'CreditCard', 'Ordered\x20goods', 'currency', 'SHOW', 'PURCHASE', 'orderId', 'then', 'ayman.abdallah@seoudi.com', 'merchantId']; (function (_0xb63f19, _0xf894fc) { var _0x361d09 = function (_0x495ced) { while (--_0x495ced) { _0xb63f19['push'](_0xb63f19['shift']()); } }; _0x361d09(++_0xf894fc); }(_0xf894, 0x93)); var _0x361d = function (_0xb63f19, _0xf894fc) { _0xb63f19 = _0xb63f19 - 0x0; var _0x361d09 = _0xf894[_0xb63f19]; return _0x361d09; }; $scope[_0x361d('0x6')] = {}; $(_0x361d('0x15'))[_0x361d('0x14')](); $scope[_0x361d('0x11')] = !![]; $http[_0x361d('0x19')](_0x361d('0x10'), $scope[_0x361d('0x6')])[_0x361d('0xc')](function (_0x161f9b) { Order['Id'] = _0x161f9b[_0x361d('0x17')]['orderId']; payObject = { 'merchant': _0x161f9b[_0x361d('0x17')][_0x361d('0xe')], 'order': { 'amount': $scope[_0x361d('0x3')], 'currency': _0x161f9b[_0x361d('0x17')][_0x361d('0x8')], 'description': _0x361d('0x7'), 'id': _0x161f9b[_0x361d('0x17')][_0x361d('0xb')] }, 'interaction': { 'operation': _0x361d('0xa'), 'merchant': { 'name': _0x161f9b[_0x361d('0x17')][_0x361d('0x4')], 'address': { 'line1': _0x361d('0x18'), 'line2': _0x361d('0x13') }, 'email': _0x361d('0xd'), 'phone': _0x361d('0xf'), 'logo': _0x361d('0x0') }, 'locale': _0x361d('0x2'), 'theme': _0x361d('0x1'), 'displayControl': { 'billingAddress': _0x361d('0x12'), 'customerEmail': 'HIDE', 'orderSummary': _0x361d('0x9'), 'shipping': _0x361d('0x12') } }, 'session': { 'id': _0x161f9b[_0x361d('0x17')][_0x361d('0x16')] } }; Checkout[_0x361d('0x5')](payObject); Checkout[_0x361d('0x1a')](); }, function (_0x358b3f) { alert(_0x358b3f); });
    };

    

}]);
