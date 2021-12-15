$('.custom-toggle').click(function () {
    if ($('.custom-toggle > i').css("transform") == 'matrix(1, 0, 0, 1, 0, 0)' || $('.custom-toggle > i').css("transform") == 'none') {
        $('.custom-toggle > i').css("transform", "rotate(180deg)");
    }
    else {
        $('.custom-toggle > i').css("transform", "rotate(0deg)");
    }
    
})

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}
var editors;
$(document).ready(function () {
  
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
            });
        }, 3000);
    }

    $('.select2-selection').click(function (e) {
        if ((e.target.getAttribute("class") == "select2-selection__rendered" || e.target.getAttribute("aria-owns") == "select2-keyword-results") && $(".select2-dropdown").children()[0].getAttribute('class') != 'addKeyword') {
            $(".select2-dropdown").prepend("<div class='addKeyword'><input type=text name='keyword' id='keyword-input' placeholder='Nhập để thêm Từ Khóa....'/><i title='Thêm từ khóa mới' class='submit-keyword fas fa-plus-circle'></i></div>")
        }
        $('.submit-keyword').click(function () {
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
    $(".article-remove").click(function (e) {
        $('.modal-body').html('Bạn có muốn xóa ?' + $(e.target).val());
        $('#ModalConfirm').modal('show');
        var token = $('input[name="__RequestVerificationToken"]').val();
        $('.confirm-delete').click(function () {
            $.ajax({
                url: 'articles/delete',
                data: {
                    id: e.target.parentNode.getAttribute('index'),
                    __RequestVerificationToken: token
                },
                method:'post'
            }).done(function (resp) {
                $('#ModalConfirm').modal('hide');
                remove(e)
                alertMsg("alert-success", resp, "done");

            }).fail(function (resp) {
                alertMsg("alert-success", resp, "done");
            })
        })
    })

    function remove(e) {
        var currNode = e.target;
        while (currNode && currNode.tagName != "TR") {
            currNode = currNode.parentNode;
        }
        $(currNode).remove();
    }
})





