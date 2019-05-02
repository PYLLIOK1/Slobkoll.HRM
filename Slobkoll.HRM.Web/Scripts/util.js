$(function () {
    var notificationhub = $.connection.myHub;
    notificationhub.client.message = function (message) {
        notifSet(message);
        alert(message);
    };
    $.connection.hub.start();

});
function notifyMe(message) {
    var notification = new Notification(message, {
        tag : "ache-mail",
    body : "Проверь скорее!",
});
}

function notifSet(message) {
    if (!("Notification" in window))
        alert("Ваш браузер не поддерживает уведомления.");
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