

let arr = []

function addNum(){

    var number = parseInt(document.getElementById("userNum").value);

    arr.push(number);

    document.getElementById("showNums").innerHTML += number + "<br/>";
}

function snortNums(){

    arr.sort();

    for(let i = 0; i < arr.length; i++){
    document.getElementById("showSorted").innerHTML += arr[i] +"<br/>";

    }

}

function sortNums(){

    let sorted = [arr.length]

    let lowest = arr[0];

    for(let j = 0; j < arr.length; j++){

        for(let i = 0; i < arr.length; i++){

            if(i = 0){
                if(arr[i] < lowest){
                    lowest = arr[i];
                }

                sorted.push(lowest);
            }

            if(arr[i] > lowest){
                let next = arr[i];

                if(arr[i] < next){
                    next = arr[i];
                }

                sorted.push(next);

            }

        }

    }

    for (let i = o; i < sorted.length; i++){
        document.getElementById("showNums").innerText += sorted[i];
    }


}