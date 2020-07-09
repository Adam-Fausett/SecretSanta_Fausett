// base AJAX setup
$.ajaxSetup({
    success: function (data, status, xhr) {
        //console.debug('AJAX success', this.url, `Status: (${xhr.status}) ${xhr.statusText}`);
    },
    error: function (xhr, status, err) {
        var exceptionMessage;
        try {
            exceptionMessage = xhr.responseJSON.ExceptionMessage || xhr.responseJSON.Message || xhr.responseText;
        }
        catch (ex) { }

        console.error('AJAX error', this.url, `Status: (${xhr.status}) ${xhr.statusText}`, (exceptionMessage ? + exceptionMessage : ''));
    }
});

// show loading text on button when doing ajax call (data-loading-text)
function loadingButtonAjax(ajaxSettings) {
    var minLoadingTime = 800;
    var timestamp = Date.now();
    var $this = $(this);
    $this.button('loading');
    return new Promise((resolve, reject) => {
        $.ajax(ajaxSettings)
            .then(function (data) {
                setTimeout(function () {
                    $this.button('reset');
                    var successMsg = $this.attr('data-success-text');
                    if (successMsg) {
                        TOAST.success(successMsg);
                    }
                    resolve(data);
                }, minLoadingTime - (Date.now() - timestamp));
            })
            .fail(function (err) {
                //console.error(err);
                $this.button('reset');
                var errorMsg = $this.attr('data-error-text');
                if (errorMsg) {
                    TOAST.error(errorMsg);
                }
                else {
                    var exceptionMessage = err.responseJSON && err.responseJSON.ExceptionMessage || err.responseJSON && err.responseJSON.Message || err.responseText || err.statusText || err;
                    if (exceptionMessage) {
                        try {
                            exceptionMessage = JSON.parse(exceptionMessage);
                        } catch (ex) { }
                        TOAST.error('An error occurred attempting the requested task' + (exceptionMessage ? ':<br /> ' + exceptionMessage : '&hellip;'));
                    }
                    else {
                        TOAST.error('Unknown error occurred');
                    }
                }

                reject(err);
            });
    });
}
