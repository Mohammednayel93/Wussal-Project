
$(document).ready(function () {

    LoadDataGridEmplyee();

});

var LoadDataGridEmplyee = function () {

    $.ajax({
        url: '/test/LoadData',
        method: 'GET'
    }).done(function (data) {

        var obj = {

            data: data,
            ordering: false,
            ServerSide: true,
            processing: true,
            bDestroy: true,
            columns: [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "Email", "name": "Email", "autoWidth": true },
                { "data": "Gender", "name": "Gender", "autoWidth": true },
                { "data": "City", "name": "City", "autoWidth": true },
                {
                    "render": function (data, type, row) { return "<a class='btn btn-info'  href='#' >  Edit  </a>"; }
                },
                {
                    "render": function (data, type, row) { return "<a href='#' class='btn btn-danger'  >  Delete  </a>"; }
                },
                {
                    "render": function (data, type, row) { return "<a href='#'  class='btn btn-primary'   >  Details  </a>"; }
                }


            ]

        };

        $('#table1').DataTable(obj);

    })
}
