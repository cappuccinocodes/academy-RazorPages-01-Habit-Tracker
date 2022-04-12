function calculate() {
    var tbl = document.getElementById("records");
    var result = 0;

    for (var i = 1; i < tbl.rows.length; i++) {
        //result = result + row.cells[1].innerHTML;
        result = result + +tbl.rows[i].cells[1].innerHTML;
    }
    var resultArea = document.getElementById("result");
    resultArea.append(`${result}`)
}