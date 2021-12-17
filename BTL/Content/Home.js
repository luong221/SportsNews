﻿
var owl = $('.slide-content');
    owl.owlCarousel({
        items:1,
    loop:true,
    navContainer:'.nav-circlepop',
    dots:false,
    margin:10,
    autoplay:true,
    autoplayTimeout:3000,
    autoplaySpeed: 1000,
    autoplayHoverPause:true
});

$(document).ready(function() {

    $(window).scroll(function () {
        if ($(window).scrollTop() > 69) {
            $('.navbars').addClass('navbar-fixed');
            $('.content').addClass('content-fixed');
        }
        if ($(window).scrollTop() < 70) {
            $('.navbars').removeClass('navbar-fixed');
            $('.content').removeClass('content-fixed');
        }
    });

    $(window).scroll(function () {
        if ($(window).scrollTop() >= ($(document).height() - $(window).height() - 100)) {
            getData();
        }
    });
    $("textarea").each(function () {
        this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
    }).on("input", function () {
        this.style.height = "auto";
        this.style.height = (this.scrollHeight) + "px";
    });
    $("#comment").keyup(function () {
        if ($(this).val()!='') {
            $("#comment-submit").attr("disabled", false);
        }
        else {
            $("#comment-submit").attr("disabled", true);
        }
    })
    
});
//    class CountPage {
//        static page = 1;
//}
//    function getData() {
//        $.ajax({
//            method: 'get',
//            url: '/home/getListData',
//            data: { page: CountPage.page }
//        }).done(function (resp) {
//            console.log(CountPage.page)
//            $('.content-left').append(resp);
//            CountPage.page++;
//            console.log(resp)
//            if (resp == null) {
//                $('.content-left').append("<div class='btn-bottom'><button class='see-more rounded'>Xem Thêm</button></div");
//            }
//        }).fail(function (e) { alert(e) })
//    }
//    $('.user').click(function(){
//    if($('.user-modal').hasClass('show')){
//        $('.user-modal').removeClass("show");
//    }
//    else{
//        $('.user-modal').addClass("show");
//    }


    $('body').click(function (e) {
    if($('.user-modal').hasClass('show')){
        if(e.target.className != "user-modal" && e.target.className != "far fa-user-circle"){
        $('.user-modal').removeClass("show");
        }
    }
});