$(document).ready(function () {
    $("img").hover(function () {
        TweenMax.to(this, .5, {
            scaleX: 1.1,
            scaleY: 1.1,
            onComplete: imagemVoltar,
            onCompleteParams: [this]
        });
    });
    function imagemVoltar(x) {
        TweenMax.to(x, .5, {
            scaleX: 1,
            scaleY: 1,
            ease: Elastic.easeOut.config(.1, .1),
        });
    }
    $(".mdl-card").hover(function () {
        var preco = "#" + $(this).attr('id') + "-preco";
        TweenMax.to(this, .25, { rotationZ: 1, onComplete: voltar, onCompleteParams: [this, preco] });
        //TweenMax.to(preco, 2.5, {
        //    scaleX: .8,
        //    scaleY: .8,
        //    y: 40,
        //});
        //var card_bottom = "#" + $(this).attr('id') + "-bottom-card";
        //TweenMax.to(card_bottom, .25, { rotation: 30 });
    });
    function voltar(x, preco) {
        TweenMax.to(x, .25, { rotationZ: -1, onComplete: inicio, onCompleteParams: [x] });
        //TweenMax.to(preco, 2.5, {
        //    scaleX: 1,
        //    scaleY: 1,
        //    ease:
        //    Elastic.easeOut.config(1, 0.3),
        //    y: 0
        //});
    }
    function inicio(x) {
        TweenMax.to(x, .25, { rotationZ: 0 });
    }
});