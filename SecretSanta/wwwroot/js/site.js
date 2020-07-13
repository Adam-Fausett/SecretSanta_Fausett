// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function delayAsync(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

/**
 * Show error messages in a nice message box.
 * (requires sweetalert2)
 * @param {any} error
 */
function showError(error) {
    Swal.fire({
        title: 'Oh No!',
        html:
            `<div class="error-messages">
                <div class="col-12">
                    <p>${error.message}</p>
                </div>
            </div>`,
        icon: 'error',
        confirmButtonText: 'OK'
    });
}

function isMobileDevice() {
    return navigator.userAgent.match(/Mobi/);
};

/**
  * Extends Element Objects with a function named scrollIntoViewAsync
  *
  * @param {Object} scrollOptions - the normal scrollIntoView options
  * @param {Object} observerOptions - IntersectionObserver options
  */
Element.prototype.scrollIntoViewAsync = function (scrollOptions, observerOptions = {}) {
    this.scrollIntoView(scrollOptions);

    const element = this;

    const defaultObserverOptions = {
        threshold: 1.0
    };

    return new Promise((resolve, reject) => {
        try {
            const intersectionObserver = new IntersectionObserver((entries) => {
                    let [entry] = entries;

                    if (entry.isIntersecting) {
                        setTimeout(() => {
                                resolve();
                                intersectionObserver.unobserve(element);
                        }, 10);
                    }
                },
                Object.assign(defaultObserverOptions, observerOptions));

            intersectionObserver.observe(element);
        } catch (error) {
            reject(error);
        }
    });
}
