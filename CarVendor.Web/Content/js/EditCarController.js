var CarApp = angular.module('CarApp', ['ngFileUpload']);
CarApp.controller('EditCarCTR', function ($scope, $http, Upload, $window, $timeout) {
    $scope.EditCar = { Id: 0}
    var urlParams = new URLSearchParams(window.location.search);
    var CarId = urlParams.get('CarCode');
    console.log(CarId);
    $scope.successMessagebool = false;
    var Option = {
        Id: 0, Categoryselected: null, MoreDetails: [{ Id: 0, Price: null, Colorselected: null, file: null }]
    };

    var details = { Id: 0, Price: null, Colorselected: null, file: null };

    $scope.Options = [{ Id: 0, Categoryselected: null, MoreDetails: [{ Id: 0, Price: null, Colorselected: null, file: null }] }];

    $http.get("/api/CarDetails/GetCarByCode/" + CarId).then(function (data) {
        $scope.EditCar = data.data;

        $scope.EditCar.CarFamily = data.data.CarFamily.toString();
        $scope.EditCar.Brand = data.data.Brand.toString();
        $scope.EditCar.Id = data.data.Id;
        angular.forEach(data.data.Options, function (categoreyValue, categoryKey) {
            $scope.Options[categoryKey] = angular.copy(Option);
            $scope.Options[categoryKey].Category = categoreyValue.Category.toString();
            $scope.Options[categoryKey].Id = categoreyValue.Id;
            angular.forEach(categoreyValue.moreDetails, function (value, key) {
                $scope.Options[categoryKey].MoreDetails[key] = angular.copy(details);
                $scope.Options[categoryKey].MoreDetails[key].Color = value.Color.toString();
                $scope.Options[categoryKey].MoreDetails[key].Price = value.Price;
                $scope.Options[categoryKey].MoreDetails[key].Id = value.Id;
                $scope.Options[categoryKey].MoreDetails[key].Quantity = value.Quantity;
                $scope.Options[categoryKey].MoreDetails[key].file = value.Images[0];
            });
        });


    });

    $http.get("/api/CartDetails/getFilters").then(function (data) {
        $scope.Brands = data.data.Brands;
        $scope.Categories = data.data.Categories;

        $scope.Colors = data.data.Colors;
        $scope.Models = data.data.Models;
        $scope.CarFamilies = data.data.CarFamilies;

    });

    $scope.SetNewCategory = function () {
        $scope.Options.push(angular.copy(Option));
    };

    $scope.SetNewOptions = function (index) {
        $scope.Options[index].MoreDetails.push(angular.copy(details));
    };

    $scope.SaveCar = function () {

        if ($scope.EditCarFrom.$valid) {
            $scope.EditCar.Options = $scope.Options;
            $scope.SubmetAction = false;
            $scope.updateCar($scope.EditCar);
        }
        else {
            $scope.SubmetAction = true;
        }
    };

    $scope.SetFile = function (file, Id) {
        var y = $scope.Options;
        if (file.length > 0)
            $("#" + Id).attr('src', URL.createObjectURL(file[0]));

    };

    $scope.DeleteOption = function (categoryIndex, index) {

        $scope.Options[categoryIndex].MoreDetails.splice(index, 1)

    };

    $scope.DeleteCategory = function (index) {
        $scope.Options.splice(index, 1)

    };

    $scope.updateCar = function (Car) {

        $scope.CarImages = [];
        angular.forEach(Car.Options, function (Optionvalue, Optionkey) {
            angular.forEach(Optionvalue.MoreDetails, function (value, key) {
                $scope.CarImages.push(value.file);
            });
        });
        Upload.upload({
            url: "/api/CartDetails/UploadFiles",
            data:{
                files: $scope.CarImages
            }
        }).then(function (response) {

            $scope.Result = response.data;
            var i = 0;
            
            angular.forEach(Car.Options, function (Optionvalue, Optionkey) {
                angular.forEach(Optionvalue.MoreDetails, function (value, key) {
                    value.file = response.data[i++];
                });
            });

            $http.post("/api/CartDetails/edit/"+ Car.Id, Car).then(function () {

                $scope.successMessage = "Car updated successfully";
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
    $scope.cancelUpdate = function () {
        window.location.href = "../";
    };
});