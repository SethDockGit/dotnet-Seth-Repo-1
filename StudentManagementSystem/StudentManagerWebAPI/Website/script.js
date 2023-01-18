var api = `https://localhost:44398`;

let students = new Array();

function showStudentView(){

    fetch(`${api}/student/students`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        students = data.students;

        document.getElementById("content").innerHTML = '<table class="table table-bordered w-auto" style="background-color:lightskyblue; margin:auto; text-align:center" id="studentsTable">';

        for(let i = 0; i < data.students.length; i++){

            document.getElementById("studentsTable").innerHTML += 
            `<tr><td>Student ID: ${data.students[i].id}</td><td>${data.students[i].name}</td>
            <td>Age: ${data.students[i].age}</td><td id="courses${[i]}"></td>`;

            for (let j = 0; j < data.students[i].courses.length; j++){
                    
                document.getElementById(`courses${[i]}`).innerText = "Courses: ";
                document.getElementById(`courses${[i]}`).innerHTML += `</br>- Course: ${data.students[i].courses[j].courseName}
                CourseID: ${data.students[i].courses[j].courseId} 
                Professor: ${data.students[i].courses[j].professor}
                Description: ${data.students[i].courses[j].description}`;
            }  
        } 
    });

    document.getElementById("edit").innerHTML = `<button type="button" class="btn btn-secondary" onclick="studentEditOptions()">Edit Students</button>`;
}


function studentEditOptions(){

    document.getElementById("edit").innerText = "";
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-secondary" 
    onclick="selectAddStudent()">Add a Student</button><button type="button" class="btn btn-secondary" 
    onclick="selectRemoveStudent()">Remove a Student</button><button type="button" class="btn btn-secondary" 
    onclick="getStudentId()">Edit a Student</button><button type="button" class="btn btn-secondary" 
    onclick="cancelEditStudents()">Cancel</button>`;

}

var newStudentName = "";
var newStudentAge = 0;
let courseIDs = new Array();
let courses = new Array();

function selectAddStudent(){

    document.getElementById("edit").innerHTML = `</br></br><input id="enterName" type="text" placeholder="Enter name: (Last, First)">`;
    document.getElementById("edit").innerHTML += `<input id="enterAge" type="text" placeholder="Enter Age">`;
    document.getElementById("edit").innerHTML += `</br><button type="button" class="btn btn-secondary" 
    onclick="saveNewStudent()">Save New Student</button>`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-secondary" 
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
        document.getElementById("message").innerHTML += `</br><button type="button" class="btn btn-secondary" 
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
    document.getElementById("edit").innerHTML += `</br><button type="button" class="btn btn-secondary" 
    onclick="confirmRemoveStudent()">Confirm: Remove Student</button>`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-secondary" 
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
        document.getElementById("message").innerHTML += `</br><button type="button" class="btn btn-secondary" 
        onclick="continueToMain()">Continue</button>`;

    });
}

function cancelRemoveStudent(){

    selectEditStudents();
}

function getStudentId(){

    document.getElementById("edit").innerHTML = `<input id="editStudentById" style="width: 200px;" type="text" placeholder="Enter ID of student to edit.">
    </br><button type="button" class="btn btn-secondary" onclick="editStudent()">Confirm</button>`;

}

let tempID = -1;

