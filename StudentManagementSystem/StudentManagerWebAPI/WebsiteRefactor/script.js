var api = `https://localhost:44398`;

var students = new Array();

var selectedStudentID = -1; //is changed only by viewStudent()

var selectedStudent;

function showStudentView(){

    fetch(`${api}/student/students`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        students = data.students;

        document.getElementById("table").innerHTML = 
        `<div class="table-wrapper-scroll-y my-custom-scrollbar">
        <table class="table table-responsive table-bordered table-fixed table-hover" style="background-color:lightskyblue;" id="studentsTable">`;

        document.getElementById("studentsTable").innerHTML = 
        `<colgroup><col style="width: 1%;"><col><col style="width: 1%;"></colgroup>
        </thead><tr><th scope="col">ID</th>
        <th scope="col">Name (Last, First)</th><th scope="col"></th></tr></thead><tbody></tbody></table></div>`;

        for(let i = 0; i < data.students.length; i++){

            document.getElementById("studentsTable").innerHTML += 
            `<tr><th scope="row">${data.students[i].id}</th><td>${data.students[i].name}</td>
            <td id="student${[i]}"><button type="button" class="btn btn-primary" onclick="viewStudent(${[i]})">View</button></td>`;
        } 

        document.getElementById("table").innerHTML += `<button type="button" class="btn btn-success"  style="margin:10px;" onclick="enrollNewStudent()">Enroll New Student</button>`;

    });
}
function enrollNewStudent(){

    document.getElementById("editForms").innerHTML =
    `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <div class="form-group">
            <label for="studentName">Student Name</label>
            <input type="text" class="form-control" id="enterName" placeholder="Enter Student Name (Last, First)">
         </div>
         <div class="form-group">
            <label for="studentAge">Student Age</label>
            <input type="text" class="form-control" id="enterAge" placeholder="Enter Student Age">
        </div>
        <button type="submit" onclick="saveNewStudent()" data-bs-toggle="modal" data-bs-target="#saveStudentModal"
        class="btn btn-primary">Submit</button>
    </form>`;
}
function saveNewStudent(){

    newStudentName = document.getElementById("enterName").value; 
    newStudentAge = document.getElementById("enterAge").value;

    var APIRequest = {

        Id: -1,
        Name: newStudentName,
        Age: newStudentAge,
        Courses: new Array(),
    }

    fetch(`${api}/student/addstudent`, {
        method: 'POST',
        body: JSON.stringify(APIRequest),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);


        document.getElementById("saveStudentTitle").innerText ="Add New Student";
        document.getElementById("saveStudentBody").innerText = data.message;
    })

}
function viewStudent(i){

    selectedStudentID = i + 1;

    document.getElementById("view").innerHTML = `<div class="card" style="width: 18rem;">
    <div class="card-body">
      <h5 class="card-title">${students[i].name}</h5>
      <h6 class="card-subtitle mb-2 text-muted">Age: ${students[i].age}</h6>
      <h6 class="card-subtitle mb-2 text-muted">ID: ${students[i].id}</h6>
      <p class="card-text">
      <button type="button" class="btn btn-outline-info" style="margin:10px;"  onclick="editStudent()">Edit</button>
      </br></br>Courses:</p>
    </div>
    <ul class="list-group list-group-flush" id="courses"></ul>
    <div class="card-body">
      <button type="button" class="btn btn-success btn-sm" style="margin:10px;"  onclick="addCourseToStudent()">Add a Course</button>
      <button type="button" class="btn btn-danger btn-sm" style="margin:10px;"  onclick="dropCourseFromStudent()">Drop a Course</button>
    </div>
    </div>`;

    for (let j = 0; j < students[i].courses.length; j++){
                    
        document.getElementById("courses").innerHTML += 
        `<li class="list-group-item">${students[i].courses[j].courseName} ID: ${students[i].courses[j].courseId}</li>`;
    }  
}
function editStudent(){


}
function addCourseToStudent(){

    document.getElementById("editForms").innerHTML =
    `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <div class="form-group">
            <label for="courseID">Course ID</label>
            <input type="text" class="form-control" id="courseIDToAdd" placeholder="Enter course ID">
         </div>
        <button type="submit" onclick="confirmAddCourseToStudent()" data-bs-toggle="modal" data-bs-target="#addCourseModal"
        class="btn btn-primary">Submit</button>
    </form>`;
}
function confirmAddCourseToStudent(){
    var courseAddID = document.getElementById("courseIDToAdd").value;
    var castedCourseID = Number(courseAddID);

    var APIRequest = {
        StudentId: selectedStudentID,
        CourseId: castedCourseID,
    }

    fetch(`${api}/student/addstudentcourse`, {
        method: 'POST',
        body: JSON.stringify(APIRequest),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("editForms").innerText = "";

        document.getElementById("addCourseTitle").innerText = `Student ${selectedStudentID}`;

        document.getElementById("addCourseBody").innerText = data.message;

    });
}
function dropCourseFromStudent(){

    document.getElementById("editForms").innerHTML =
    `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <div class="form-group">
            <label for="courseID">Course ID</label>
            <input type="text" class="form-control" id="courseIDToDrop" placeholder="Enter course ID">
         </div>
        <button type="submit" onclick="confirmDropCourseFromStudent()" data-bs-toggle="modal" data-bs-target="#dropCourseModal"
        class="btn btn-primary">Submit</button>
    </form>`;
}
function confirmDropCourseFromStudent(){

    var courseDropID = document.getElementById("courseIDToDrop").value;
    var castedCourseID = Number(courseDropID);

    var APIRequest = {
        StudentId: selectedStudentID,
        CourseId: castedCourseID,
    }

    fetch(`${api}/student/dropstudentcourse`, {
        method: 'POST',
        body: JSON.stringify(APIRequest),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        document.getElementById("editForms").innerText = "";

        document.getElementById("dropCourseTitle").innerText = `Student ${selectedStudentID}`;

        document.getElementById("dropCourseBody").innerText = data.message;

    });

}
function enrollNewStudent(){

}


