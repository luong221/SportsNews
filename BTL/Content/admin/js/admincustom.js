$('.custom-toggle').click(function () {
    if ($('.custom-toggle > i').css("transform") == 'matrix(1, 0, 0, 1, 0, 0)' || $('.custom-toggle > i').css("transform") == 'none') {
        $('.custom-toggle > i').css("transform", "rotate(180deg)");
    }
    else {
        $('.custom-toggle > i').css("transform", "rotate(0deg)");
    }
    
})