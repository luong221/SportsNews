



function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}
var editors;
$(document).ready(function () {

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })


    $('.custom-toggle').click(function () {
        console.log($('.custom-toggle > i').css("transform"))
        if ($('.custom-toggle > i').css("transform") == 'matrix(1, 0, 0, 1, 0, 0)' || $('.custom-toggle > i').css("transform") == 'none') {
            $('.custom-toggle > i').css("transform", "rotate(180deg)");
        }
        else {
            $('.custom-toggle > i').css("transform", "rotate(0deg)");
        }

    })
    DecoupledEditor
        .create(document.querySelector('#editor'))
        .then(editor => {
            editors = editor;
            const toolbarContainer = document.querySelector('#toolbar-container');
            toolbarContainer.appendChild(editor.ui.view.toolbar.element);
        })
        .catch(error => {
            console.error(error);
        });


    $('select.form-control').select2();
    $('#keyword, #keyword-edit').select2({
        minimumResultsForSearch: 10,
        ajax: {
            url: '/keywords',
            dataType: 'json',
            type: "GET",
            delay: 500,
            data: function (params) {
                return {
                    term: params.term,
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data.results, function (item) {
                        return {
                            text: item.name,
                            id: item.id
                        }
                    })
                };
            }
        }
    });


    $('#img-file').change(function () {
        const file = $('#img-file')[0].files[0];
        if (file) {
            $("#preview").attr("src", URL.createObjectURL(file));
            $("#preview").css("display", "inline");
        }
    })

    $("#submit-article").click(function () {
        $("#description").val(utf8_to_b64(editors.getData()));
        var a = $('#keyword').find(':selected');
        var keys = [];
        $.each(a, function (index, value) {
            keys.push(value.value);
        })
        $("#keywordSubmit").val(keys);
        $("#form-article").submit()
    })


    function alertMsg(classname, resp, status) {
        var data = $('#keyword-input').val('');
        $('#alert-msg').attr("style", "display:block");
        $('#alert-msg').addClass(classname);
        if (status == "fail") {
            $('#alert-msg').append("<p>" + resp.responseJSON.msg + "</p>");
        }
        else {
            $('#alert-msg').append("<p>" + resp.msg + "</p>");
        }
        setTimeout(function () {
            $("#alert-msg").fadeTo(500, 0).slideUp(500, function () {
                $(this).hide();
                $(this).empty();
                $(this).removeClass(classname);
            });
        }, 4000);
    }

    $('.select2-selection').click(function (e) {
        if ((e.target.parentNode.getAttribute("aria-owns") == "select2-keyword-results" || e.target.getAttribute("aria-owns") == "select2-keyword-results") && $(".select2-dropdown").children()[0].getAttribute('class') != 'addKeyword') {
            $(".select2-dropdown").prepend("<div class='addKeyword'><input type=text name='keyword' id='keyword-input' placeholder='Nhập để thêm Từ Khóa....'/><i title='Thêm từ khóa mới' class='submit-keyword fas fa-plus-circle'></i></div>")
        }
        $('.submit-keyword').unbind().bind('click', function (e) {
            e.preventDefault()
            var data = $('#keyword-input').val();
            var token = $('input[name="__RequestVerificationToken"]').val();
            if (data.length > 1) {
                $.ajax({
                    url: '/keywords/create',
                    data: {
                        name: data,
                        __RequestVerificationToken: token
                    },
                    method: 'post',
                }).done(function (resp) {
                    alertMsg("alert-success", resp, "done");
                }).fail(function (resp) {
                    alertMsg("alert-danger", resp, "fail");
                });
            }
        })
    })
    $("#submit-edit").click(function () {
        $("#description").val(utf8_to_b64(editors.getData()));
        var a = $('#keyword-edit').find(':selected');
        var keys = [];
        $.each(a, function (index, value) {
            keys.push(value.value);
        })
        $("#keywordSubmit").val(keys);
        $("#form-article").submit()
    })
    function deleteObject(url, content,objectId,event) {
        $('.modal-body').html(content);
        $('#ModalConfirm').modal('show');
        var token = $('input[name="__RequestVerificationToken"]').val();
        $('.confirm-delete').click(function () {
            $.ajax({
                url: url,
                data: {
                    id: objectId,
                    __RequestVerificationToken: token
                },
                method: 'post'
            }).done(function (resp) {
                $('#ModalConfirm').modal('hide');
                remove(event)
                alertMsg("alert-success", resp, "done");

            }).fail(function (resp) {
                alertMsg("alert-success", resp, "done");
            })
        })
    }
    $(".article-remove").click(function (e) {
        e.preventDefault();
        let id = e.target.getAttribute('index');
        deleteObject("/articles/delete", "Bạn có muốn xóa bài viết này ?", id, e);
    })
    $(".censor").click(function (e) {
        e.preventDefault();
        let id = e.target.getAttribute('index');
        deleteObject("/censor", "Bạn có muốn duyệt bài viết này ?", id, e);
    })
    $(".journalist-remove").click(function (e) {
        e.preventDefault();
        let id = e.target.getAttribute('index');
        deleteObject("/info/delete", "Bạn có xóa nhà báo này ?", id, e);
    })
    $(".reader-remove").click(function (e) {
        e.preventDefault();
        let id = e.target.getAttribute('index');
        deleteObject("/info/delete", "Bạn có muốn xóa bạn đọc này ?", id, e);
    })
    function remove(e) {
        var currNode = e.target;
        while (currNode && currNode.tagName != "TR") {
            currNode = currNode.parentNode;
        }
        $(currNode).remove();
    }

    $(".toggle-password").click(function () {

        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $("#password");
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
    $('#img-file').change(function () {
        const file = $('#img-file')[0].files[0];
        if (file) {
            $(".img").css("background", "url(" + URL.createObjectURL(file) + ")");
            $(".img").css("background-position", "center");
            $(".img").css("background-size", "cover");
        }
    })

    const ctx = document.getElementById('myChart');
    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: "Số bài viết trong tháng",
                data: [],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    ajax_chart(myChart);
    $('#month,#year').change(function () {
        ajax_chart(myChart)
    })
    function ajax_chart(chart) {
        var data = data || {};
        $.ajax({
            url: '/countArticleInMonth/' + $('#month').val() + '/' + $('#year').val(),
            method: 'get'
        }).done(function (resp) {
            var label = []
            var value = []
            resp.forEach(function (e) {
                label.push(e.label)
                value.push(e.value)
            })
            chart.data.labels = label;
            chart.data.datasets[0].data = value;
            chart.update();
        }).fail(function (resp) {
            console.log(resp);
        })
        
    }
    
})





