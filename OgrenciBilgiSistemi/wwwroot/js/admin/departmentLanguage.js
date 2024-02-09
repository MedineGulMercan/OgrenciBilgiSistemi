
$(document).ready(function () {
    TabloGetir();
});

var DepartmentLanguageDT;
function TabloGetir() {
    DepartmentLanguageDT = $("#DepartmentLanguageTable").DataTable({
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
            url: "/Admin/DepartmentLanguage/DepartmentLanguageGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "languageName", render: function (data, filter, row) {
                    console.log(data);
                    return `<a href="/Admin/DepartmentLanguage/Detail?id=${row.id}">${data}</a>`
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}

//#region Create Course
async function DepartmentLanguageCreate(event) {
    event.preventDefault();
    var form = document.getElementById("departmentLanguage-create-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/DepartmentLanguage/DepartmentLanguageCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Admin/DepartmentLanguage/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region DepartmentLanguage Update
async function DepartmentLanguageUpdate(event) {
    event.preventDefault();
    var form = document.getElementById("departmentLanguage-update-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/DepartmentLanguage/DepartmentLanguageUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("Id");
                window.location.href = `/Admin/DepartmentLanguage/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion
//#region DepartmentLanguage Delete

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
                    url: '/Admin/DepartmentLanguage/DepartmentLanguageDelete',
                    type: 'POST',
                    dataType: 'json',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        if (data.success) {
                            window.location.href = `/Admin/DepartmentLanguage/Index?&success=true&message=${data.message}`;
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