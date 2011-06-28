
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
            $(this).parent().parent().find('.exclude option:selected').remove().appendTo($(this).parent().parent().find('.include'));
        });
        $('.console .removebutton').click(function () {
            $(this).parent().parent().find('.include option:selected').remove().appendTo($(this).parent().parent().find('.exclude'));
        });

        $('form').submit(function () {
            $('.include option').each(function (i) {
                $(this).attr("selected", "selected");
            });

        });


        $(function () {
            $('textarea.tinymce').tinymce({
                // Location of TinyMCE script
                script_url: '/MANAGE/Scripts/tiny_mce/tiny_mce.js',

                // General options
                theme: "advanced",
                plugins: "",

                // Theme options
                //            theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
                //            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
                //            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
                //            theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
                theme_advanced_toolbar_location: "top",
                theme_advanced_toolbar_align: "left",
                theme_advanced_statusbar_location: "bottom",
                theme_advanced_resizing: true,

                // Example content CSS (should be your site CSS)
                //content_css: "/Content/ManageSite.css",

                // Drop lists for link/image/media/template dialogs
                //            template_external_list_url: "lists/template_list.js",
                //            external_link_list_url: "lists/link_list.js",
                //            external_image_list_url: "lists/image_list.js",
                //            media_external_list_url: "lists/media_list.js",

                // Replace values for the template plugin
//                template_replace_values: {
//                    username: "Some User",
//                    staffid: "991234"
//                }
            });


        });

    });


//    var urlParams = {};
    $(document).ready(function () {

         $('.search_by_button').click(function () {
            var windowLocation = window.location + '';
            var url = windowLocation.substring(0, windowLocation.indexOf('?'));
            var querystring = window.location.search.substring(1);
            var urlParams = getUrlParams(querystring);
            var newParams = getUrlParams('page=0&sortBy=&SearchBy=' + $('select[name="searchBy"]').val() + '&searchValue=' + $('input[name="searchValue"]').val());


            for(var name in newParams){
               urlParams[name] = newParams[name];
            }
            
            var newQuerystring = '?';
            for(var name in urlParams){
               newQuerystring += name + '=' + urlParams[name] + '&';
            }
            window.location = url + newQuerystring;
         });



//      

});


} (jQuery));

function getUrlParams(url){
   var urlParams = {};
   var e,
       a = /\+/g,  // Regex for replacing addition symbol with a space
       r = /([^&=]+)=?([^&]*)/g,
       d = function (s) { return decodeURIComponent(s.replace(a, " ")); },
       //q = window.location.search.substring(1);
       q = url;

    while (e = r.exec(q))
       urlParams[d(e[1])] = d(e[2]);

    return urlParams;
}