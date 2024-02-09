
$(document).ready(function () {
    TabloGetir();
});

//#region Table 
var CourseDT;
function TabloGetir() {
    CourseDT = $("#CourseAssessmentTable").DataTable({
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
            url: "/Teacher/CourseAssessment/CourseAssessmentGetAll",
            type: 'POST',
            datatype: "json",
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "assessmentName", render: function (data, filter, row) {
                    console.log(row)
                    return `<a href="/Teacher/CourseAssessment/Detail?id=${row.id}">${data}</a>`
                }
            },

            { data: "passingScore" },
            { data: "courseName" },
            { data: "assessmentTypeName" },
            { data: "impactCourseGrade" },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
    });
}
//#endregion

//#region Create AssessmentType
async function CourseAssessmentCreate(event) {
    event.preventDefault();
    var form = document.getElementById("courseAssessment-create-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Teacher/CourseAssessment/CourseAssessmentCreate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                window.location.href = "/Teacher/CourseAssessment/Index?success=true&message=" + data.message;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion

//#region Update CourseAssessment
async function CourseAssessmentUpdate(event) {
    event.preventDefault();
    debugger
    var form = document.getElementById("courseAssessment-update-form");
    var formData = new FormData(form);
    await $.ajax({
        url: '/Teacher/CourseAssessment/CourseAssessmentUpdate',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                var id = formData.get("Id");
                window.location.href = `/Teacher/CourseAssessment/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}
//#endregion