

app.controller('CartCTR', function ($scope, $http, $location) {
    var urlParams = new URLSearchParams(window.location.search);
    var RequestId = urlParams.get('RequestId');
    $http.get("/api/CarDetails/CartData?SessionId=" + RequestId).then(function (data) {
        console.log(data);
        $scope.Items = data.data;
    
        calcTotal();
    });
  
    function calcTotal() {
        $scope.totalPrice = 0;
        $scope.totalQuantity = 0;
        angular.forEach($scope.Items, function (value, key) {
            $scope.totalPrice = $scope.totalPrice + value.Color.Price * value.Quantity;
            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;
        });
        $('.cartSpan').html($scope.totalQuantity + " | " + $scope.totalPrice + " EGP");
        $('.cartpart').css("background-color", "#8ad329");

    }
    $scope.QuantityChange = function () {
        calcTotal();
    }
    $scope.DeleteItem = function (index) {
        $scope.Items.splice(index, 1)
        calcTotal();
    }
    $scope.GoToCustomerInfo = function () {
       
        $http.post("/api/CartDetails/SetFinalItems?SessionId=" + RequestId, $scope.Items)
        window.location.href = "/Home/CustomerInfo?RequestId=" + RequestId;
    }

});

app.controller('CustomerInfoCTR', function ($scope, $http) {
    var urlParams = new URLSearchParams(window.location.search);
    var RequestId = urlParams.get('RequestId');
    $http.get("/api/CartDetails/GetFinalItems?SessionId=" + RequestId).then(function (data) {
        $scope.Items = data.data; 
        calcTotal();
    })
    function calcTotal() {
        $scope.totalPrice = 0;
        $scope.totalQuantity = 0;
        angular.forEach($scope.Items, function (value, key) {
            $scope.totalPrice = $scope.totalPrice + value.Color.Price * value.Quantity;
            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;
        });
        $('.cartSpan').html($scope.totalQuantity + " | " + $scope.totalPrice + " EGP");
        $('.cartpart').css("background-color", "#8ad329");

    }
   

    $scope.CompletPay = function () {
        carsData.CustomerInfo = $scope.CustomerInfo
        if ($scope.CustomerInfoFrom.$valid) {
            $scope.SubmetAction = false;
            $http.post("/api/CartDetails/Payment?SessionId=" + RequestId, $scope.CustomerInfo).then(function () {

                window.location.href = "/Home/CardInfo?RequestId=" + RequestId;

            })
        }
        else {
            $scope.SubmetAction = true;
        }
        
    }
});

app.controller('HomeCTR', function ($scope, $http) {
  
    $http.get("/api/CarDetails/IndexData").then(function (data) {
        $scope.Cars = data.data;
        $scope.CarPrice = new Array($scope.Cars.length);
    });
    $http.get("/api/CartDetails/getFilters").then(function (data) {
        $scope.Brands = data.data.Brands;
        $scope.Categories = data.data.Categories;

        $scope.Colors = data.data.Colors;
    })
    $scope.CategoryChange = function (car, Id,index) {
        cardata = car.Categories.find(x => x.Id == Id).Colors[0];
        $scope.CarColorselected = cardata.Id.toString();
        $scope.CarPrice[index] = cardata.Price;
        car.FirstImageView = cardata.Images[0].Name
    }
    $scope.ColorChange = function (car,Category, ColorId,index) {

        cardata = Category.Colors.find(x => x.Id == ColorId);
       
        $scope.CarPrice[index] = cardata.Price;
        car.FirstImageView = cardata.Images[0].Name
    }
    $scope.FindCar = function () {
        $http.get("/api/CarDetails/IndexData?Brand=" + $scope.Brandselected + "&Category=" + $scope.Categoryselected + "&Color=" + $scope.Colorselected).then(
            function (data) {
                $scope.Cars = data.data;
            })
    }
    var cartProduct = [];
    $scope.addToCart = function (product) {
        product.CarId = product.Id;

        product.price = $("#Price_" + product.Id).text();
        product.color = { id: $("#color_" + product.Id).val(), text: $("#color_" + product.Id).children("option:selected").text() };
        product.category = { id: $("#category_" + product.Id).val(), text: $("#category_" + product.Id).children("option:selected").text() }

        cartProduct.push(product);
        updateCart(cartProduct);
    }

    function updateCart(cart) {
        let sum = 0;
        for (var i = 0; i < cart.length; i++) {
            sum += eval(cart[i].price);
        }
        $('.cartSpan').html(cart.length + " | " + sum + " EGP");
        $('.cartpart').css("background-color", "#8ad329");

        //  createCookie("shoppingCart", JSON.stringify(cart), 2);
    }
    $scope.goToCart = function () {


        let shoppingCart = {
            sessionId: null,
            cartItems: cartProduct
        }
        $http.post("/Home/cart", shoppingCart).then(function (result) {
            console.log(result);
            window.location.href = "/home/cart?RequestId=" + result.data.SessionId;
        });
    }
   

});
app.controller('CardInfoCTR', function ($scope, $http) {
    $scope.loading = false;
    $scope.CreditCard = {};
    $scope.BankInfo = {};
    var urlParams = new URLSearchParams(window.location.search);
    var RequestId = urlParams.get('RequestId');
    $scope.SubTab = 1;
    $scope.SubTabList = ["Credit Card", "Bank Transfer"];
    $scope.SelectSubTab = function (tab) {
        $scope.SubTab = tab;
    }
    $http.get("/api/CartDetails/GetFinalItems?SessionId=" + RequestId).then(function (data) {
        $scope.Items = data.data;
        calcTotal();
    })
    function calcTotal() {
        $scope.totalPrice = 0;
        $scope.totalQuantity = 0;
        angular.forEach($scope.Items, function (value, key) {
            $scope.totalPrice = $scope.totalPrice + value.Color.Price * value.Quantity;
            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;
        });
        $('.cartSpan').html($scope.totalQuantity + " | " + $scope.totalPrice + " EGP");
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

       
    }
    $scope.PayByCreditCard = function () {
        $scope.CreditCard.TotalPrice = $scope.totalPrice;
       

        if ($scope.CartInfoFrom.$valid) {
            $scope.loading = true;
            $http.post("/api/CartDetails/paybycreditcard?SessionId=" + RequestId, $scope.CreditCard).then(function () {
                $scope.loading = false;
                window.location.href = "/Requests";
            })
        }
        else {
            $scope.SubmetAction = true;
        }
    }
 
});