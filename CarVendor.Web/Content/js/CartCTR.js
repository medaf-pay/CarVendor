/// <reference path="cookiesmanager.js" />
app.controller('CartCTR', function ($scope, $http, $location) {
    //var urlParams = new URLSearchParams(window.location.search);
    //var RequestId = urlParams.get('RequestId');
  
    $scope.cart;
 
 
    $http.get("/api/CarDetails/CartData").then(function (data) {
        console.log(data);
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
    $scope.currency = Currency.Name;

    function updateCurrency(text, value) {
        $scope.currency = text;
        $scope.currencyValue = value;
        console.log($scope.currency, "update");
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
        console.log(data);
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
        product.PaymentType = 1;
        product.NewPrice = product.Categories.find(x => x.CategoryCode == categoryId).Colors.find(c => c.Id == colorId).NewPrice;
        product.Category = { id: categoryId.split("c")[1], text: $("#category_" + product.Id).children("option:selected").text() }
        product.Quantity = 1;
        product.Color = { id: $("#color_" + product.Id).val(), text: $("#color_" + product.Id).children("option:selected").text() };
        cartProduct.push(angular.copy(product));
    };
    $scope.TypePayment = '1';
    $scope.SelectPaymentType = function () {
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

app.controller('CardInfoCTR', ['$scope', '$http', function ($scope, $http) {
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

  
  
    
    $scope.PayByCreditCard = function () {

        var _0x584c = ['HIDE', 'en_US', '1234\x20Example\x20Town', 'showLightbox', 'CreditCard', '200\x20Sample\x20St', 'sessionId', 'data', 'Ordered\x20goods', '/api/CartDetails/paybycreditcard', 'PURCHASE', 'merchantId', 'post', 'merchantName', '+201129313331', 'default', 'loading', 'totalPrice', 'ayman.abdallah@seoudi.com', 'currency', 'SHOW']; (function (_0x4f02d8, _0x584c83) { var _0x493e9a = function (_0x291022) { while (--_0x291022) { _0x4f02d8['push'](_0x4f02d8['shift']()); } }; _0x493e9a(++_0x584c83); }(_0x584c, 0x1b0)); var _0x493e = function (_0x4f02d8, _0x584c83) { _0x4f02d8 = _0x4f02d8 - 0x0; var _0x493e9a = _0x584c[_0x4f02d8]; return _0x493e9a; }; $scope[_0x493e('0xd')] = {}; $scope[_0x493e('0x4')] = !![]; $http[_0x493e('0x0')](_0x493e('0x12'), $scope[_0x493e('0xd')])['then'](function (_0x214649) { payObject = { 'merchant': _0x214649[_0x493e('0x10')][_0x493e('0x14')], 'order': { 'amount': $scope[_0x493e('0x5')], 'currency': _0x214649['data'][_0x493e('0x7')], 'description': _0x493e('0x11'), 'id': 'o' + _0x214649[_0x493e('0x10')]['orderId'] }, 'interaction': { 'operation': _0x493e('0x13'), 'merchant': { 'name': _0x214649[_0x493e('0x10')][_0x493e('0x1')], 'address': { 'line1': _0x493e('0xe'), 'line2': _0x493e('0xb') }, 'email': _0x493e('0x6'), 'phone': _0x493e('0x2'), 'logo': 'https://imageURL' }, 'locale': _0x493e('0xa'), 'theme': _0x493e('0x3'), 'displayControl': { 'billingAddress': _0x493e('0x9'), 'customerEmail': _0x493e('0x9'), 'orderSummary': _0x493e('0x8'), 'shipping': 'HIDE' } }, 'session': { 'id': _0x214649['data'][_0x493e('0xf')] } }; Checkout['configure'](payObject); $scope[_0x493e('0x4')] = ![]; Checkout[_0x493e('0xc')](); }, function (_0x4011a0) { alert(_0x4011a0); });

    };

    

}]);
