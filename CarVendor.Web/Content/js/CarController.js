var CarApp = angular.module('CarApp', ['ngFileUpload']);
CarApp.controller('CreateCarCTR', function ($scope, $http, Upload, $window, $timeout) {
    $scope.NewCar = {}
    $scope.successMessagebool = false;
    var Option = {
        Categoryselected: null, MoreDetails: [{ Price: null, Colorselected: null, file: null }]
    };
    var details = { Price: null, Colorselected: null, file: null };
    $scope.Options = [{ Categoryselected: null, MoreDetails:[{ Price: null, Colorselected: null, file: null }] }]

    $http.get("/api/CartDetails/getFilters").then(function (data) {
        $scope.Brands = data.data.Brands;
        $scope.Categories = data.data.Categories;

        $scope.Colors = data.data.Colors;
        $scope.Models = data.data.Models;
        $scope.CarFamilies = data.data.CarFamilies;
    })


    $scope.SetNewCategory = function () {
        $scope.Options.push(angular.copy(Option));
    }
    $scope.SetNewOptions = function (index) {
        $scope.Options[index].MoreDetails.push(angular.copy(details));
    }

    $scope.SaveCar = function () {
    
        if ($scope.NewCarFrom.$valid) {
            $scope.NewCar.Options = $scope.Options;
            $scope.SubmetAction = false;
            $scope.AddNewCar($scope.NewCar);
        }
        else {
            $scope.SubmetAction = true;
        }
    }

    $scope.SetFile = function (file,Id)
    {
        var y = $scope.Options;
        if (file.length!=0)
            $("#" + Id ).attr('src', URL.createObjectURL(file[0]));
        
    }
    $scope.DeleteOption = function (categoryIndex, index) {
      
        $scope.Options[categoryIndex].MoreDetails.splice(index, 1)
    
    }
    $scope.DeleteCategory = function (index) {
        $scope.Options.splice(index, 1)

    }

    //$scope.DeleteCar = function (car) {
        
    //}
     
    $scope.AddNewCar = function (Car) {
      
        $scope.CarImages = [];
        angular.forEach(Car.Options, function (Optionvalue, Optionkey) {
            angular.forEach(Optionvalue.MoreDetails, function (value, key) {
                $scope.CarImages.push(value.file);
            });
        });
            Upload.upload({
                url: "/api/CartDetails/UploadFiles",
                
                data:
                {
                    files: $scope.CarImages,
                  
                },
            }).then(function (response) {
                
                    $scope.Result = response.data;
                    var i = 0;
                    angular.forEach(Car.Options, function (Optionvalue, Optionkey) {
                        angular.forEach(Optionvalue.MoreDetails, function (value, key) {
                            value.file = response.data[i++];
                        });
                });
                $http.post("/api/CartDetails/AddNewCar", Car).then(function () {
                   
                    $scope.successMessage = "Add Car submitted successfully";
                    $scope.successMessagebool = true;
                    angular.element('#FocusBtn').trigger('click');
                    $("#FocusBtn").click();
                    $timeout(function () {
                        $scope.successMessagebool = false;
                        $window.location.reload();
                    }, 5000);

                });
                  
                
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            });
        
    };
});




CarApp.controller('DeleteCar', function ($scope, $http, Upload, $window, $timeout, $location) {
    debugger;
    var id = Number(location.pathname.split("/").pop());
    debugger;
    $scope.editCar = {};

    $http.get("/api/CartDetails/getFilters").then(function (data) {
        $scope.Brands = data.data.Brands;
        $scope.Categories = data.data.Categories;

        $scope.Colors = data.data.Colors;
        $scope.Models = data.data.Models;
        //$scope.CarFamilies = data.data.CarFamilies;
    })

    $http.get("/api/CartDetails/GetCar/" + id).then(function (data) {
        debugger;
        console.log("data", data)
        $scope.editCar = data.data;
                   debugger;
        //$scope.Brands = data.data.Brands;
        //$scope.Categories = data.data.Categories;

        //$scope.Colors = data.data.Colors;
        //$scope.Models = data.data.Models;
        //$scope.CarFamilies = data.data.CarFamilies;
    });

    console.log("EditCarrrr",$scope.editCar);
});