function editStudent(){

    tempID = document.getElementById("editStudentById").value;

    //how to do error handling for invalid input? ask Kellen

    document.getElementById("edit").innerHTML = '<button type="button" class="btn btn-secondary" onclick="editStudentCourses()">Edit Courses</button>';
    document.getElementById("edit").innerHTML += '<button type="button" class="btn btn-secondary" onclick="editStudentInfo()">Edit Information</button>';
    document.getElementById("edit").innerHTML += '<button type="button" class="btn btn-secondary" onclick="cancelEditStudents()">Cancel</button>';
    
}
function editStudentInfo(){

    document.getElementById("edit").innerHTML = `<input id="editStudentName" type="text" placeholder="Enter Student name">`;
    document.getElementById("edit").innerHTML += `</br><input id="editStudentAge" type="text" placeholder="Enter Student age">`;
    document.getElementById("edit").innerHTML += `</br></br><button type="button" class="btn btn-secondary" onclick="ConfirmEditedStudent()">Finish and Confirm</button>`;
}
function editStudentCourses(){

    //if time later can show list of courses here, copypaste for loop up there but with the saved variables instead.
    document.getElementById("edit").innerHTML += `</br></br><input id="addCourseToStudent" type="text" placeholder="Enter a course ID to add">
    </br><button type="button" class="btn btn-secondary" onclick="addCourseToStudent()">Add Course</button>`;
    document.getElementById("edit").innerHTML += `</br></br><input id="removeCourseFromStudent" style="width: 225px" type="text" placeholder="Enter a course ID to remove">
    </br><button type="button" class="btn btn-secondary" onclick="removeCourseFromStudent()">Remove Course</button>`;

}
function addCourseToStudent(){

    var courseAddID = document.getElementById("addCourseToStudent").value;
    var castedCourseID = Number(courseAddID);
    var castedStudentID = Number(tempID);

    var transfer = {
        StudentId: castedStudentID, 
        CourseId: castedCourseID,
    }

    fetch(`${api}/student/addstudentcourse`, {
        method: 'POST',
        body: JSON.stringify(transfer),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("message").innertext = `${data.message}`;
    });
}
function removeCourseFromStudent(){

    var courseAddID = document.getElementById("removeCourseFromStudent").value;
    var castedID = Number(courseAddID);

    var transfer = {
        StudentId: tempStudent.StudentId, 
        CourseId: castedID,
    }

    fetch(`${api}/student/removestudentcourse`, {
        method: 'POST',
        body: JSON.stringify(transfer),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("message").innertext = data.message;
    });


}
function ConfirmEditedStudent(){
    
    tempStudent.Age = document.getElementById("enterStudentAge").value;
    tempStudent.StudentName = document.getElementById("enterStudentName").value;

    fetch(`${api}/student/editstudent`, {
        method: 'POST',
        body: JSON.stringify(tempStudent),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
    });
}

function cancelEditStudents(){

    showStudentView();
}
function showCourseView(){
    fetch(`${api}/student/courses`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("content").innerHTML = '<table class="table table-bordered w-auto" style="background-color:lightskyblue; margin:auto; text-align:center" id="coursesTable">';

        for(let i = 0; i < data.courses.length; i++){

            document.getElementById("coursesTable").innerHTML += 
            `<tr><td>Course ID: ${data.courses[i].courseId}</td><td>Name: ${data.courses[i].courseName}</td>
            <td>Professor: ${data.courses[i].professor}</td><td>Description: ${data.courses[i].description}</td>`;

        } 
    });

    document.getElementById("edit").innerHTML = `<button type="button" class="btn btn-secondary" onclick="selectEditCourses()">Edit Courses</button>`;
}
function selectEditCourses(){

    document.getElementById("edit").innerHTML = "";
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-secondary" 
    onclick="selectAddCourse()">Add a Course</button><button type="button" class="btn btn-secondary" 
    onclick="selectRemoveCourse()">Remove a Course</button><button type="button" class="btn btn-secondary" 
    onclick="selectEditCourse()">Edit a Course</button><button type="button" class="btn btn-secondary" 
    onclick="cancelEditCourses()">Cancel</button>`;

}
function selectAddCourse(){
    document.getElementById("edit").innerHTML = `</br></br><input id="enterCourseTitle" type="text" placeholder="Enter title of the course here">`;
    document.getElementById("edit").innerHTML += `<input id="enterAge" type="text" placeholder="Enter Age">`;
    document.getElementById("edit").innerHTML += `</br><button type="button" class="btn btn-secondary" 
    onclick="saveNewStudent()">Save New Student</button>`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-secondary" 
    onclick="cancelAddStudent()">Cancel</button>`;
}
function selectRemoveCourse(){

}
function selectEditCourse(){

}
function cancelEditCourses(){

}