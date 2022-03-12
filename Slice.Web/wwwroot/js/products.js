$(document).ready(function () {
    $('#loadData').DataTable({
        "ajax": {
            "url": "/api/products",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "25%" },
            { "data": "foodType.name", "width": "25%" },
            { "data": "category.name", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" >
                            <a href="/Admin/products/upsert?id=${data}"  class="btn btn-success text-white mx-2">
                            <i class="bi bi-pencil-square"></i>  </a>
                            <a   class="btn btn-danger text-white mx-2">
                             <i class="bi bi-trash-fill"></i>  </a>
                            </div>`
                },

                "width": "15%"
            }
        ],
        "width": "100%"
    });
});