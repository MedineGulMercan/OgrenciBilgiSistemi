async function TeacherLogin(event) {
    event.preventDefault();
    var form = document.getElementById("teacher-login-form"); // Formu burada yakala
    var formData = new FormData(form); // formdata formun içindeki input vb nesnelerin namelerini alıp controllera gönderiyor.
    await $.ajax({
        url: '/Login/SignIn',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.authorization) {
                if (data.type == "Ogrenci") {
                    window.location.href = "/Student/Home/Index";
                }
                else if (data.type == "Ogretmen") {
                    window.location.href = "/Teacher/Home/Index";
                }
                else {
                    window.location.href = "/Admin/Home/Index";
                }
            }
            else {
                error("Kullanıcı bulunamadı.")
            }
        },
    });
}
async function StudentLogin(event) {
    event.preventDefault();
    var form = document.getElementById("student-login-form"); // Formu burada yakala
    var formData = new FormData(form);

    console.log(formData.get("Type"))
    console.log(formData.get("TC"))
    await $.ajax({
        url: '/Login/SignIn',
        type: 'POST',
        dataType: 'json',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data) {
                window.location.href = "/Student/Home/Index";
            }
            else {
                error("Kullanıcı bulunamadı.")
            }
        },
    });
}
