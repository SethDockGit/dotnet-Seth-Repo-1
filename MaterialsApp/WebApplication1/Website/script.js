
var api = `https://localhost:44366`;

var username = "";

function checkResources(){

    var username = document.getElementById("username").value;

    fetch(`${api}/materials/${username}`)
        .then((response) => response.json())
        .then((data) => {
            console.log(data);

            document.getElementById('yourResources').innerText = '';

            if(data.success === false){

                document.getElementById('yourResources').innerText += data.message;
            }
            else{

                var user = data.user;
                document.getElementById('yourResources').innerText += `User: ${user.username} WoodCount: ${user.woodCount} StoneCount: ${user.stoneCount} GoldCount: ${user.goldCount} IronCount: ${user.ironCount}`
            }  
        });

        document.getElementById("depositOrWithdraw").style.display = "block";
}

var resourceSelection = "";

function selectWood(){
    resourceSelection = "wood";
    document.getElementById("enterAmount").style.display = "block";

    if(selectorID == 1){
        document.getElementById("deposit").style.display = "block";
    }
    if(selectorID == 2){
        document.getElementById("withdraw").style.display = "block";
    }
}
function selectStone(){
    resourceSelection = "stone";
    document.getElementById("enterAmount").style.display = "block";

    if(selectorID == 1){
        document.getElementById("deposit").style.display = "block";
    }
    if(selectorID == 2){
        document.getElementById("withdraw").style.display = "block";
    }
}
function selectGold(){
    resourceSelection = "gold";
    document.getElementById("enterAmount").style.display = "block";

    if(selectorID == 1){
        document.getElementById("deposit").style.display = "block";
    }
    if(selectorID == 2){
        document.getElementById("withdraw").style.display = "block";
    }
}
function selectIron(){
    resourceSelection = "iron";
    document.getElementById("enterAmount").style.display = "block";

    if(selectorID == 1){
        document.getElementById("deposit").style.display = "block";
    }
    if(selectorID == 2){
        document.getElementById("withdraw").style.display = "block";
    }
}

var selectorID = 0;

function selectDeposit(){
    document.getElementById("selectResource").style.display = "block";
    selectorID = 1;
}

function selectWithdraw(){
    document.getElementById("selectResource").style.display = "block";
    selectorID = 2;
}

function depositResource(){

    var amount = document.getElementById("enterAmount").value;

    var obj = {
        ResourceType: resourceSelection
    }

    fetch(`${api}/materials/${username}/${amount}`, {
        method: 'POST',
        body: JSON.stringify(obj),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
    });

}