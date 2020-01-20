﻿function createCookie(cookieName, cookieValue) {
    var date = new Date();
    date.setTime(date.getTime() + (100000 * 24 * 60 * 60 * 1000));
    document.cookie = cookieName + "=" + cookieValue + "; expires=" + date.toGMTString();
}

function accessCookie(cookieName) {
    var name = cookieName + "=";
    var allCookieArray = document.cookie.split(';');
    for (var i = 0; i < allCookieArray.length; i++) {
        var temp = allCookieArray[i].trim();
        if (temp.indexOf(name) == 0)
            return temp.substring(name.length, temp.length);
    }
    return "";
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

//function checkCookie() {
//    var user = accessCookie("testCookie");
//    if (user != "")
//        alert("Welcome Back " + user + "!!!");
//    else {
//        user = prompt("Please enter your name");
//        num = prompt("How many days you want to store your name on your computer?");
//        if (user != "" && user != null) {
//            createCookie("testCookie", user, num);
//        }
//    }
//}