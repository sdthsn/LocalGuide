$(document).ready(function () {
    getData();


});
function getData() {
    $.ajax({
        // the URL for the request
        url: "http://localhost:51596/City/info",
        type: "GET",
        dataType: "json"
    })
        .done(inserData)
        .fail(errorFn)
        .always(function (data, textStatus, jqXHR) {
            console.log("The request is complete!");
        });
}

function inserData(data) {
    var htmls = '';
    $.each(data, function (i, item) {
        var name = item.name;
        var shortName = item.id;
        var shortDescription = item.shortSummery;
        var para1 = item.description;
        htmls += '<aside><div><span class="imageId" id="' + item.id + '"><img src="http://localhost:51596/City/download?id='+item.id + '" alt="' + name + '"></span><h3>' + name + '</h3><h5>' + shortDescription + '</h5><div class="expanding_panel" data-link-text="Read More"><p>' + para1 + '</p></div></div></aside>'
    });
    htmls += '<br/><a id="addSpot" href="Add.html">Add a Spot</a>';
    var x = $('.attractions').append(htmls);
    inject_markup();
    activate_panels();
}

function errorFn(xhr, status, strErr) {
    var hide = $('.attractions').remove();
    var result = $('#errorResult').html('<p style="color:red">Something went wrong</p><br/><a href="List.html">Try again</a>');
    console.log("There was an error!");
}

function inject_markup() {
    $('.expanding_panel').each(function () {
        var link_text = $(this).attr('data-link-text');
        var content = $(this).html();
        $(this).html('<div class="expanding_panel_content_container" style="height: 0px"><div class="expanding_panel_content">' + content + '</div></div>');
        $(this).append('<div class="expanding_panel_trigger">' + link_text + '</div>');
    });

}
function activate_panels() {
    $('.expanding_panel .expanding_panel_trigger').on('click', function () {

        var new_height = null;
        var selected_panel = $(this).closest('.expanding_panel');
        var selected_content = selected_panel.find('.expanding_panel_content_container');
        selected_panel.toggleClass('open');
        if (selected_panel.hasClass('open')) {
            new_height = selected_panel.find('.expanding_panel_content').outerHeight(true)
        } else {
            new_height = 0;
        }
        selected_content.animate({ 'height': new_height + 'px' }, 2000, function () {
            if (new_height != 0) {
                $(this).removeAttr('style');
            }
        });

    });

}


