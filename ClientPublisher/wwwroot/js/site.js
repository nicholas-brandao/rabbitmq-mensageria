$(document).ready(function () {

    // Envia os dados para Api apenas na primeira vez que carrega o sistema
    if (sessionStorage["primeiraVez"] == undefined) {

        sessionStorage["primeiraVez"] = true;

        var Paginas = CarregarParametrosPaginas();

        $.ajax({
            type: "POST",
            url: "http://localhost:54866/api/rabbitmqpublisher",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(Paginas),
            success: function (data) {


                $(".modal-title").html("Sucesso");
                $(".modal-msg").html(data);

                $("#modalDialog").modal('show');
            },
            error: function (data) {
                $(".modal-title").html("Erro");
                $(".modal-msg").html(data);

            }

        });
    } else {
        $(".modal-title").html("Aviso");
        $(".modal-msg").html("O sistema já enviou as informações da página para a Api");

        $("#modalDialog").modal('show');
    }

});

function RetornaIp() {
    return $.ajax({
        type: 'GET',
        url: "https://api.ipify.org?format=text",
        global: false,
        async: false,
        success: function (data) {
            return data;
        }
    }).responseText;
}

function CarregarParametrosPaginas() {

    var pagina = [CarregaPagina("Index"), CarregaPagina("Landing"), CarregaPagina("CheckoutPedido"), CarregaPagina("ConfirmacaoPedido")];

    return pagina;
}

function CarregaPagina(nomePagina) {

    var pagina = null;
    var parametros = "";
    $.ajax({
        type: 'GET',
        async: false,
        url: ("/" + nomePagina),
    }).done(function (data) {

        switch (nomePagina) {

            case ("Landing"):
                parametros = [{
                    'Nome': "Nome",
                    'Valor': $(data).find('#Nome').val()
                },
                {
                    'Nome': "Email",
                    'Valor': $(data).find('#Email').val()
                }];
                break;
            case ("CheckoutPedido"):
                parametros = [{
                    'Nome': "NumeroPedido",
                    'Valor': $(data).find('#NumeroPedido').html()
                },
                {
                    'Nome': "Status",
                    'Valor': $(data).find('#Status').html()
                }];
                break;
            case ("ConfirmacaoPedido"):
                parametros = [{
                    'Nome': "FormaPagamento",
                    'Valor': $(data).find('#FormaPagamento').val()
                },
                {
                    'Nome': "Total",
                    'Valor': $(data).find('#Total').val()
                }];
                break;

            default:
        }
        pagina = {
            "Ip": RetornaIp(),
            "NomePagina": nomePagina,
            "Browser": navigator.userAgent,
            "Parametros": parametros

        };

    });

    return pagina;

}