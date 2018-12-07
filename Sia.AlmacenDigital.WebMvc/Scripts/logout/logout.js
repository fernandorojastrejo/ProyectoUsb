$("#aLogout").on("click", function (e) {
    //prevent Default functionality
    e.preventDefault();
    var tokenForm = $("input[name='__RequestVerificationToken']").val();
    var urlPeticion = mvcUrl + 'Account/LogOff';
    $.ajax({
        url: urlPeticion,
        type: "POST",
        data: { __RequestVerificationToken: tokenForm }

    })
        .done(function (data) {
            if (data == "Ok") {
                //console.log("success " + data);
                borrarLocalStorage();
                window.location.href = mvcUrl;
            }
        });
});

function borrarLocalStorage() {
    //var usuarioId = localStorage.getItem('usuarioId');
    var token = localStorage.getItem('tokenWebApi');

    if (localStorage) {
        localStorage.removeItem('tokenWebApi', token);
        //localStorage.removeItem('usuarioId', usuarioId);
        //console.log("Eliminando STORAGE token: " + localStorage.getItem('tokenWebApi') + " UI: " + localStorage.getItem('usuarioId'));
    }
}