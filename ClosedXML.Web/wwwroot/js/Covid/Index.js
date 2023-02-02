var $tblCovid;

$(document).ready(function () {
    pageLoad();
    tblCovidLoad();

    $('#covidDataTable tbody').on('click', 'tr', function () {
        $(this).addClass('bigger-font-size').siblings().removeClass('bigger-font-size');
    }).on('dblclick', 'tr', function () {
        $(this).removeClass('bigger-font-size');
    });
});

function pageLoad() {
    $('#reload').click(function () {
        $('#covidDataTable').DataTable().ajax.reload();
    });
    $('#btnSearch').click(function () {
        $('#covidDataTable').DataTable().destroy();
        tblCovidLoad();
    });

};

function tblCovidLoad() {
    $tblCovid = $('#covidDataTable').DataTable({
        "language": {
            "url": "/lib/datatables/tr.json"
        },
        "scrollY": 400,
        "scrollX": true,
        "proccessing": true,
        "serverSide": true,
        "searchDelay": 600,
        "searching": false,
        "pagingType": "full_numbers",
        "ordering": true,
        "paging": true,
        "pageLength": 25,
        "responsive": true,
        "ajax": {
            url: "/covid/getCovidPaging",
            type: 'POST',
            data: {
                covidDate: $("#CovidDate").val(),
                city: $("#City").val(),
                count: $("#Count").val()
            }
        },
        "columnDefs": [
            { "name": "Id", "data": "id", "targets": 0, "visible": true, "autoWidth": true, "searchable": true },
            { "name": "City", "data": "city", "targets": 1, "visible": true, "autoWidth": true, "searchable": true },
            { "name": "CovidDate", "data": "covidDate", "targets": 2, "visible": true, "autoWidth": true, "searchable": true },
            { "name": "Count", "data": "count", "targets": 3, "visible": true, "autoWidth": true, "searchable": true },          
        ],
        "order": [[0, "desc"]]
    });
};

$("#btnExcelExport").click(function () {
    saveExcel("covidDataTable", "/covid/getCovidListExcelAsync", {
        covidDate: $("#CovidDate").val(),
        city: $("#City").val(),
        count: $("#Count").val()
    })
});

