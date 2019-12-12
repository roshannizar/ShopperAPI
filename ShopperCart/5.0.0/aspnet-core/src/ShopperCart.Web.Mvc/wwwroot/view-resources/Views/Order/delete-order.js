const spinner = document.getElementById("spinner");

function showSpinner() {
    spinner.style.visibility = 'visible';
    spinner.style.display = 'inline';
    spinner.style.opacity = '0.5';
}

function closeSpinner() {
    spinner.style.visibility = 'hidden';
    spinner.style.display = 'none';
}

function DeleteOrder(id) {

    showSpinner();

    $.ajax({
        url: 'http://localhost:21021/api/v1/Order/'+parseInt(id),
        dataType: 'json',
        type: 'delete',
        contentType: 'application/json',
        data: parseInt(id),
        processData: false,
        success: function (data, textStatus, jQxhr) {
            console.log(data, textStatus, jQxhr);
            closeSpinner();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(jqXhr, textStatus, errorThrown);
            closeSpinner();
        }
    });
}