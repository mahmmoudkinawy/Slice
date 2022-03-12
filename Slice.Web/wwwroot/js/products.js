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
        ],
        "width": "100%"
    });
});