$(document).ready(function () {
    var expImg = /^[h][t][t][p].*([.][j][p][g]|[.][p][n][g])$/;
    $("#LinkImagemPD").focusout(function () {
        var x = $(this).val();
        if (expImg.test(x)) {
            var jqxhr = $.ajax(x)
                .done(function () {
                    $("#img_produto").prop("src", x);
                    $("#LinkImagemPD").css({ 'background-color': 'white', 'color': 'black' });
                    console.log("link válido");
                })
                .fail(function () {
                    $("#img_produto").prop("src", "http://security.ufpb.br/intrum/contents/categorias/cordofones/udecra/sem-imagem.jpg/@@images/3cb1ca55-87cc-45a0-8d02-52a15fabc728.jpeg");
                    $("#LinkImagemPD").css({ 'background-color': 'red', 'color': 'white' });
                    console.log("link inválido");
                });
        } else {
            $("#img_produto").prop("src", "http://security.ufpb.br/intrum/contents/categorias/cordofones/udecra/sem-imagem.jpg/@@images/3cb1ca55-87cc-45a0-8d02-52a15fabc728.jpeg");
            $("#LinkImagemPD").css({ 'background-color': 'red', 'color': 'white' });
            console.log("link inválido");
        }
    });
});