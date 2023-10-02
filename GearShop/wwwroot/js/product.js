﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'description', "width": "20%" },
            {
                data: 'price', render: function (data) {
                    return '$' + data;
                }
                , "width": "10%"
            },
            {
                data: 'imgUrl', "render": function (data) {
                    return '<img src="' + data + '" width="80px">';
                },
                "width": "15%"
            },
            { data: 'stock', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/product/CreateAndUpdate?id=${data}" class="btn btn-success rounded-pill mx-2">
                                    <i class="bi bi-pencil"></i> 
                                </a>
                                <a onClick=Delete('/admin/product/delete?id=${data}') class="btn btn-danger rounded-pill mx-2">
                                    <i class="bi bi-trash"></i> 
                                </a>
                            </div>`
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}


