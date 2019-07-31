

$(function () {
    LoadDataGrid();
})

var LoadDataGrid = function () {


    $.ajax({
        url: '/Admin/Product/LoadDataGrid',
        method: 'GET',
    }).done(function (data) {

        var dtObj = {
            data: data,
            processing: true,
            responsive: true,
            retrieve: true,
            ordering: false,
            dom: 'Bfrtip',
            buttons: [
                { "extend": 'pdf', "text": 'PDF', "className": 'btn btnPrint btn-success btn-sm' },
                { "extend": 'copy', "text": 'Copy', "className": 'btn btnPrint btn-success btn-sm' },
                { "extend": 'excel', "text": 'Excel', "className": 'btn btnPrint btn-success btn-sm' },
                { "extend": 'csv', "text": 'CSV', "className": 'btn btnPrint btn-success btn-sm' },
                { "extend": 'print', "text": 'Print', "className": 'btn btnPrint btn-success btn-sm' },
            ],
            columns: [
                { 'data': 'Id' },
                { 'data': 'Name' },
                { 'data': 'Date' },
                { 'data': 'Category' },
                { 'data': 'Price' },
                { 'data': 'UserName' },
                {
                    "render": function (data, type, row) {
                        var buttons = "<a class='btn btn-success' onclick='return Approve(" + row.Id + ")' href='#' >  Approve  </a>";

                        return buttons;
                    }
                },
                {
                    "render": function (data, type, row) {
                        var buttons = "<a class='btn btn-danger' onclick='return Delete(" + row.Id + ")' href='#' >  Delete  </a>";
                        return buttons;
                    }
                },
                {
                    "render": function (data, type, row) {
                        var buttons = "<a class='btn btn-primary' onclick='return Details(" + row.Id + ")' href='#' >  Details  </a>";
                        return buttons;
                    }
                }

            ]
        }

        $('#table1').dataTable().fnDestroy();
        $('#table1').dataTable(dtObj);

    }).fail(function (e) { toastr.error(e.responseText); })
}

var Approve = function (code) {
    $.ajax({
        type: "Get",
        url: '/Admin/Product/Approve',
        data: {
            id: code
        },
        success: function (data) {
            toastr.success("Success", "Done");
            LoadDataGrid();

        }
    });
}
var Details = function (code) {
    $.ajax({
        type: "Get",
        url: '/Admin/Product/Details',
        data: {
            id: code
        },
        success: function (data) {
            $('#modalMessage .modal-content').html(data);
            $('#modalMessage').modal('show');
        }
    });
}
var Delete = function (code) {

    bootbox.confirm({
        title: 'Deleted',
        message: "Are You Sure To Delete ? ",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Cancel'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Confirm'
            }
        },
        callback: function (result) {

            if (result) {

                $.ajax({
                    type: "Get",
                    url: '/Admin/Product/Delete',
                    data: {
                        id: code
                    },
                    success: function (data) {


                        toastr.success("Success", "Done");
                        LoadDataGrid();

                    }

                })
            }
        }
    });

    //$('#modal-edit .modal-body').html(data);
    //$('#modal-edit').modal('show');
}
