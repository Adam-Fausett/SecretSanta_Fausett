// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function delayAsync(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

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