var courses = new Array();

function showCourseView(){

    fetch(`${api}/student/courses`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        courses = data.courses;

        document.getElementById("table").innerHTML = 
        `<div class="table-wrapper-scroll-y my-custom-scrollbar">
        <table class="table table-responsive table-bordered table-fixed table-hover" style="background-color:lightskyblue;" id="coursesTable">`;

        document.getElementById("coursesTable").innerHTML = 
        `<colgroup><col style="width: 1%;"><col><col style="width: 1%;"></colgroup>
        </thead><tr><th scope="col">ID</th>
        <th scope="col">Title</th><th scope="col"></th></tr></thead><tbody></tbody></table></div>`;

        for(let i = 0; i < data.courses.length; i++){

            document.getElementById("coursesTable").innerHTML += 
            `<tr><th scope="row">${data.courses[i].courseId}</th><td>${data.courses[i].courseName}</td>
            <td><button type="button" class="btn btn-primary" onclick="viewCourse(${[i]})">View</button></td>`;
        } 

        document.getElementById("table").innerHTML += `<button type="button" class="btn btn-success"  style="margin:10px;" onclick="addNewCourse()">Add a New Course</button>`;
    });
}
function viewCourse(i){

    document.getElementById("view").innerHTML = `<div class="card" style="width: 18rem;">
    <div class="card-body">
      <h5 class="card-title">${courses[i].courseName}</h5>
      <h6 class="card-subtitle mb-2 text-muted">Professor: ${courses[i].professor}</h6>
      <h6 class="card-subtitle mb-2 text-muted">ID: ${courses[i].courseId}</h6>
      <p class="card-text">
      <button type="button" class="btn btn-outline-info" style="margin:10px;"  onclick="editCourse()">Edit</button>
      </br></br>Description: ${courses[i].description}</p>
    </div>
    <ul class="list-group list-group-flush" id="courses"></ul>
    </div>`;

}
function editCourse(){

}
function addNewCourse(){

}