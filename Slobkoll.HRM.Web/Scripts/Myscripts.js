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


$('.higauthor').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskAuthor?id=' + id);
});
$('.higperfomen').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskPerfomer?id=' + id);
});
$('.higarchive').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $("#workspace").load('@Url.Action("Home","TaskAuthor")' + '?id=' + id);
});
$('.showobserver').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $("#workspace").load('@Url.Action("Home","TaskAuthor")' + '?id=' + id);
});
$('.higobserverarchive').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $("#workspace").load('@Url.Action("Home","TaskAuthor")' + '?id=' + id);
});

function clickauthorfile(Id) {
    $.ajax({
        type: "POST",
        url: "/Home/TaskFileDownload",
        data: "id=" + Id,
        success: function (msg) {
            $('#workspace').append("<a id='downloadtask' download>aaa</a>");
            $('#downloadtask').attr("href", "/Temp/" + msg);
            document.getElementById("downloadtask").click();
            $('#downloadtask').remove();
        }
    });
};
function clickperfomerfile(Id) {
    $.ajax({
        type: "POST",
        url: "/Home/SubTaskFileDownload",
        data: "id=" + Id,
        success: function (msg) {
            $('#workspace').append("<a id='downloadsubtask' download>aaa</a>");
            $('#downloadsubtask').attr("href", "/Temp/" + msg);
            document.getElementById("downloadsubtask").click();
            $('#downloadsubtask').remove();
        }
    });
};
