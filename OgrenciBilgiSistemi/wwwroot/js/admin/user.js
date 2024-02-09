
$(document).ready(function () {
    TabloGetir();
});
class TeacherCourseCreateDto {
    constructor(TeacherId, CourseId) {
        this.TeacherId = TeacherId;
        this.CourseId = CourseId;
    }
}
async function UserCreate(event) {
    event.preventDefault();
    var form = document.getElementById("user-create-form"); // Formu burada yakala
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/User/UserCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Admin/User/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
async function UserUpdate(event) {
    event.preventDefault();
    var form = document.getElementById("user-update-form"); // Formu burada yakala
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/User/UserUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("UserId");
                window.location.href = `/Admin/User/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}

//#region Table 
var CourseDT;
function TabloGetir() {
    CourseDT = $("#user-table").DataTable({
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
            url: "/Admin/User/UserGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "name", render: function (data, filter, row) {
                    return `<a href="/Admin/User/Detail?id=${row.id}">${data} ${row.surname}</a>`
                },
            },
            { data: "tc" },
            { data: "emailAddress" },
            {
                data: "birthday", render: function (data, filter, row) {
                    var formattedDate = new Date(data).toLocaleDateString("tr-TR");
                    return formattedDate;
                },
            },
            { data: "phoneNumber" },
            { data: "roleName" },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}
//#endregion

//kullanıcı eklerken seçilen labela göre inputlar değişiyor.
function ChangeAddModal() {
    var roleName = $("#RoleId option:selected").text();
    if (roleName == 'Öğretmen') {
        document.getElementById("class-div").style.display = 'none';
        document.getElementById("department-div").style.display = 'none';
        $("#name-label").text('Öğretmen Adı:');
        $("#surname-label").text('Öğretmen Soyadı:');
        $("#tc-label").text('Öğretmen TC:');
        $("#email-address-label").text('Öğretmen E-Mail Adres:');
        $("#birthday-label").text('Öğretmen Doğum Tarihi:');
        $("#tel-label").text('Öğretmen Telefon No:');
    }
    if (roleName == 'Öğrenci') {
        document.getElementById("class-div").style.display = 'block';
        document.getElementById("department-div").style.display = 'block';
        $("#name-label").text('Öğrenci Adı:');
        $("#surname-label").text('Öğrenci Soyadı:');
        $("#tc-label").text('Öğrenci TC:');
        $("#email-address-label").text('Öğrenci E-Mail Adres:');
        $("#birthday-label").text('Öğrenci Doğum Tarihi:');
        $("#tel-label").text('Öğrenci Telefon No:');
    }
}

            
//function AddRowTeacherTable(event) {
//    event.preventDefault();

//    var tablebody = document.getElementById("teacher-course-table-body");
//    if (tablebody) {
//        var teacherId = $("#TeacherId").val();
//        var courseId = $("#CourseId").val();
//        var courseName = $("#CourseId option:selected").text();

//        var row = document.createElement("tr");
//        row.innerHTML = `
//        <td hidden><input type="hidden" id="department-id" value="${teacherId}" /></td>
//        <td hidden><input type="hidden" id="semester-id" value="${courseId}" /></td>
//        <td>${courseName}<input type="hidden" id="course-id" value="${courseName}" /></td>
//        <th style="text-align:center;"><i onclick="DeleteRow(this)" class="fa fa-trash" aria-hidden="true"></i></th>`;

//        tablebody.appendChild(row);
//    }
//}

//function DeleteRow(btn) {
//    var row = btn.parentNode.parentNode;
//    row.parentNode.removeChild(row);
//}


//function TeacherCoursesTableGetData() {
//    // Bu fonksiyon, tablodaki satırları okuyup her bir satırı TeacherCourseCreateDto'ya çevirir.
//    var tableBody = document.getElementById("teacher-course-table-body");
//    var rows = tableBody.getElementsByTagName("tr");
//    var dataObjects = [];

//    var teacherDataId = $("#TeacherId").val();
//    var teacherData = new TeacherCourseCreateDto(teacherDataId, null);
//    dataObjects.push(teacherData)

//    for (var i = 0; i < rows.length; i++) {
//        var row = rows[i];

//        // Satırdaki input elemanlarını seç
//        var inputs = row.getElementsByTagName("input");

//        // Input değerlerini al
//        var teacherId = inputs[0].value;
//        var courseId = inputs[1].value;

//        // Yeni TeacherCourseCreateDto nesnesi oluştur ve diziye ekle
//        var dto = new TeacherCourseCreateDto(teacherId, courseId);
//        dataObjects.push(dto);
//    }
//    return dataObjects;
//}

//async function TeacherCoursesTableUpdate(event) {
//    event.preventDefault();
//    var teacherCourseCreateDto = TeacherCoursesTableGetData();
//    if (teacherCourseCreateDto != null && teacherCourseCreateDto.length > 0) {
//        await $.ajax({
//            url: '/Admin/User/TeacherCourseUpdate',
//            type: 'POST',
//            dataType: 'json',
//            data: { teacherCourseCreateDto },
//            success: function (data) {
//                if (data.success) {
//                    var id = $("#UserId").val();
//                    window.location.href = `/Admin/User/Detail?id=${id}&success=true&message=${data.message}`;
//                }
//                else {
//                    error(data.message)
//                }
//            }
//        });
//    }
//}