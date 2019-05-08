$(function () {
    var notificationhub = $.connection.myHub;
    notificationhub.client.message = function (message) {
        notifSet(message);
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "0",
            "extendedTimeOut": "0",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        toastr["info"](message);
    };
    $.connection.hub.start();

});
function notifyMe(message) {
    var notification = new Notification(message, {
        tag : "ache-mail",
    body : "Проверь скорее!"
});
}

function notifSet(message) {
    if (!("Notification" in window)) {
        alert(message);
    }   
    else if (Notification.permission === "granted")
        setTimeout(notifyMe(message), 2000);
    else if (Notification.permission !== "denied") {
        Notification.requestPermission(function (permission) {
            if (!('permission' in Notification))
                Notification.permission = permission;
            if (permission === "granted")
                setTimeout(notifyMe(message), 2000);
        });
    }
}