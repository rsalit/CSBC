
$(document).ready(function () {
    $('button').addClass('buttonNormal');
    $("button").click(function() {
        $('button').removeClass('buttonNormal');
        $('button').addClass('buttonClicked');
    });
});


