
(function ($) {

    $(window).load(function () {
        $('#data_table').find('tr:odd').addClass('odd');
        $('#data_table').find('tr:even').addClass('even');
    });

    $(window).load(function () {
        $('.input-validation-error').parent('td').parent('tr').addClass('row-validation-error');

    });

} (jQuery));