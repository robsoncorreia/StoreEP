document.getElementById("btn_criar_nova_categoria").addEventListener("click", adicionar);
var txt_nova_categoria = document.getElementById("txt_nova_categoria");
var txt_novo_fabricante = document.getElementById("txt_novo_fabricante");
var produto_categoria = document.getElementById("Produto_Categoria");
var btn_criar_novo_fabricante = document.getElementById("btn_criar_novo_fabricante").addEventListener("click", adicionar)
var produto_fabricante = document.getElementById("Produto_Fabricante");

function adicionar() {
    var btn = this.id === "btn_criar_nova_categoria";
    if (btn) {
        if (txt_nova_categoria.value == "") {
            return;
        }
        var option = document.createElement("option");
        option.textContent = txt_nova_categoria.value;
        produto_categoria.appendChild(option);
        txt_nova_categoria.value = "";
    } else {
        if (txt_novo_fabricante.value == "") {
            return;
        }
        var option = document.createElement("option");
        option.textContent = txt_novo_fabricante.value;
        produto_fabricante.appendChild(option);
        txt_novo_fabricante.value = "";
    }
}