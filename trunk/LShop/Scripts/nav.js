//导航效果
$(function () {
    var $navli = $("#nav li");
    $navli.hover(function () {
        $(this).find(".jnNav").show();
    }, function () {
        $(this).find(".jnNav").hide();
    });
})