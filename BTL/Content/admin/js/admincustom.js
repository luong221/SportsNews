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

    $('#img-file').change(function () {
        const file = $('#img-file')[0].files[0];
        if (file) {
            $("#preview").attr("src", URL.createObjectURL(file));
            $("#preview").css("display", "inline");
        }
    })

    $("#submit-article").click(function () {
        $("#description").val(utf8_to_b64(editors.getData()))
        $("#form-article").submit()
    })


    
})




