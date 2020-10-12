$(function () {
    var mes = $('#custId').val();
    if (mes == "1") {
        $.notify("You have successfully added", "success");
    }
    if (mes == "2") {
        $.notify("You have successfully updated", "success");
    }
    if (mes == "3") {
        $.notify("You have successfully deleted", "success");
    }
 
});