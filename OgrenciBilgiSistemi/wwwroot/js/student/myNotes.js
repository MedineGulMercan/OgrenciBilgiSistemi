var selectElement = document.getElementById("semesterId");
var selectedOption = selectElement.options[selectElement.selectedIndex];
var classId = selectedOption.getAttribute("data-credit");
var semesterId = selectElement.value;
$(document).ready(function () {
    TabloGetir();
});

//#region Table 
var myNotesTable;
function TabloGetir() {
    myNotesTable = $("#MyNotes").DataTable({
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
            url: "/Student/MyNotes/GetAllMyNotes",
            type: 'POST',
            datatype: "json",
            data: function (d) {
                d.classId = classId;
                d.semesterId = semesterId;
            },
            dataSrc: function (data) {
                return data;
            },
        },
        columns: [
            {
                data: "courseName",
            },
            {
                data: "courseNotesString",
                render: function (data, type, row) {
                    if (data != null) {
                        var tr = '<ul>'
                        for (var i = 0; i < data.length; i++) {
                            tr += `<li>${data[i]}</li>`
                        }
                        tr = tr + '</ul>';
                        return tr;
                    }
                    else {
                        return "";
                    }
                }
            },
            {
                data: "courseAvarage",
                render: function (data, type, row) {
                    var sayi = parseFloat(data);
                    var formattedSayi = sayi.toFixed(2);
                    return formattedSayi;
                }
            },
            {
                data: "letterNote",
            },
            {
                data: "gectiMi",
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
        initComplete: function () {
            $('#semesterId').on('change', async function () {
                var selectElement = document.getElementById("semesterId");
                var selectedOption = selectElement.options[selectElement.selectedIndex];
                classId = selectedOption.getAttribute("data-credit");
                semesterId = selectElement.value;
                myNotesTable.ajax.reload(null, true);
            });
        },
        createdRow : function (row, data, dataIndex) {
            // Her bir tr için style ekleyebilirsiniz
            $(row).find('td').css('vertical-align', 'middle');
            $(row).find('td').css('text-align', 'center');
        }
    });
}