function addNums(){
    var firstNum = document.getElementById("firstNumInput").value;
    var secondNum = document.getElementById("secondNumInput").value;

    var result = parseInt(firstNum) + parseInt(secondNum);

    document.getElementById("showAnswer").style.display = "block";
    document.getElementById("result").innerText = result;

}