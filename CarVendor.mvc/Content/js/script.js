// Smooth scroll blocking
document.addEventListener( 'DOMContentLoaded', function() {
	if ( 'onwheel' in document ) {
		window.onwheel = function( event ) {
			if( typeof( this.RDSmoothScroll ) !== undefined ) {
				try { window.removeEventListener( 'DOMMouseScroll', this.RDSmoothScroll.prototype.onWheel ); } catch( error ) {}
				event.stopPropagation();
			}
		};
	} else if ( 'onmousewheel' in document ) {
		window.onmousewheel= function( event ) {
			if( typeof( this.RDSmoothScroll ) !== undefined ) {
				try { window.removeEventListener( 'onmousewheel', this.RDSmoothScroll.prototype.onWheel ); } catch( error ) {}
				event.stopPropagation();
			}
		};
	}

	try { $('body').unmousewheel(); } catch( error ) {}
});

function include(url){
  document.write('<script src="'+url+'"></script>');
  return false ;
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
$(window).load(function() { 
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
        step:150,
        speed:800
      });
  }   
});


/* Copyright Year
========================================================*/
var currentYear = (new Date).getFullYear();
$(document).ready(function() {
  $("#copyright-year").text( (new Date).getFullYear() );
});


/* Superfish menu
========================================================*/
include('/Content/js/superfish.js');
include('/Content/js/jquery.mobilemenu.js');


/* Orientation tablet fix
========================================================*/
$(function(){
// IPad/IPhone
	var viewportmeta = document.querySelector && document.querySelector('meta[name="viewport"]'),
	ua = navigator.userAgent,

	gestureStart = function () {viewportmeta.content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6, initial-scale=1.0";},

	scaleFix = function () {
		if (viewportmeta && /iPhone|iPad/.test(ua) && !/Opera Mini/.test(ua)) {
			viewportmeta.content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
			document.addEventListener("gesturestart", gestureStart, false);
		}
	};
	
	scaleFix();
	// Menu Android
	if(window.orientation!=undefined){
  var regM = /ipod|ipad|iphone/gi,
   result = ua.match(regM)
  if(!result) {
   $('.sf-menu li').each(function(){
    if($(">ul", this)[0]){
     $(">a", this).toggle(
      function(){
       return false;
      },
      function(){
       window.location.href = $(this).attr("href");
      }
     );
    } 
   })
  }
 }
});
var ua=navigator.userAgent.toLocaleLowerCase(),
 regV = /ipod|ipad|iphone/gi,
 result = ua.match(regV),
 userScale="";
if(!result){
 userScale=",user-scalable=0"
}
document.write('<meta name="viewport" content="width=device-width,initial-scale=1.0' + userScale + '">')

function ColorChange(colorId,carId)
{
    $.ajax({
        url: "api/CarDetails/GetImageByColorId/" + carId + "/" + colorId, success: function (result) {
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