$('.showauthor').click(function () {
    $(this).find('span').text(function (_, value) { return value == '-' ? '+' : '-' });
    $('.higauthor').slideToggle(200, function () {
    });
});
$('.showperfomen').click(function () {
    $(this).find('span').text(function (_, value) { return value == '-' ? '+' : '-' });
    $('.higperfomen').slideToggle(200, function () {
    });
});
$('.showarchive').click(function () {
    $(this).find('span').text(function (_, value) { return value == '-' ? '+' : '-' });
    $('.higarchive').slideToggle(200, function () {
    });
});
$('.showobserver').click(function () {
    $(this).find('span').text(function (_, value) { return value == '-' ? '+' : '-' });
    $('.higobserver').slideToggle(200, function () {
    });
});
$('.showobserverarchive').click(function () {
    $(this).find('span').text(function (_, value) { return value == '-' ? '+' : '-' });
    $('.higobserverarchive').slideToggle(200, function () {
    });
});