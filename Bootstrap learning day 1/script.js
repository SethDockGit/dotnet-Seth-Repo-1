
var isCol1 = true;

for (let y = 0; y <= 7; y++) {


    document.getElementById("chessboard").innerHTML += `<div class="row" id="${y}"></div>`;

    for (let x = 0; x <= 7; x++) {

        if (isCol1) {

            var col = '<div class="col" style="background-color:lavender; height:130px;"></div>';

            document.getElementById(`${y}`).innerHTML += `${col}`;
            isCol1 = false;
        }

        else {

            var col = '<div class="col" style="background-color:black; height:130px;"></div>';

            document.getElementById(`${y}`).innerHTML += `${col}`;
            isCol1 = true;
        }
    }

    if (y % 2 === 0) {
        isCol1 = false;
    }
    if (y % 2 !== 0) {
        isCol1 = true;
    }
}