function askNumber(){
    var a = prompt("gimme a number");
    var b = prompt("gimme a number");

    var c = parseInt(a) + parseInt(b);

    alert("your number is" + c);
}

function greetUser(){
    var name = 
        document.getElementById("userNameInput").value;

    document.getElementById("greeting").style.display = "block";
    document.getElementById("userName").innerText = name;
}

