let currentMoney = 0.00;

function addDollar(){

    currentMoney += 1.00;
    displayBalance();
}

function addQuarter(){

    currentMoney += .25;
    displayBalance();
}

function addDime(){

    currentMoney += .10;
    displayBalance();
}

function addNickel(){

    currentMoney += .05;
    displayBalance();
}

function displayBalance(){
    document.getElementById("currentBalance").innerText = currentMoney.toFixed(2);
}

displayBalance();

let inventory = [
    {
        "id": 0,
        "name": "Hungerbuster",
        "quantity": 9,
        "price" : 1.00
    },
    {
        "id": 1,
        "name": "Bag o' Chips",
        "quantity": 3,
        "price": 1.50
    },
    {
        "id": 2,
        "name": "Dorringos",
        "quantity": 7,
        "price": 0.75
    },
    {
        "id": 3,
        "name": "Salted Pork",
        "quantity": 11,
        "price": 0.60
    },
    {
        "id": 4,
        "name": "Egg",
        "quantity": 0,
        "price": 2.50
    },
    {
        "id": 5,
        "name": "Toaster Pie",
        "quantity": 8,
        "price": 3.25
    },
    {
        "id": 6,
        "name": "Hot Rocket",
        "quantity": 4,
        "price": 1.25
    },
    {
        "id": 7,
        "name": "Yum-bits",
        "quantity": 1,
        "price": 1.35
    },
    {
        "id": 8,
        "name": "Dinner-in-a-can",
        "quantity": 8,
        "price": 5.25
    }
];


var row = `<div class="row" id="1"></div>`;

document.getElementById("items").innerHTML += `${row}`;

for (let i = 0; i < inventory.length; i++) {

    var message = `${inventory[i].price}${inventory[i].name}`

    var button = `<button type="button" class="btn btn-secondary btn-lg" style="width: 150px" onclick="loadVend(${inventory[i].id}, ${message})"><u>${inventory[i].name}</u>
    </br>$${inventory[i].price.toFixed(2)}</br></br>(Quantity: ${inventory[i].quantity})</button>`;

    var col = `<div class="col-sm-4" style="padding: 20px;">${button}</div>`;
        
    document.getElementById("1").innerHTML += `${col}`;

}

let buy = false;

function buy(buy){
    buy = true;
}

function loadVend(id, message){

    displayMessage(message)

    if (buy == true){
        requestVend(id);
    }
}

function displayMessage(message){

    document.getElementById("MessagePort").innerText = message;
}


function requestVend(id){
    var item = inventory[id];
    
    var message = "";

    if(item === undefined){
        message = `DEV ERROR: Item of id ${id} not found.`
    }
    else if(item.quantity === 0){
        message = `Sorry, ${item.name} is out of stock.`
    }
    else if(currentMoney < item.price){
        message = `${item.price}$ is needed for this item, please enter more money.`;
    }
    else{
        item.quantity--;
        currentMoney -= item.price;
        displayBalance();

        message = `Vended a ${item.name}, thank you!`;
    }

    displayMessage(message);
}