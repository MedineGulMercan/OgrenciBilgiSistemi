$(document).ready(function () {
    TabloGetir();
});

var CourseRegistrationDT;
function TabloGetir() {
    CourseRegistrationDT = $("#CourseRegistrationTable").DataTable({
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
            url: "/Admin/CourseRegistration/StudentGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "studentName", render: function (data, filter, row) {
                    console.log(data);
                    return `<a href="/Admin/CourseRegistration/Detail?id=${row.studentId}">${data}</a>`
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}

async function RegistrationApproved() {
    event.preventDefault();
    studentId = document.getElementById("student-name").value;
    var formData = new FormData();
    formData.append("id", studentId);
    await $.ajax({
        url: '/Admin/CourseRegistration/RegistrationApproved',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("id");
                window.location.href = "/Admin/CourseRegistration/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });

}


async function DeleteRegister() {
    event.preventDefault();
    studentId = document.getElementById("student-name").value;
    var formData = new FormData();
    formData.append("id", studentId);
    await $.ajax({
        url: '/Admin/CourseRegistration/DeleteRegister',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("id");
                window.location.href = "/Admin/CourseRegistration/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });

}
