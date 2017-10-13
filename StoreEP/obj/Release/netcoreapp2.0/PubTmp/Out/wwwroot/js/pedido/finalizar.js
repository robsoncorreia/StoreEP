$(document).ready(function () {
    $(".mdl-icon-toggle").click(function () {
        $("#endereco_id").val($(this).attr("id"))
    });
    $("#btn_finalizar").click(function () {
        if ($("#endereco_id").val() == "") {
            alert("Endereço nulo");
        } else {
            alert("Endereço não nulo");
        }
    });
});