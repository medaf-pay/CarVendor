var EditCarCategoryApp = angular.module('EditCarCategoryApp', ['ngFileUpload']);
EditCarCategoryApp.controller('EditCarCategory', function ($scope, Upload, $timeout) {
    $scope.SetFile = function (file) {

        $scope.SelectedFile = file;

    };
    $scope.UploadFiles = function (carId, categoryId) {
        if ($scope.SelectedFile) {
            Upload.upload({
                url: `/api/EditCategory/UploadFile?id=${carId}&Category=${categoryId}`,
                data: {
                    files: $scope.SelectedFile
                }
            }).then(function (response) {
                window.location.href = `/Cars/Details?id=${carId}&Category=${categoryId}`;

                $timeout(function () {
                    $scope.Result = response.data;
                });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            }, function (evt) {
                var element = angular.element(document.querySelector('#dvProgress'));
                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
            });
        }
    };
});