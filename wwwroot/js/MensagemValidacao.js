function Mensagem(Mensagem, Tipo)
{
    if (Tipo == "sucesso") {
        var CorBackground = "linear-gradient(to right, #00b09b, #96c93d)";
    } else if (Tipo == "erro") {
        var CorBackground = "linear-gradient(to right, #DC143C, #800000)";
    }

    Toastify({
        text: Mensagem,
        duration: 3000,
        newWindow: true,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "right", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
            background: CorBackground,
        },
        onClick: function () { } // Callback after click
    }).showToast();
}