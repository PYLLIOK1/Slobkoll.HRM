$('.showauthor').click(function () {
    $(this).find('span').text(function (_, value) { return value === '-' ? '+' : '-'; });
    $('.higauthor').slideToggle(200, function () {
    });
});
$('.showperfomen').click(function () {
    $(this).find('span').text(function (_, value) { return value === '-' ? '+' : '-'; });
    $('.higperfomen').slideToggle(200, function () {
    });
});
$('.showarchive').click(function () {
    $(this).find('span').text(function (_, value) { return value === '-' ? '+' : '-'; });
    $('.higarchive').slideToggle(200, function () {
    });
});
$('.showobserver').click(function () {
    $(this).find('span').text(function (_, value) { return value === '-' ? '+' : '-'; });
    $('.higobserver').slideToggle(200, function () {
    });
});
$('.showobserverarchive').click(function () {
    $(this).find('span').text(function (_, value) { return value === '-' ? '+' : '-'; });
    $('.higobserverarchive').slideToggle(200, function () {
    });
});

$('.higauthor').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskAuthor?id=' + id);
    if ($(this).find('#img').hasClass('false')) {
        $(this).find('#img').replaceWith("<img id='img' src='/Content/1.png' class='img-responsive center-block' width='30' height='30' />");
    }


});
$('.higperfomen').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskPerfomer?id=' + id);
    if ($(this).find('#img').hasClass('false')) {
        $(this).find('#img').replaceWith("<img id='img' src='/Content/1.png' class='img-responsive center-block' width='30' height='30' />");
    }
});
$('.higarchive').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskArchive?id=' + id);
});
$('.higobserver').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskObserver?id=' + id);
});
$('.higobserverarchive').children('tr').click(function () {
    var id = $(this).find('.id').attr('id');
    $('#workspace').load('TaskArchiveObserver?id=' + id);
});

function clickauthorfile(Id) {
    $.ajax({
        type: "POST",
        url: "/Home/TaskFileDownload",
        data: "id=" + Id,
        success: function (msg) {
            $('#workspace').append("<a id='downloadtask' download>aaa</a>");
            $('#downloadtask').attr("href", msg);
            document.getElementById("downloadtask").click();
            $('#downloadtask').remove();
        }
    });
}
function clickperfomerfile(Id) {
    $.ajax({
        type: "POST",
        url: "/Home/SubTaskFileDownload",
        data: "id=" + Id,
        success: function (msg) {
            $('#workspace').append("<a id='downloadsubtask' download>aaa</a>");
            $('#downloadsubtask').attr("href", msg);
            document.getElementById("downloadsubtask").click();
            $('#downloadsubtask').remove();
        }
    });
}

function onChangeEdit(Id, text) {
    $.ajax({
        type: "POST",
        url: "/Home/EditStatusPerfomer",
        data: {
            Id: Id,
            text: text
        },
        success: function (id) {
            $('#workspace').load('TaskAuthor?id=' + id);
        }
    });
}
function OnSuccess(data) {
    $('#workspace').load('TaskPerfomer?id=' + data);
}

function AddCommentAuthor(idsub, text, id) {
    $.ajax({
        type: "POST",
        url: "/Home/AddCommentAuthor",
        data: {
            idSubTask: idsub,
            commentText: text,
            idTask: id
        },
        success: function (id) {
            $('#workspace').load('TaskAuthor?id=' + id);
        }
    });
}
function AddCommentPerfomer(idsub, text, id) {
    $.ajax({
        type: "POST",
        url: "/Home/AddCommentPErfomer",
        data: {
            idSubTask: idsub,
            commentText: text,
            idTask: id
        },
        success: function (id) {
            $('#workspace').load('TaskPerfomer?id=' + id);
        }
    });
}
