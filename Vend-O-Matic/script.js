let currentMoney = 0.00;

function addDollar(){

    currentMoney += 1.00;
}

function addQuarter(){

    currentMoney += .25;
}

function addDime(){

    currentMoney += .10;
}

function addNickel(){

    currentMoney += .05;
}

function displayBalance(){
    document.getElementById("currentBalance").innerText = currentMoney;
}

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
        "name": "Dinnerinacan",
        "quantity": 8,
        "price": 5.25
    }
];




for (let i = 0; i < inventory.length; i++) {

    if (i % 3 == 0 || i == 0){
        document.getElementById("items").innerHTML += `<div class="row" id="${i}"></div>`;
    }

    var button = `<button type="button" class="btn btn-secondary btn-lg"><u>${inventory[i].name}</u>
    </br>$${inventory[i].price}</br></br>(Quantity: ${inventory[i].quantity})</button>`;

    var col = `<div class="col" style="background-color:lavender; height:130px;">${button}</div>`;
        
    document.getElementById(`${i}`).innerHTML += `${col}`;

}



document.getElementById("1A").innerHTML += `<u>${inventory[0].name}</u>
</br>$${inventory[0].price}</br></br>(Quantity: ${inventory[0].quantity})`
 
document.getElementById("1B").innerHTML += `<u>${inventory[1].name}</u>
</br>$${inventory[1].price}</br></br>(Quantity: ${inventory[1].quantity})`

document.getElementById("1C").innerHTML += `<u>${inventory[2].name}</u>
</br>$${inventory[2].price}</br></br>(Quantity: ${inventory[2].quantity})`

document.getElementById("2A").innerHTML += `<u>${inventory[3].name}</u>
</br>$${inventory[3].price}</br></br>(Quantity: ${inventory[3].quantity})`

document.getElementById("2B").innerHTML += `<u>${inventory[4].name}</u>
</br>$${inventory[4].price}</br></br>(Quantity: ${inventory[4].quantity})`

document.getElementById("2C").innerHTML += `<u>${inventory[5].name}</u>
</br>$${inventory[5].price}</br></br>(Quantity: ${inventory[5].quantity})`

document.getElementById("3A").innerHTML += `<u>${inventory[6].name}</u>
</br>$${inventory[6].price}</br></br>(Quantity: ${inventory[6].quantity})`

document.getElementById("3B").innerHTML += `<u>${inventory[7].name}</u>
</br>$${inventory[7].price}</br></br>(Quantity: ${inventory[7].quantity})`

document.getElementById("3C").innerHTML += `<u>${inventory[8].name}</u>
</br>$${inventory[8].price}</br></br>(Quantity: ${inventory[8].quantity})`

function populateItems(){
    //TODO: implement code to populate the page with the items in the inventory
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

        message = `Vended a ${item.name}, thank you!`;
    }
}