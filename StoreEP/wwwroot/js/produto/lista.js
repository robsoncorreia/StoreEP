$(document).ready(function () {

    $(".mdl-card").hover(function () {
        var preco = "#" + $(this).attr('id') + "-preco";
        TweenMax.to(preco, 2.5, {
            scaleX: .8,
            scaleY: .8,
            ease: Elastic.easeOut.config(.5, 0.3),
            y: 40,
        });
        TweenMax.to(this, .25, { rotationZ: 1, onComplete: voltar, onCompleteParams: [this, preco] });
    });
    function voltar(x, preco) {
        TweenMax.to(x, .25, { rotationZ: -1, onComplete: inicio, onCompleteParams: [x] });
        TweenMax.to(preco, 2.5, {
            scaleX: 1,
            scaleY: 1,
            ease:
            Elastic.easeOut.config(1, 0.3),
            y: 0,
        });
    }
    function inicio(x) {
        TweenMax.to(x, .25, { rotationZ: 0 });
    }
});