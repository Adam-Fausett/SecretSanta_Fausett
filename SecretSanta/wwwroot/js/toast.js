var TOAST = (function () {
    var MessageType = {
        Info: {
            localStorageKey: 'TOAST-InfoMessage',
            class: 'info',
            glyphicon: 'glyphicon',
            showClose: false,
        },
        Success: {
            localStorageKey: 'TOAST-SuccessMessage',
            class: 'success',
            glyphicon: 'glyphicon-ok',
            showClose: false,
        },
        Warning: {
            localStorageKey: 'TOAST-WarningMessage',
            class: 'warning',
            glyphicon: 'glyphicon-warning-sign',
            showClose: true,
        },
        Error: {
            localStorageKey: 'TOAST-ErrorMessage',
            class: 'danger',
            glyphicon: 'glyphicon-warning-sign',
            showClose: true,
            minimumToastMillis: 5000
        },
    };

    var notificationSummary = $('.error-summary #summary');
    var lastNotificaitonID = 0;
    var notifications = [];

    function removeToast(id) {
        var element = $('#summaryelement_' + id);
        element.fadeOut(function () {
            element.remove();
        });
        notifications[id] = null;
    }

    function showToast(message, forceClose, messageType) {
        messageType = messageType || MessageType.Info;
        if (message && message.trim().length > 0) {
            var notificationID = lastNotificaitonID++;

            notificationSummary.prepend('<div id="summaryelement_' + notificationID + '" class="clearfix"><div class="alert alert-' + messageType.class + ' float-right" style="position: relative;' + (forceClose || messageType.showClose ? ' padding-right: 26px;' : '') + '"><span class="glyphicon ' + messageType.glyphicon + '"></span>' + (messageType.glyphicon ? '&nbsp;&nbsp;' : '') + message + (forceClose || messageType.showClose ? '&nbsp;&nbsp;<button type="button" class="close" style="position: absolute; top: 8px; right: 8px;" onclick="TOAST.removeToast(' + notificationID + ');">&times;</button>' : '') + '</div></div>');

            if (!forceClose) {
                var minToastMillis = (messageType.minimumToastMillis || 3000);
                var maxToastMillis = 10000;

                var wordsPerMinute = 200;
                var wordsPerSecond = wordsPerMinute / 60;

                var numWords = (message.match(/\s/g) || []).length + 1;

                var toastLength = (numWords / wordsPerSecond) * 1000;

                if (!toastLength || toastLength < minToastMillis) {
                    toastLength = minToastMillis;
                }
                else if (toastLength > maxToastMillis) {
                    toastLength = maxToastMillis;
                }

                notifications.push({
                    notificationID: notificationID,
                    toastLength: toastLength
                });

                if (notifications.length === 1) {
                    var fn = function (notificationID, toastLength) {
                        setTimeout(function () {
                            var element = $('#summaryelement_' + notificationID);
                            element.fadeOut(function () {
                                element.remove();
                            });

                            notifications[notificationID] = null;

                            var newNotificationID = notificationID + 1;
                            while (newNotificationID < notifications.length && !notifications[newNotificationID] && newNotificationID < lastNotificaitonID) {
                                ++newNotificationID;
                            }

                            if (newNotificationID < lastNotificaitonID) {
                                fn(notifications[newNotificationID].notificationID, notifications[newNotificationID].toastLength);
                            }
                            else {
                                notifications = [];
                                lastNotificaitonID = 0;
                            }
                        }, toastLength);
                    }
                    fn(notificationID, toastLength);
                }
            }
        }
    }

    //#region Toast Helper Methods
    function showSuccess(message, forceClose) {
        showToast(message, forceClose, MessageType.Success);
    }

    function showError(message, forceClose) {
        showToast(message, forceClose, MessageType.Error);
    }

    function showWarning(message, forceClose) {
        showToast(message, forceClose, MessageType.Warning);
    }

    function showInfo(message, forceClose) {
        showToast(message, forceClose, MessageType.Info);
    }
    //#endregion

    return {
        remove: removeToast,
        success: showSuccess,
        error: showError,
        warn: showWarning,
        info: showInfo,
    }
})();