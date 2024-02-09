
$(document).ready(function () {
    TabloGetir();
});
class TeacherCourseCreateDto {
    constructor(TeacherId, CourseId) {
        this.TeacherId = TeacherId;
        this.CourseId = CourseId;
    }
}

class CourseLetterScoreUpdateDto {
    constructor(CourseId, ScoreStart, ScoreEnd, Grade) {
        this.CourseId = CourseId;
        this.ScoreStart = ScoreStart;
        this.ScoreEnd = ScoreEnd;
        this.Grade = Grade;
    }
}
//#region Table 
var CourseDT;
function TabloGetir() {
    CourseDT = $("#CourseTable").DataTable({
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
            url: "/Admin/Course/CourseGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "courseName", render: function (data, filter, row) {
                    return `<a href="/Admin/Course/Detail?id=${row.id}">${data}</a>`
                }
            },
            { data: "courseCredit" },
            { data: "courseAkts" },
            { data: "courseCode" },
            { data: "practicalCourse" },
            { data: "preparationCourse" },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}
//#endregion

//#region Create Course
async function CourseCreate(event) {
    event.preventDefault();
    var form = document.getElementById("course-create-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Course/CourseCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Admin/Course/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region Update Course
async function CourseUpdate(event) {
    event.preventDefault();
    var form = document.getElementById("course-update-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Admin/Course/CourseUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("Id");
                window.location.href = `/Admin/Course/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion
function AddRowTeacherTable(event) {
    event.preventDefault();
    var tablebody = document.getElementById("teacher-course-table-body");
    if (tablebody) {
        var teacherId = $("#TeacherId").val();
        var courseId = $("#Id").val();
        var teacherName = $("#TeacherId option:selected").text();

        var row = document.createElement("tr");
        row.innerHTML = `
        <td hidden><input type="hidden" id="teacher-id" value="${teacherId}" /></td>
        <td hidden><input type="hidden" id="course-id" value="${courseId}" /></td>
        <td>${teacherName}<input type="hidden" id="teacher-name" value="${teacherName}" /></td>
        <th style="text-align:center;"><i onclick="DeleteRow(this)" class="fa fa-trash" aria-hidden="true"></i></th>`;
        tablebody.appendChild(row);
    }
}
function TeacherCoursesTableGetData() {
    // Bu fonksiyon, tablodaki satırları okuyup her bir satırı TeacherCourseCreateDto'ya çevirir.
    var tableBody = document.getElementById("teacher-course-table-body");
    var rows = tableBody.getElementsByTagName("tr");
    var dataObjects = [];

    var courseId = $("#Id").val();
    var courseData = new TeacherCourseCreateDto(null, courseId);
    dataObjects.push(courseData)

    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];

        // Satırdaki input elemanlarını seç
        var inputs = row.getElementsByTagName("input");

        // Input değerlerini al
        var teacherId = inputs[0].value;
        var courseId = inputs[1].value;

        // Yeni TeacherCourseCreateDto nesnesi oluştur ve diziye ekle
        var dto = new TeacherCourseCreateDto(teacherId, courseId);
        dataObjects.push(dto);
    }
    return dataObjects;
}
async function TeacherCoursesTableUpdate(event) {
    event.preventDefault();
    var teacherCourseCreateDto = TeacherCoursesTableGetData();
    if (teacherCourseCreateDto != null && teacherCourseCreateDto.length > 0) {
        await $.ajax({
            url: '/Admin/Course/TeacherCourseUpdate',
            type: 'POST',
            dataType: 'json',
            data: { teacherCourseCreateDto },
            success: function (data) {
                if (data.success) {
                    var id = $("#Id").val();
                    window.location.href = `/Admin/Course/Detail?id=${id}&success=true&message=${data.message}`;
                }
                else {
                    error(data.message)
                }
            }
        });
    }
}

async function CourseLetterScoreUpdate(event, input, courseId, letterGradeId) {
    event.preventDefault();
    var formData = new FormData();
    formData.append("CourseGrade", input.value);
    formData.append("CourseId", courseId);
    formData.append("LetterGradeId", letterGradeId);

    await $.ajax({
        url: '/Admin/Course/CourseLetterScoreUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                success(data.message);
            }
            else {
                error(data.message)
            }
        }
    });
}

function DeleteRow(btn) {
    var row = btn.parentNode.parentNode;
    row.parentNode.removeChild(row);
}

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
                    url: '/Admin/Course/CourseDelete',
                    type: 'POST',
                    dataType: 'json',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        if (data.success) {
                            window.location.href = `/Admin/Course/Index?&success=true&message=${data.message}`;
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