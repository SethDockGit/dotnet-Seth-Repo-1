var api = `https://localhost:44398`;

var students = new Array();

function showStudentView(){

    fetch(`${api}/student/students`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        students = data.students;

        document.getElementById("content").innerHTML = 
        '<table class="table table-bordered w-auto" style="background-color:lightskyblue; margin:auto; text-align:center" id="studentsTable">';

        for(let i = 0; i < data.students.length; i++){

            document.getElementById("studentsTable").innerHTML += 
            `<tr><td>Student ID: ${data.students[i].id}</td><td>${data.students[i].name}</td>
            <td id="student${[i]}"><button type="button" class="btn btn-secondary" onclick="viewStudent(${[i]})">View</button></td>`;
        } 
    });
}
function viewStudent(i){


    document.getElementById(`student${[i]}`).innerHTML = `<td id="student${[i]}"><button type="button" class="btn btn-secondary" onclick="viewStudent(${[i]})">View</button></td>`;
    document.getElementById(`student${[i]}`).innerHTML += `</br> Age: ${students[i].age} </br> Courses:`;

    for (let j = 0; j < students[i].courses.length; j++){
                    
        document.getElementById(`student${[i]}`).innerHTML += `- Course: ${students[i].courses[j].courseName}
        CourseID: ${students[i].courses[j].courseId} 
        Professor: ${students[i].courses[j].professor}
        Description: ${students[i].courses[j].description}`;
    }  

    document.getElementById(`student${[i]}`).innerHTML += `</br> <button type="button" class="btn btn-secondary" onclick="editStudent(${[i]})">Edit</button>`;

}
function editStudent(i){


}