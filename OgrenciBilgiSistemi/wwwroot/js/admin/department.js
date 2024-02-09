$(document).ready(function () {
    TabloGetir();
    GetTeachers();
});

class SemesterCoursesTableDto {
    constructor(DepartmentId, ClassId, SemesterId, CourseId,TeacherId) {
        this.DepartmentId = DepartmentId;
        this.ClassId = ClassId;
        this.SemesterId = SemesterId;
        this.CourseId = CourseId;
        this.TeacherId = TeacherId;
    }
}

//#region Table 
var CourseDT;
function TabloGetir() {
    CourseDT = $("#DepartmentTable").DataTable({
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
            url: "/Admin/Department/DepartmentGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "departmentName", render: function (data, filter, row) {
                    return `<a href="/Admin/Department/Detail?id=${row.id}">${data}</a>`
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}
//#endregion

//#region Create Department
async function DepartmentCreate(event) {
    event.preventDefault();
    var form = document.getElementById("department-create-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Department/DepartmentCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Admin/Department/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region Update Department
async function DepartmentUpdate(event) {
    event.preventDefault();
    var form = document.getElementById("department-update-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Department/DepartmentUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("Id");
                window.location.href = `/Admin/Department/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

function AddRowTable(event) {
    event.preventDefault();

    var tablebody = document.getElementById("SemesterCoursesTableBody");
    if (tablebody) {
        var departmentId = $("#Id").val();
        var classId = $("#ClassId").val();
        var className = $("#ClassId option:selected").text();
        var semesterId = $("#SemesterId").val();
        var semesterName = $("#SemesterId option:selected").text();
        var courseId = $("#CourseId").val();
        var courseName = $("#CourseId option:selected").text();
        var teacherId = $("#TeacherSelect").val();
        var teacherName = $("#TeacherSelect option:selected").text();

        var row = document.createElement("tr");
        row.innerHTML = `
        <td hidden><input type="hidden" id="department-id" value="${departmentId}" /></td>
        <td>${className}<input type="hidden" id="class-id" value="${classId}" /></td>
        <td>${semesterName}<input type="hidden" id="semester-id" value="${semesterId}" /></td>
        <td>${courseName}<input type="hidden" id="course-id" value="${courseId}" /></td>
        <td>${teacherName}<input type="hidden" id="teacher-id" value="${teacherId}" /></td>
        <th style="text-align:center;"><i onclick="DeleteRow(this)" class="fa fa-trash" aria-hidden="true"></i></th>`;

        tablebody.appendChild(row);
    }
}
function DeleteRow(btn) {
    var row = btn.parentNode.parentNode;
    row.parentNode.removeChild(row);
}
function SemesterCoursesTableGetData() {
    // Bu fonksiyon, tablodaki satırları okuyup her bir satırı SemesterCoursesTableDto'ya çevirir.
    var tableBody = document.getElementById("SemesterCoursesTableBody");
    var rows = tableBody.getElementsByTagName("tr");
    var dataObjects = [];
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];

        // Satırdaki input elemanlarını seç
        var inputs = row.getElementsByTagName("input");

        // Input değerlerini al
        var departmentId = inputs[0].value;
        var classId = inputs[1].value;
        var semesterId = inputs[2].value;
        var courseId = inputs[3].value;
        var teacherId = inputs[4].value;
        console.log(teacherId);

        // Yeni SemesterCoursesTableDto nesnesi oluştur ve diziye ekle
        var dto = new SemesterCoursesTableDto(departmentId, classId, semesterId, courseId, teacherId);
        dataObjects.push(dto);
    }
    return dataObjects;
}
async function SemesterCoursesTableUpdate(event) {
    event.preventDefault();
    var semesterCoursesUpdateDto = SemesterCoursesTableGetData();
    if (semesterCoursesUpdateDto != null && semesterCoursesUpdateDto.length > 0) {
        console.log(semesterCoursesUpdateDto)
        await $.ajax({
            url: '/Admin/Department/SemesterCoursesTableUpdate',
            type: 'POST',
            dataType: 'json',
            data: { semesterCoursesUpdateDto },
            success: function (data) {
                if (data.success) {
                    var id = $("#Id").val();
                    window.location.href = `/Admin/Department/Detail?id=${id}&success=true&message=${data.message}`;
                }
                else {
                    error(data.message)
                }
            }
        });
    }
}

async function GetTeachers() {
    var id = document.getElementById("CourseId").value;
    // Ders ID'sine göre öğretmenleri getir
   await $.ajax({
        type: 'POST',
        url: '/Admin/Department/GetTeachersByCourse',
        data: { id: id }, //bak burda {id: id } kendi objeni gönderiyorsunya o zaman bu bir json değil formdata olmadığı içinde bunlarda gider şimdi dene
       success: function (result) {
           if (result.success = true) {
               var teachers = result.teachers;

               // Öğretmenleri select'e ekle
               var teacherSelect = $('#TeacherSelect');
               teacherSelect.empty();  //Önceki öğretmenleri temizles

               $.each(teachers, function (index, teacher) {
                   teacherSelect.append($('<option>', {
                       value: teacher.id,
                       text: teacher.name + ' '+ teacher.surname,
                   }));
               });
           } else {
               console.log(result.Message);
           }
       },
       error: function (error) {
           console.log(error);
       }
    });

}