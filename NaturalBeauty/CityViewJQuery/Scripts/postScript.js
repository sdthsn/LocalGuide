$(document).ready(function () {
    postData();

});

function postData() {
    $('#upload').click(function () {
        var filename = $('#loadImage').val();
        var name = $('#name').val();
        var shortSummary = $('#shortSummary').val();
        var description = $('#description').val();
        var url = 'http://localhost:51596/City/upload?name=' + name + '&shortSummery=' + shortSummary + '&description=' + description;

        $.ajax({
            type: 'POST',
            url: url,
            enctype: 'multipart/form-data',
            data: {
                file: filename
            },
            success: function (data) {
                showResult(data);
            },
            error: function (data) {
                errorFn(data)
            }
        });
    });
}

function showResult(data) {
    var htmls = '<p>' + data + '</p><br/><a href="Add.html">Add more Spots</a>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="List.html">Back to main page</a>';
    var hide = $('#input').remove();
    var result = $('#result').html(htmls);
}

function errorFn(data) {
    var hide = $('#input').remove();
    var result = $('#result').html('<p style="color:red">"Something went wrong"</p><br/><a href="Add.html">Add more Spots</a>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="List.html">Back to main page</a>');
}