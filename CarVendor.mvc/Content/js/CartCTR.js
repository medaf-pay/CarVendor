

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
            $scope.totalPrice = $scope.totalPrice + value.Price * value.Quantity;
            $scope.totalQuantity = $scope.totalQuantity + value.Quantity;
        });
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

    })
    $scope.Items = carsData;
   

    $scope.CompletPay = function () {
        carsData.CustomerInfo = $scope.CustomerInfo
        $http.post("/api/CartDetails/Payment?SessionId=" + RequestId, $scope.CustomerInfo)
        window.location.href = "/Home/CardInfo?RequestId=" + RequestId;
    }
});

app.controller('HomeCTR', function ($scope, $http) {


    $http.get("/api/CartDetails/getFilters").then(function (data) {
        $scope.Brands = data.data.Brands;
        $scope.Categories = data.data.Categories;
        $scope.Brandselected = '1';
        $scope.Colors = data.data.Colors;

    })
    $scope.FindCar = function () {
        window.location.href = "/Home/Index?Brand=" + $scope.Brandselected + "&Category=" + $scope.Categoryselected + "&Color=" + $scope.Colorselected;
    }

});
app.controller('CardInfoCTR', function ($scope, $http) {
    var urlParams = new URLSearchParams(window.location.search);
    var RequestId = urlParams.get('RequestId');
    $scope.SubTab = 1;
    $scope.SubTabList = ["Credit Card", "Bank Transfer"];
    $scope.SelectSubTab = function (tab) {
        $scope.SubTab = tab;
    }

    $scope.SentBankTransferData = function () {

    }
    $scope.PayByCreditCard = function () {

    }
});