function AddRowTable(event) {
    event.preventDefault();
    //her row eklendiğinde kredileri topluyorum, 45 den büyük olunca hata veriyorum 
    var selectElement = document.getElementById("TeacherandCourses");
    var selectedOption = selectElement.options[selectElement.selectedIndex];
    var creditValue = selectedOption.getAttribute("data-credit");

    var credit = document.getElementById("harcanan-kredi");
    var creditCount = parseInt(credit.innerHTML);
    var count = creditCount + parseInt(creditValue);

    if (count > 45) {
        error("Seçilen ders kredisi 45den büyük olamaz!")
        return null;
    }
    credit.innerHTML = count

    var tablebody = document.getElementById("CourseSelectionTableBody");
    if (tablebody) {
        /*var studentId = $("#Id").val();*/
        var courseId = $("#TeacherandCourses").val();
        var courseAndTeacherName = $("#TeacherandCourses option:selected").text();

        var row = document.createElement("tr");
        row.innerHTML = `
        <td>${courseAndTeacherName}<input type="hidden" id="class-id" value="${courseId}" /></td>
        <td hidden>${parseInt(creditValue)}<input type="hidden" class="course-credit" value="${parseInt(creditValue)}" </td>
        <td style="text-align:center;"><i onclick="DeleteRow(this)" class="fa fa-trash" aria-hidden="true"></i></td>`;
        tablebody.appendChild(row);
    }
}
function DeleteRow(btn) {
    var row = btn.parentNode.parentNode;
    var credit = document.getElementById("harcanan-kredi");

    var courseCreditValue = row.querySelector('input.course-credit').value;

    console.log(courseCreditValue)

    var creditCount = parseInt(credit.innerHTML) - parseInt(courseCreditValue)
    credit.innerHTML = creditCount;
    row.parentNode.removeChild(row);
}

async function CourseSelectionCreate() {
    var tableBody = document.getElementById('CourseSelectionTableBody');
    var rows = tableBody.getElementsByTagName('tr');
    var courseSelectionsCreateDto = [];
    for (var i = 0; i < rows.length; i++) {
        var cells = rows[i].querySelectorAll('td');
        var newData = {
            SemesterCoursesId: cells[0].querySelector('input').value,
        }
        courseSelectionsCreateDto.push(newData);
    }
    var submitButton = document.getElementById('onayButton');
    await $.ajax({
        url: '/Student/CourseSelection/CourseSelectionCreate',
        type: 'POST',
        data: { courseSelectionsCreateDto },
        success: function (data) {
            if (data.success) {
                submitButton.disabled = true;
                success("Ders Kaydınız Onaya Gönderilmiştir. ");
                //var id = $("#Id").val();
                //window.location.href = `/Admin/Department/Detail?id=${id}&success=true&message=${data.message}`;
            }
            else {
                error(data.message)
            }
        }
    });
}

    
