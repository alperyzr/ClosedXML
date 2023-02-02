function s2ab(s) {
    debugger;
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
    return buf;
}

function saveFile(data) {
    debugger;
    var blob = new Blob([s2ab(atob(data.base64_file))], {
        type: data.content_type
    });
    href = URL.createObjectURL(blob);
    var a = document.createElement("a");
    a.href = URL.createObjectURL(blob);
    a.download = data.file_name;
    a.click();
}

function saveExcel(tableName, url, postParams) {  
    var table = $('#' + tableName).DataTable();
    var settings = $('#' + tableName).dataTable().fnSettings();

    var col = new Array();
    for (var index in settings.aoColumns) {
        var data = settings.aoColumns[index];
        var column = { "name": data.sName };
        col.push(column);
    }
    var ord = {
        "column": settings.aLastSort[0].col,
        "dir": settings.aLastSort[0].dir
    };
    var orders = [];
    orders.push(ord);

    postParams.order = orders;
    postParams.columns = col;
    postParams.length = 1000;

    $.ajax({
        url: url,
        type: 'POST',
        data: postParams,
        success: function (data) {
            debugger;
        },
        error: function (data) {
            //$(".loader-wrapper").css("display", "none");
            //if (data.responseText == "IsAjaxPost") {
            //    Swal.fire({
            //        icon: 'error',
            //        title: 'ÜZGÜNÜZ !',
            //        html: 'Bu İşlem İçin Yetkiniz  Bulunmamaktadır',
            //        showCloseButton: true,
            //    })
            //}
        },
    }).done(function (data) {
        saveFile(data);
    });
}