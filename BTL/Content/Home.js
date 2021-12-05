
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
            $('.navbar').addClass('navbar-fixed');
            $('.content').addClass('content-fixed');
        }
        if ($(window).scrollTop() < 70) {
            $('.navbar').removeClass('navbar-fixed');
            $('.content').removeClass('content-fixed');
        }
    });

    $(window).scroll(function () {
        if ($(window).scrollTop() >= ($(document).height() - $(window).height() - 100)) {
            getData();
        }
    });
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

})

    $('body').click(function (e) {
    if($('.user-modal').hasClass('show')){
        if(e.target.className != "user-modal" && e.target.className != "far fa-user-circle"){
        $('.user-modal').removeClass("show");
        }
    }
});