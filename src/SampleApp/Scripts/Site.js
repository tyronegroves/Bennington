
(function ($) {

    $(document).ready(function () {
        setup_the_edit_forms();
        setup_the_data_tables();
    });

    // the code below is for the calls made in the document.ready above

    function setup_the_edit_forms(){
        apply_validation_errors_to_the_containing_row();
        setup_the_date_picker();
        setup_the_consoles();
        setup_tiny_mce();
    }

    function setup_the_data_tables(){
        make_the_rows_in_a_data_table_alternate_css_styles();
        make_the_data_table_links_share_all_pagination_and_sorting_and_searching_values();
        make_the_search_by_button_work();
        make_hitting_enter_on_the_search_form_initiate_a_search_form();
    }

    function make_the_rows_in_a_data_table_alternate_css_styles() {
        $('#data_table').find('tr:odd').addClass('odd');
        $('#data_table').find('tr:even').addClass('even');
    }

    function apply_validation_errors_to_the_containing_row() {
        $('.input-validation-error').parent('td').parent('tr').addClass('row-validation-error');
    }

    function setup_the_date_picker() {
        $(".is_a_date").datepicker();
    }

    function setup_the_consoles() {
        make_the_console_add_and_remove_buttons_work();
        submitting_a_form_should_select_all_console_options_on_the_include_side();
    }

    function make_the_console_add_and_remove_buttons_work() {
        $('.console .addbutton').click(function () {
            $(this).parent().parent().find('.exclude option:selected').remove().appendTo($(this).parent().parent().find('.include'));
        });
        $('.console .removebutton').click(function () {
            $(this).parent().parent().find('.include option:selected').remove().appendTo($(this).parent().parent().find('.exclude'));
        });
    }

    function submitting_a_form_should_select_all_console_options_on_the_include_side() {
        $('form').submit(function () {
            $('.include option').each(function (i) {
                $(this).attr("selected", "selected");
            });
        });
    }

    function setup_tiny_mce() {
        $(function () {
            $('textarea.tinymce').tinymce({
                // Location of TinyMCE script
                script_url: '/MANAGE/Scripts/tiny_mce/tiny_mce.js',

                // General options
                theme: "advanced",
                plugins: "",
                theme_advanced_toolbar_location: "top",
                theme_advanced_toolbar_align: "left",
                theme_advanced_statusbar_location: "bottom",
                theme_advanced_resizing: true,
            });
        });
    }

    function make_the_data_table_links_share_all_pagination_and_sorting_and_searching_values(){
        $('.data_table a').each(function (index) {
            build_the_url($(this));
        });

        $('.PagedList-pager a').each(function (index) {
            build_the_url($(this));
        });

        function build_the_url($selector) {
            var current_href = $selector.attr('href');

            if (current_href == undefined || current_href == '') return;

            $selector.attr('href', '?x'
                + getTheValue(current_href, 'page')
                + getTheValue(current_href, 'sortBy')
                + getTheValue(current_href, 'searchBy')
                + getTheValue(current_href, 'searchValue')
                + getTheValue(current_href, 'sortOrder'));
        }

        function getTheValue(url, id) {
            if (parseUri(url).queryKey[id] != undefined)
                return '&' + id + '=' + parseUri(url).queryKey[id];

            if (jQuery.url.param(id) != undefined)
                return '&' + id + '=' + jQuery.url.param(id);

            return '';
        }
    }

    function make_the_search_by_button_work() {
        $('.search_by_button').click(function () {
            var windowLocation = window.location + '';
            var url = windowLocation.substring(0, windowLocation.indexOf('?'));
            window.location = url + '?searchBy=' + $('select[name="searchBy"]').val() + '&searchValue=' + $('input[name="searchValue"]').val();
        });
    }

    function make_hitting_enter_on_the_search_form_initiate_a_search_form(){
        $('.searchForm').parent('form').submit(function() {
            $('.search_by_button').click();
            return false;
        });
    }

} (jQuery));