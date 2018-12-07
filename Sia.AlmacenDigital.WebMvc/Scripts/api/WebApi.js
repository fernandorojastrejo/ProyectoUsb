(function (webApiCliente, $, undefined) {
    webApiCliente.nombreToken = 'tokenWebApi';
    webApiCliente.token = localStorage.getItem(webApiCliente.nombreToken);
    webApiCliente.headers = {};
    if (webApiCliente.token) {
        webApiCliente.headers.Authorization = 'Bearer ' + webApiCliente.token;
    }

}(window.webApiCliente = window.webApiCliente || {}, jQuery));