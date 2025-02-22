$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "timeOut": "5000"
    };
    window.exibirAlerta = function (tipo, mensagem, titulo) {
        toastr[tipo](mensagem, titulo);
    };
});
