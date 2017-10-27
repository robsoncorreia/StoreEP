$(document).ready(function () {
    $("img").hover(function () {
        TweenMax.to(this, .5, {
            scaleX: 1.1,
            scaleY: 1.1,
            rotation: Math.floor((Math.random() * 4) + 1)
        });
    });
    $("img").mouseleave(function () {
        TweenMax.to(this, .5, {
            scaleX: 1,
            scaleY: 1,
            rotation: 0
        });
    });
    $(".mdl-card").hover(function () {
        var preco = "#" + $(this).attr('id') + "-preco";
        TweenMax.to(this, .25, { rotationZ: 1, onComplete: voltar, onCompleteParams: [this, preco] });
    });
    function voltar(x, preco) {
        TweenMax.to(x, .25, { rotationZ: -1, onComplete: inicio, onCompleteParams: [x] });
    }
    function inicio(x) {
        TweenMax.to(x, .25, { rotationZ: 0 });
    }
});