// Write your JavaScript code.
$("#btn_visualizar").click(function () {
    var src = $("#url_image_produto").val();
    if (src === "") {
        return;
    }
    var jqxhr = $.ajax(src).done(function () {
        $("#img_visualizar").attr("src", src);
    }).fail(function () {
        alert("Imagem inválida");
    }).always(function () {

    });
});
