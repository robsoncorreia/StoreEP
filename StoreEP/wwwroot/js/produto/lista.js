$(document).ready(function () {

    $(".mdl-card").hover(function () {
        TweenMax.to(this, .25, { rotationZ: 1, backgroundColor: "rgba(0, 117, 255, 0.11)", onComplete: voltar, onCompleteParams: [this] });
    });
    function voltar(x) {
        TweenMax.to(x, .25, { rotationZ: -1, backgroundColor: "white", onComplete: inicio, onCompleteParams: [x] });

    }
    function inicio(x) {
        TweenMax.to(x, .25, { rotationZ: 0, backgroundColor: "white" });
    }
});