// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function delayAsync(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

/* Extends Element Objects with a function named scrollIntoViewPromise
*  options: the normal scrollIntoView options without any changes
*/

Element.prototype.scrollIntoViewAsync = function (scrollOptions, observerOptions = {}) {
    this.scrollIntoView(scrollOptions);

    const defaultObserverOptions = {
        threshold: 1.0
    };

    let parent = this;

    return {
        then: function (x) {
            const intersectionObserver = new IntersectionObserver((entries) => {
                let [entry] = entries;

                if (entry.isIntersecting) {
                    setTimeout(() => { x(); intersectionObserver.unobserve(parent) }, 10);
                }
            }, Object.assign(defaultObserverOptions, observerOptions));

            intersectionObserver.observe(parent);
        }
    };
}
