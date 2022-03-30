var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadData("cancelled");
    } else if (url.includes("completed")) {
        loadData("completed");
    } else if (url.includes("ready")) {
        loadData("ready");
    } else {
        loadData("inProcess");
    }
});


function loadData(status) {
    dataTable = $('#loadData').DataTable({
        "ajax": {
            "url": "/api/Orders?status=" + status,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "pickUpName", "width": "15%" },
            { "data": "appUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpTime", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" >
                            <a href="/Admin/Orders/OrderDetail?id=${data}" class="btn btn-success text-white mx-2">
                            <i class="bi bi-pencil-square"></i>  </a>
                            </div>`
                },

                "width": "15%"
            }
        ],
        "width": "100%"
    });
}