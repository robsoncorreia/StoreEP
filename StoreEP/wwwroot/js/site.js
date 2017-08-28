// Write your JavaScript code.
$("#btn_visualizar").click(function(){
    var src = $("#url_image_produto").val();
    $("#img_visualizar").attr("src", src);
});