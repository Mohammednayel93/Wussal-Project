$(function () {
    LoadData();
})


var LoadData = function () {


    $.ajax({
        url: '/Admin/ContactUs/LoadData',
        method: 'GET',
    }).done(function (data) {
       
        var dtObj = {
            data: data,
            processing: true,
            responsive: true,
            retrieve: true,
            ordering: false,
            dom: 'Bfrtip',
           
            columns: [
                { 'data': 'Id' },
                { 'data': 'Email' },
                { 'data': 'Subject' },
                { 'data': 'Message' },
               
                {
                    "render": function (data, type, row) {
                        var buttons = "<a class='btn btn-success' onclick='return Replay(" + row.Id + ")' href='#' >  Replay  </a>";
                         return buttons;
                    }
                },
                {
                    "render": function (data, type, row) {
                        var buttons = "<a class='btn btn-danger' onclick='return Delete(" + row.Id + ")' href='#' >  Delete  </a>";
                        return buttons;
                    }
                }
                , {
                    "render": function (data, type, row) {
                  
                        var buttons = "<a class='btn btn-primary' onclick='return Details(" + row.Id + ")' href='#' >  Details  </a>";
                        return buttons;
                    }
                }
            ]
        }

        $('#contactus').dataTable().fnDestroy();
        $('#contactus').dataTable(dtObj);

    }).fail(function (e) { toastr.error(e.responseText); })
}

 











var Delete = function (code) {

    bootbox.confirm({
        title: 'Delete',
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
                    url: '/Admin/ContactUs/Delete',
                    data: {
                        id: code
                    },
                    success: function (data) {


                        toastr.success("Deleted Success", "Done");
                        LoadData();

                    }

                })
            }
        }
    });

   
}

var Details = function (code) {
    $.ajax({
        type: "Get",
        url: '/Admin/ContactUS/Details',
        data: {
            id: code
        },
        success: function (data) {
    
            $('#modalMessage .modal-content').html(data);
            $('#modalMessage').modal('show');
        }
    });
}
var Replay = function (code) {
    $.ajax({
        type: "Get",
        url: '/Admin/ContactUS/Reply',
        data: {
            id: code
        },
        success: function (data) {

            $('#modalMessage .modal-content').html(data);
            $('#modalMessage').modal('show');
        }
    });
}