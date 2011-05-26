
(function ($) {

    $(window).load(function () {
        $('#data_table').find('tr:odd').addClass('odd');
        $('#data_table').find('tr:even').addClass('even');
    });

} (jQuery));