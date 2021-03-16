var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url" : "/Admin/ApplicationUser/GetAll"
        },
        "columns": [
            { "data": "userName", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "postalCode", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ApplicationUser/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onClick=Delete("/Admin/ApplicationUser/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
`;
                },"width":"40%"
            }
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
