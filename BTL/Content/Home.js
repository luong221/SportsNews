
$(document).ready(function() {
    $('.slide-content').owlCarousel({
        items: 1,
        loop: true,
        navContainer: '.nav-circlepop',
        dots: false,
        margin: 20,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplaySpeed: 1000,
        autoplayHoverPause: true
    });
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

    $(".textarea").each(function () {
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

    function getCommentNode(e) {
        let currNode = e.target;
        while (currNode && currNode.getAttribute("class") != 'content-comment') {
            currNode = currNode.parentNode;
        }
        return currNode.children[1].children[0];
    }
    $(".comment-modified").click(function (e) {
        let node = getCommentNode(e);
        let name = node.children[0].textContent;
        let content = node.children[1].textContent;
        node.replaceChildren();
        $(node).append("<strong>" + name + "</strong>")
        $(node).append('<textarea class="textarea1" index="' + e.target.getAttribute('index') + '" type="textarea" name="content" id="content-input" >' + content + '</textarea>')
        $(node).append('<button type="button" index="' + e.target.getAttribute('index') + '" class="btn btn-save-comment">Lưu</button>')
        $(".textarea1").each(function () {
            this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
        }).on("input", function () {
            this.style.height = "auto";
            this.style.height = (this.scrollHeight) + "px";
        });
    })
    $(document).on('click', '.btn-save-comment', function (e) {
        let content = $(".textarea1").val();
        let name = $(".textarea1").parent().children()[0].textContent;
        if (content != '') {
            $.ajax({
                url: '/comment/modified',
                method: 'post',
                data: {
                    id: e.target.getAttribute('index'),
                    content: content
                }
            }).done(function (resp) {
                $(".textarea1").each(function (i, v) {
                    if (e.target.getAttribute('index') == v.getAttribute("index")) {
                        let node = v.parentNode;
                        node.replaceChildren();
                        $(node).append("<strong>" + name + "</strong>")
                        $(node).append('<span>' + content + '</span>')
                    }
                })
            }).fail(function (resp) {

            })
        }
        else {
            alert("Không được để trống");
        }
    })
    $(".comment-delete").click(function (e) {
        $.ajax({
            url: '/comment/delete',
            method: 'post',
            data: { id: e.target.getAttribute("index") }
        }).done(function (resp) {
            let node = getCommentNode(e)
            $(node).remove();
        }).fail(function (resp) {
            alert("fail");
        })
    })

    
    $(window).scroll(function () {
        if ($(document).scrollTop() >= ($(document).height() - $(window).height())) {
            var page = $(".gif").attr("page");
            var total = $(".gif").attr("total");
            if (page <= total) {
                $.ajax({
                    url: '/home/getListData',
                    method: 'get',
                    data: {
                        page: page
                    },
                    beforeSend: function () {
                        $(".gif > img").show();
                    },
                }).done(function (resp) {
                    setTimeout(function () {
                        $(".gif > img").hide();
                        $(".other").append(resp);
                        $(".gif").attr("page", parseInt(page) + 1);
                    }, 1500);
                    
                }).fail(function (resp) {
                    console.log(resp);
                })
            }
        }
    });

});


$('body').click(function (e) {
    if($('.user-modal').hasClass('show')){
        if(e.target.className != "user-modal" && e.target.className != "far fa-user-circle"){
            $('.user-modal').removeClass("show");
        }
    }
});