// Smooth scroll blocking
document.addEventListener('DOMContentLoaded', function () {
    if ('onwheel' in document) {
        window.onwheel = function (event) {
            if (typeof (this.RDSmoothScroll) !== undefined) {
                try { window.removeEventListener('DOMMouseScroll', this.RDSmoothScroll.prototype.onWheel); } catch (error) { }
                event.stopPropagation();
            }
        };
    } else if ('onmousewheel' in document) {
        window.onmousewheel = function (event) {
            if (typeof (this.RDSmoothScroll) !== undefined) {
                try { window.removeEventListener('onmousewheel', this.RDSmoothScroll.prototype.onWheel); } catch (error) { }
                event.stopPropagation();
            }
        };
    }

    try { $('body').unmousewheel(); } catch (error) { }
});

function include(url) {
    document.write('<script src="' + url + '"></script>');
    return false;
}

/* cookie.JS
========================================================*/
include('/Content/js/jquery.cookie.js');


/* DEVICE.JS
========================================================*/
include('/Content/js/device.min.js');

/* Stick up menu
========================================================*/
include('/Content/js/tmstickup.js');
$(window).on('load',function () {
    if ($('html').hasClass('desktop')) {
        $('#stuck_container').TMStickUp({
        })
    }
});

/* Easing library
========================================================*/
include('/Content/js/jquery.easing.1.3.js');


/* ToTop
========================================================*/
include('/Content/js/jquery.ui.totop.js');
$(function () {
    $().UItoTop({ easingType: 'easeOutQuart' });
});



/* DEVICE.JS AND SMOOTH SCROLLIG
========================================================*/
include('/Content/js/jquery.mousewheel.min.js');
include('/Content/js/jquery.simplr.smoothscroll.min.js');
$(function () {
    if ($('html').hasClass('desktop')) {
        $.srSmoothscroll({
            step: 150,
            speed: 800
        });
    }
});


/* Copyright Year
========================================================*/
var currentYear = (new Date).getFullYear();
$(document).ready(function () {
    $("#copyright-year").text((new Date).getFullYear());
});


/* Superfish menu
========================================================*/
include('/Content/js/superfish.js');
//include('/Content/js/jquery.mobilemenu.js');


/* Orientation tablet fix
========================================================*/
$(function () {
    // IPad/IPhone
    var viewportmeta = document.querySelector && document.querySelector('meta[name="viewport"]'),
        ua = navigator.userAgent,

        gestureStart = function () { viewportmeta.content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6, initial-scale=1.0"; },

        scaleFix = function () {
            if (viewportmeta && /iPhone|iPad/.test(ua) && !/Opera Mini/.test(ua)) {
                viewportmeta.content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
                document.addEventListener("gesturestart", gestureStart, false);
            }
        };

    scaleFix();
    // Menu Android
    if (window.orientation != undefined) {
        var regM = /ipod|ipad|iphone/gi,
            result = ua.match(regM)
        if (!result) {
            $('.sf-menu li').each(function () {
                if ($(">ul", this)[0]) {
                    $(">a", this).toggle(
                        function () {
                            return false;
                        },
                        function () {
                            window.location.href = $(this).attr("href");
                        }
                    );
                }
            })
        }
    }
    //let jsonShoppingCart = readCookie("shoppingCart");
    //let shoppingCart = JSON.parse(jsonShoppingCart);
    //if (shoppingCart && shoppingCart.length > 0) {
    //    cart = shoppingCart;
    //    updateCart(cart);
    //}
});
var ua = navigator.userAgent.toLocaleLowerCase(),
    regV = /ipod|ipad|iphone/gi,
    result = ua.match(regV),
    userScale = "";
if (!result) {
    userScale = ",user-scalable=0"
}
document.write('<meta name="viewport" content="width=device-width,initial-scale=1.0' + userScale + '">')

var cart = [];

function ColorChange(colorId, carId) {
    $.ajax({
        url: "/api/CarDetails/GetImageByColorId/" + carId + "/" + colorId, success: function (result) {
            var tt = result;
            $('#' + carId).attr("src", result);

        }
    });

}

function CategoryChange(CategoryId, carId) {
    $.ajax({
        url: "api/CarDetails/GetPriceCategoryId/" + carId + "/" + CategoryId, success: function (result) {
            var tt = result;
            $('#Price_' + carId).text(result);
            var s = $('#Price_' + carId).val();
            console.log(s)
        }
    });
}
function Onload() {
    

    $.ajax({
        url: "/api/CartDetails/ReadCurancy", success: function(result)
        {
            debugger;
            Currency = result;
            var code = getCookie('LagguageUser');
           
         
            if (result == null && code != null) {
              
                Currency = {};
                Currency.Name = $('#currencydropdown #' + code).text();
                Currency.Code = code;
                $('#currencydropdown #' + code).attr("selected", true);

            }
            else if (result == null && code == null) {
               
                Currency = {};
                code = $('#currencydropdown option:selected').val();
                Currency.Name = $('#currencydropdown #' + code).text();
                Currency.Code = code;
                $('#currencydropdown #' + code).attr("selected", true);
                ChangeCurrency(1);
            }
            var yy = $('#currencydropdown #' + Currency.Code)
            console.log(yy)
             yy.attr("selected", true);
            console.log(yy)
           

        }
    });
}
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}
function ChangeCurrency(type) {
   
        var code = $('#currencydropdown option:selected').val();
        setCookie('LagguageUser', code, 1);
        $.ajax({
            url: "/api/CartDetails/ChangeCurrency?CCode=" + code, success: function (result) {
                Currency = result;
                $('#currencydropdown #' + result.Code).attr("selected", true);
                if (type != 1) {
                    location.reload();
                }
                
                console.log(result)


            }
        });
    
}
Onload();
function filterCars(CarsId) {
    window.location.href = "/Home/index/1";
    //$.ajax({
    //    url: "/Home/index/1", success: function (result) {
    //        var tt = result;
    //        //$('#Price_' + carId).text(result);
    //        //var s = $('#Price_' + carId).val();
    //        console.log(result)

    //    }
    //});        //$.ajax({
    //    url: "/Home/index/1", success: function (result) {
    //        var tt = result;
    //        //$('#Price_' + carId).text(result);
    //        //var s = $('#Price_' + carId).val();
    //        console.log(result)

    //    }
    //});
}

function addToCart(product) {
    product.price = $("#Price_" + product.CarId).text();
    product.color = { id: $("#color_" + product.CarId).val(), text: $("#color_" + product.CarId).children("option:selected").text() };
    product.category = { id: $("#category_" + product.CarId).val(), text: $("#category_" + product.CarId).children("option:selected").text() }

    cart.push(product);
    updateCart(cart);
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

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

function getJSessionId() {
    var jsId = document.cookie.match(/JSESSIONID=[^;]+/);
    if (jsId != null) {
        if (jsId instanceof Array)
            jsId = jsId[0].substring(11);
        else
            jsId = jsId.substring(11);
    }
    return jsId;
}

changeNavSelection();
function changeNavSelection(ElementId) {
    var url = window.location.pathname.split("/");
    console.log(url);
    var action = url[url.length - 1];
   
    $('#Nav' + action).addClass("current");
    if (url[url.length - 1] == "" || url[url.length - 1] == "Index")
        $('#NavHome').addClass("current");
    
}
function goToCart() {
   
    $('#cartClickId').click();

}

function CustomerType(type) {
    if (type == 2) {
        $('.CorporateDev').css("display", "block");
    }
    else {
        $('.CorporateDev').css("display", "none");
    }
}

function CheckIdentity(value) {
    if (value != 2) {
        CustomerType()
    }
}

