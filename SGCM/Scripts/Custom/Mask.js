$(document).ready(function () {
    var phoneMaskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
    phoneMaskOptions = {
        onKeyPress: function (val, e, field, options) {
            field.mask(phoneMaskBehavior.apply({}, arguments), options);
        }
    };

    $('.phone-mask').mask(phoneMaskBehavior, phoneMaskOptions);

    $('[data-toggle="tooltip"]').tooltip()
});