
var api = `https://localhost:44345`;

function GetStudents(){
    
    fetch('https://localhost:44345/students')
        .then((response) => response.json())
        .then((data) => {
            document.getElementById('students').innerHTML = "";

            for(let i = 0; i < data.length; i++){

                document.getElementById("students").innerHTML += `<li>ID: ${data[i].id} Name: ${data[i].name} ||  Age: ${data[i].age}</li>`;
            }
        });
}

function getStudentById(){

    var id = document.getElementById('studentId').value;
    fetch(`https://localhost:44345/students/${id}`)
        .then((response) => response.json())
        .then((data) => {
            console.log(data);

            document.getElementById('yourStudent').innerHTML = "";

            document.getElementById("yourStudent").innerHTML += `Student: ${data.name} || Age: ${data.age}`
        });


}


function addStudent(){

    var newName = document.getElementById("addStudent_name").value;
    var newAge = document.getElementById("addStudent_age").value;

    var obj = {
        id: -1,
        name: newName,
        age: Number(newAge)

    }


    var data = JSON.stringify({obj});

    fetch(`${api}/students/add`, {
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

function removeStudentById(){

    var id = document.getElementById("removeStudentId").value;

    fetch(`https://localhost:44345/students/${id}`, {
        method: 'DELETE'
    })
    .then((response) => response.text())
    .then((data) => {
        console.log(data);
    })
}