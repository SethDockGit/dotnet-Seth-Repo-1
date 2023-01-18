var api = `https://localhost:44398`;

function showStudentView(){

    fetch(`${api}/student/students`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("content").innerHTML = '<table class="table table-bordered w-auto" id="studentsTable">';

        for(let i = 0; i < data.students.length; i++){

            document.getElementById("studentsTable").innerHTML += 
            `<tr><td>Student ID: ${data.students[i].id}</td><td>${data.students[i].name}</td>
            <td>Age: ${data.students[i].age}</td><td id="courses${[i]}"></td>`;

            for (let j = 0; j < data.students[i].courses.length; j++){
                    
                document.getElementById(`courses${[i]}`).innerText = "Courses: ";
                document.getElementById(`courses${[i]}`).innerHTML += `</br>Course Name: ${data.students[i].courses[j].courseName}
                CourseID: ${data.students[i].courses[j].courseId} 
                Professor: ${data.students[i].courses[j].professor}
                Description: ${data.students[i].courses[j].description}`;
            }  
        } 
    });

    document.getElementById("edit").innerHTML = `<button type="button" class="btn btn-primary" onclick="selectEditStudents()">Edit Students</button>`;
}

function showCourseView(){
    fetch(`${api}/student/courses`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("content").innerHTML = '<table class="table table-bordered w-auto" id="coursesTable">';

        for(let i = 0; i < data.courses.length; i++){

            document.getElementById("coursesTable").innerHTML += 
            `<tr><td>Course ID: ${data.courses[i].courseId}</td><td>Name: ${data.courses[i].courseName}</td>
            <td>Professor: ${data.courses[i].professor}</td><td>Description: ${data.courses[i].description}</td>`;

        } 
    });

    document.getElementById("edit").innerHTML = `<button type="button" class="btn btn-primary" onclick="selectEditCourses()">Edit Courses</button>`;
}

function selectEditStudents(){

    document.getElementById("edit").innerText = "";
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="selectAddStudent()">Add a Student</button><button type="button" class="btn btn-primary" 
    onclick="selectRemoveStudent()">Remove a Student</button><button type="button" class="btn btn-primary" 
    onclick="editStudent()">Edit a Student</button><button type="button" class="btn btn-primary" 
    onclick="cancelEditStudents()">Cancel</button>`;

}

var newStudentName = "";
var newStudentAge = 0;
let courseIDs = new Array();
let courses = new Array();

function selectAddStudent(){

    document.getElementById("edit").innerHTML = `</br></br><input id="enterName" type="text" placeholder="Enter name: (Last, First)">`;
    document.getElementById("edit").innerHTML += `<input id="enterAge" type="text" placeholder="Enter Age">`;
    document.getElementById("edit").innerHTML += `</br><button type="button" class="btn btn-primary" 
    onclick="saveNewStudent()">Save New Student</button>`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="cancelAddStudent()">Cancel</button>`;

}
function saveNewStudent(){

    newStudentName = document.getElementById("enterName").value; 
    newStudentAge = document.getElementById("enterAge").value;

    var obj = {

        Id: Number(-1),  
        Name: newStudentName,
        Age: Number(newStudentAge),
        Courses: courses,
        CourseIDArray: courseIDs,
    }

    fetch(`${api}/student/addstudent`, {
        method: 'POST',
        body: JSON.stringify(obj),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);


        document.getElementById("message").innerHTML += `</br>${data.message}`;
        document.getElementById("message").innerHTML += `</br><button type="button" class="btn btn-primary" 
        onclick="continueToMain()">Continue</button>`;
    })
}
function continueToMain(){
    
    showStudentView();
    document.getElementById("message").innerText = "";

}
function cancelAddStudent(){
    
    selectEditStudents();
}
function selectRemoveStudent(){

    document.getElementById("edit").innerHTML = `<input id="studentRemoveId" type="text" placeholder="Enter the ID of the Student to Delete" style="width: 400px">`;
    document.getElementById("edit").innerHTML += `</br><button type="button" class="btn btn-primary" 
    onclick="confirmRemoveStudent()">Confirm: Remove Student</button>`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="cancelRemoveStudent()">Cancel</button>`;
}



function confirmRemoveStudent(){

    var deleteStudentID = document.getElementById("studentRemoveId").value;

    var toDelete = Number(deleteStudentID);

    fetch(`${api}/student/${toDelete}`, {
        method: 'DELETE'
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
        debugger;
        document.getElementById("message").innerHTML += `</br>${data.message}`;
        document.getElementById("message").innerHTML += `</br><button type="button" class="btn btn-primary" 
        onclick="continueToMain()">Continue</button>`;

    });
}

function cancelRemoveStudent(){

    selectEditStudents();
}

function editStudent(){

}
function cancelEditStudents(){

    showStudentView();
}
function selectEditCourses(){

    document.getElementById("edit").innerHTML = "";
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="addCourse()">Add a Course</button><button type="button" class="btn btn-primary" 
    onclick="removeCourse()">Remove a Course</button><button type="button" class="btn btn-primary" 
    onclick="editCourse()">Edit a Course</button><button type="button" class="btn btn-primary" 
    onclick="cancelEditCourses()">Cancel</button>`;

}
function addCourse(){

}
function removeCourse(){

}
function editCourse(){

}
function cancelEditCourses(){

}