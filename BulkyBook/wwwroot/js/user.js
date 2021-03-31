var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url" : "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" }
        ]
    })
}


function Delete(url)
{
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) =>
    {
        if (willDelete)
        {
            $.ajax(
            {
                    type: "DELETE",
                    url: url,
                    success: function (data)
                    {
                        if (data.success)
                        {
                            toastr.success(data.message);
                            dataTable.ajax.reload();
                        }
                        else
                        {
                            toastr.error(data.message);
                        }
                    }
            });
        }
    });
}
