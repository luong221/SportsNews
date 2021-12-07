$('.custom-toggle').click(function () {
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
        const toolbarContainer = document.querySelector('#toolbar-container');

        toolbarContainer.appendChild(editor.ui.view.toolbar.element);
    })
    .catch(error => {
        console.error(error);
    });

$('#img-file').change(function () {
    const file = $('#img-file')[0].files[0];
    if (file) {
        $("#preview").attr("src", URL.createObjectURL(file));
        $("#preview").css("display", "inline");
    }
})
