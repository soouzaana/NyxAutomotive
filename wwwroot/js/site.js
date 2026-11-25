(function () {
    var botaoMenu = document.getElementById("botaoMenu");
    var menu = document.getElementById("menu");
    if (botaoMenu) {
        botaoMenu.addEventListener("click", function () {
            menu.classList.toggle("aberto");
        });
    }

    var formularios = document.querySelectorAll("form[data-confirmar]");
    for (var i = 0; i < formularios.length; i++) {
        formularios[i].addEventListener("submit", function (e) {
            if (!confirm(this.getAttribute("data-confirmar"))) {
                e.preventDefault();
            }
        });
    }

    var form = document.getElementById("formReserva");
    if (form) {
        var veiculo = document.getElementById("veiculoId");
        var retirada = document.getElementById("retirada");
        var devolucao = document.getElementById("devolucao");
        var resumo = document.getElementById("resumo");

        function moeda(v) {
            return "R$ " + v.toFixed(2).replace(".", ",").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }

        function atualizar() {
            var opcao = veiculo.options[veiculo.selectedIndex];
            var diaria = opcao ? parseFloat(opcao.getAttribute("data-diaria")) : NaN;
            devolucao.min = retirada.value;

            if (isNaN(diaria) || !retirada.value || !devolucao.value) {
                resumo.textContent = "Selecione o veículo e as datas para ver o valor total.";
                return;
            }

            var dias = Math.round((new Date(devolucao.value) - new Date(retirada.value)) / 86400000);
            if (dias < 0) {
                resumo.textContent = "A data de devolução não pode ser anterior à retirada.";
                return;
            }
            if (dias < 1) { dias = 1; }

            resumo.innerHTML = dias + " diária(s) × " + moeda(diaria) + " = <strong>" + moeda(dias * diaria) + "</strong>";
        }

        veiculo.addEventListener("change", atualizar);
        retirada.addEventListener("change", atualizar);
        devolucao.addEventListener("change", atualizar);
        atualizar();
    }
})();
