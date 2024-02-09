
$(document).ready(function () {
    TabloGetir();
});

//#region Table 
var CourseDT;
function TabloGetir() {
    CourseDT = $("#SemesterTable").DataTable({
        paging: true,
        select: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        info: true,
        scrollX: false,
        autoWidth: false,
        filter: true,
        processing: false,
        serverSide: false,
        order: [[0, "asc"]],
        lengthMenu: [[10, 20, 50], ['10', '20', '50']],
        ajax: {
            url: "/Admin/Semester/SemesterGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "semesterName", render: function (data, filter, row) {
                    return `<a href="/Admin/Semester/Detail?id=${row.id}">${data}</a>`
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}
//#endregion

//#region Create Semester
async function SemesterCreate(event) {
    event.preventDefault();
    var form = document.getElementById("semester-create-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Semester/SemesterCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Admin/Semester/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region Update Semester
async function SemesterUpdate(event) {
    event.preventDefault();
    var form = document.getElementById("semester-update-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Semester/SemesterUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("Id");
                window.location.href = `/Admin/Semester/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region Semester Delete

function Delete() {
    var formData = new FormData();
    formData.append("Id", $("#Id").val());
    Swal.fire({
        title: "Emin misiniz?",
        text: "Silinen veriler geri getirilemez.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'İptal',
    })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Admin/Semester/SemesterDelete',
                    type: 'POST',
                    dataType: 'json',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        if (data.success) {
                            window.location.href = `/Admin/Semester/Index?&success=true&message=${data.message}`;
                        }
                        else {
                            error(data.message)
                        }
                    }
                });

            }
            else {
                // Kullanıcı "İptal" düğmesine tıkladığında yapılacak işlemleri burada tanımlayabilirsiniz.
            }
        });
}
//#endregion