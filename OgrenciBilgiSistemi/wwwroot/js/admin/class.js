
$(document).ready(function () {
    TabloGetir();
});

//#region Table 
var CourseDT;
function TabloGetir() {
    CourseDT = $("#ClassTable").DataTable({
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
            url: "/Admin/Class/ClassGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "className", render: function (data, filter, row) {
                    return `<a href="/Admin/Class/Detail?id=${row.id}">${data}</a>`
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}
//#endregion

//#region Create Class
async function ClassCreate(event) {
    event.preventDefault();
    var form = document.getElementById("class-create-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Class/ClassCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Admin/Class/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region Update Class
async function ClassUpdate(event) {
    event.preventDefault();
    var form = document.getElementById("class-update-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Class/ClassUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("Id");
                window.location.href = `/Admin/Class/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region delete Class
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
                    url: '/Admin/Class/ClassDelete',
                    type: 'POST',
                    dataType: 'json',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        if (data.success) {
                            window.location.href = `/Admin/Class/Index?&success=true&message=${data.message}`;
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