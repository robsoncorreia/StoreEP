// Write your JavaScript code.
$("#btn_visualizar").click(function () {
    var src = $("#url_image_produto").val();
    if (src === "") {
        imagem_alert();
        return;
    }
    var jqxhr = $.ajax(src).done(function () {
        $("#url_image_produto").css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, .075)");
        $("#img_visualizar").attr("src", src);
    }).fail(function () {
        imagem_alert();
        src = "http://www.fuxicodemulher.com.br/wp-content/uploads/2016/02/sem-foto_800.jpg";
        $("#img_visualizar").attr("src", src);
    }).always(function () {

    });
});
function imagem_alert(){
    $("#url_image_produto").css("box-shadow", "0px 0px 2px 1px red");
}

//https://images-submarino.b2w.io/produtos/01/00/sku/9266/0/9266029_1GG.jpg
//http://www.fuxicodemulher.com.br/wp-content/uploads/2016/02/sem-foto_800.jpg