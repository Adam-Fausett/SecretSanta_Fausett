ko.bindingHandlers.editableHTML = {
  init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
    var $element = $(element);
    var initialValue = ko.utils.unwrapObservable(valueAccessor());
    var $origObservable = bindingContext.$rawData;

    $element.html(initialValue);

    $element.on('keyup blur', function() {
      var curVal = valueAccessor();
      var newVal = $element.html();

      if (ko.isObservable($origObservable)) {
          $origObservable(newVal);
      } else {
          curVal($element.html());
      }
    });
  }
};
