$(document).ready(function () {
    $(".mdl-card").hover(function () {
        TweenMax.to(this, .2, { scaleX: 1.1, scaleY: 1.1 });
    });
    $(".mdl-card").mouseleave(function () {
        TweenMax.to(this, .2, { scaleX: 1, scaleY: 1 });
    });
});