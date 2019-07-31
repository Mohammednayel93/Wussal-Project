$(function () {
    LoadDataGrid();
})

var LoadDataGrid = function () {


    $.ajax({
        url: '/Admin/SecurityUser_Info/LoadDataGrid',
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
                { 'data': 'Code' },
                { 'data': 'UserName' },
                { 'data': 'Sec_Group' },
                { 'data': 'Notes' },
                {
                    'data': 'Code',
                    'searchable': false,
                    'sortable': false,
                    'render': (code) => {

                        var detailsTxt = $('#hiddenTxts #txtDetails').val(),
                            editTxt = $('#hiddenTxts #txtEdit').val()
                        deleteTxt = $('#hiddenTxts #txtDelete').val();

                        var ul = '<ul class="list-unstyled list-inline">';
                        ul += '<li><a href="javascript:void(0)"  onclick="return ViewDetails(' + code + ')"  title="' + detailsTxt + '" class="text-info"><i class="fa fa-info"></i></a></li>';
                        ul += '<li><a href="javascript:void(0)"  onclick="return EditRow(' + code + ')"  title="' + editTxt + '" class="text-success"><i class="fa fa-edit"></i></a></li>';
                        ul += '<li><a href="javascript:void(0)"  onclick="return DeleteRow(' + code + ')"  title="' + deleteTxt + '" class="text-danger"><i class="fa fa-trash"></i></a></li>';

                        return ul;
                    }
                }

            ]
        }

        $('#tblgrp').dataTable().fnDestroy();
        $('#tblgrp').dataTable(dtObj);

    }).fail(function (e) { toastr.error(e.responseText); })
}


var ViewDetails = function (code) {
    $.ajax({
        type: "Get",
        url: '/Admin/SecurityUser_Info/ViewSecurityUserDetails',
        data: {
            success: function (data) {
                id: code
            },


            $('#modal-details .modal-body').html(data);

            $('#modal-details input').attr('disabled', true);
            $('#modal-details textarea').attr('disabled', true);
            $('#modal-details select').attr('disabled', true);

            $('#modal-details').modal('show');

        }
    });
}


var EditRow = function (code) {
    $.ajax({
        type: "Get",
        url: '/Admin/SecurityUser_Info/ViewSecurityUserEdit',
        data: {
            id: code
        },
        success: function (data) {

            $('#modal-edit .modal-body').html(data);
            $('#modal-edit').modal('show');
        }
    });
}

var DeleteRow = function (code) {

    var confirmTxt = $('#hiddenTxts #txtConfirmDeletion').val(),
        yesTxt = $('#hiddenTxts #txtYes').val(),
        noText = $('#hiddenTxts #txtNo').val(),
        deleteTxt = $('#hiddenTxts #txtDelete').val();

    bootbox.confirm({
        title: deleteTxt,
        message: confirmTxt,
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> ' + noText
            },
            confirm: {
                label: '<i class="fa fa-check"></i>' + yesTxt
            }
        },
        callback: function (result) {

            if (result) {

                $.ajax({
                    type: "post",
                    url: '/Admin/SecurityUser_Info/DeleteSecurityUser',
                    data: {
                        id: code
                    },
                    success: function (response) {

                        if (response.Success) {
                            toastr.success(response.message);
                            LoadDataGrid();
                        }
                        else {
                            toastr.error(response.message);
                        }
                    }

                })
            }
        }
    });
}
