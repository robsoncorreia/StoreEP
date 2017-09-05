$(document).ready(function () {
    var expImg = /^[h][t][t][p].*([.][j][p][g]|[.][p][n][g])$/;
    $("#LinkImagemPD").focusout(function () {
        var x = $(this).val();
        if (expImg.test(x)) {
            var jqxhr = $.ajax(x)
                .done(function () {
                    console.log("válido");
                    $("#img_produto").prop("src", x);
                    $("#LinkImagemPD").css({ 'background-color': 'white', 'color': 'black' });
                })
                .fail(function () {
                    console.log("inválido");
                    $("#img_produto").prop("src", "http://pulauhp.com/pemesanan/produk/no-image.jpg");
                    $("#LinkImagemPD").css({ 'background-color': 'red', 'color': 'white' });
                });
        } else {
            console.log("inválido");
            $("#img_produto").prop("src", "http://pulauhp.com/pemesanan/produk/no-image.jpg");
            $("#LinkImagemPD").css({ 'background-color': 'red', 'color': 'white' });
        }
    });
});