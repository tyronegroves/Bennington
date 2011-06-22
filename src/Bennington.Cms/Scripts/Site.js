
(function ($) {

    $(window).load(function () {
        $('#data_table').find('tr:odd').addClass('odd');
        $('#data_table').find('tr:even').addClass('even');
    });

    $(window).load(function () {
        $('.input-validation-error').parent('td').parent('tr').addClass('row-validation-error');

    });

    $(document).ready(function () {
        $(function () {
            $(".is_a_date").datepicker();
        });
    });

    $(document).ready(function () {

        $('.console .addbutton').click(function () {
            $(this).parent().parent().find('.exclude option:selected').remove().appendTo('.include');
        });
        $('.console .removebutton').click(function () {
            $(this).parent().parent().find('.include option:selected').remove().appendTo('.exclude');
        });

        $('form').submit(function () {
            $('.include option').each(function (i) {
                $(this).attr("selected", "selected");
            });
        }); 
    });




} (jQuery));