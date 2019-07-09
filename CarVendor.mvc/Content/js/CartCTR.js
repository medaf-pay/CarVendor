

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
    }
});