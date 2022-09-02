function ScrollToBottom() {
    // window.scrollTo(0, document.body.scrollHeight);
    $('html,body').animate({
        scrollTop: $("#sec-Chat").offset().top
    },
        'slow');
}

$(document).ready(function () {
    var scroll = parseInt($('#builder-list table tr td').css('width'));

    $('#prev-builder-sm , #prev-builder-bg').click(function () {
        var leftPos = $('#builder-list').scrollLeft();
        $('#builder-list').animate({ scrollLeft: leftPos - scroll }, 500);
    });
    $('#next-builder-sm , #next-builder-bg').click(function () {
        var leftPos = $('#builder-list').scrollLeft();
        $('#builder-list').animate({ scrollLeft: leftPos + scroll }, 500);
    });
    $('#prev-vendor-sm , #prev-vendor-bg').click(function () {
        var leftPos = $('#vendor-list').scrollLeft();
        $('#vendor-list').animate({ scrollLeft: leftPos - scroll }, 500);
    });
    $('#next-vendor-sm , #next-vendor-bg').click(function () {
        var leftPos = $('#vendor-list').scrollLeft();
        $('#vendor-list').animate({ scrollLeft: leftPos + scroll }, 500);
    });
});



//universal
