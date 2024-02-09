var courseId = $('#CourseId').val();
var currentSemesterId = $('#AssessmentYear').val();
$(document).ready(function () {
    TabloGetir();
});

async function AddExamGrade(courseAssessmentId, studentId, input) {
    var formData = new FormData();
    formData.append("CourseAssessmentId", courseAssessmentId)
    formData.append("StudentId", studentId)
    formData.append("Score", input.value)
    await $.ajax({
        url: '/Teacher/ExamGrade/AddExamGrade',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                success(data.message)
            }
            else {
                error(data.message)
            }
        }
    });
}

//#region Table 
var examGradeDT;
function TabloGetir() {
    examGradeDT = $("#ExamGradeTable").DataTable({
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
        reload: true,
        order: [[0, "asc"]],
        lengthMenu: [[10, 20, 50], ['10', '20', '50']],
        ajax: {
            url: "/Teacher/ExamGrade/GetAllStudentCourse",
            type: 'POST',
            datatype: "json",
            data: function (d) {
                d.currentSemesterId = currentSemesterId;
                d.courseId = courseId;
            },
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "studentName",
            },
            {
                data: "assessmentTypeName",
            },
            {
                data: "score", render: function (data, filter, row) {
                    return `<input type="text" value="${row.score}" class="form-control" onblur="AddExamGrade('${row.courseAssessmentId}','${row.studentId}', this)"/>`;
                },
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
        initComplete: function () {
            $('#CourseId').on('change', async function () {
                courseId = $(this).val();
                examGradeDT.ajax.reload(null, true);
            });
            $('#AssessmentYear').on('change', async function () {
                currentSemesterId = $(this).val();
                examGradeDT.ajax.reload(null, true);
            });
        },
    });
}
//#endregion