var api = `https://localhost:44398`;

var students = new Array();

var selectedStudentID = -1; //is changed only by viewStudent()

var selectedStudent;

var selectedCourse;

var selectedCourseID = -1;

var courses = new Array();


function showStudentView() {

    document.getElementById("editForms").innerText = "";

    fetch(`${api}/student/courses`)
        .then((response) => response.json())
        .then((data) => {

            courses = data.courses;

        });

    fetch(`${api}/student/students`)
        .then((response) => response.json())
        .then((data) => {

            students = data.students;

            document.getElementById("table").innerHTML =
                `<div class="table-wrapper-scroll-y my-custom-scrollbar">
        <table class="table table-responsive table-bordered table-fixed table-hover" style="background-color:lightskyblue;" id="studentsTable">`;

            document.getElementById("studentsTable").innerHTML =
                `<colgroup><col style="width: 1%;"><col><col style="width: 1%;"></colgroup>
        </thead><tr><th scope="col">ID</th>
        <th scope="col">Name (Last, First)</th><th scope="col"></th></tr></thead><tbody></tbody></table></div>`;

            if (data.students !== null) {

                for (let i = 0; i < data.students.length; i++) {

                    document.getElementById("studentsTable").innerHTML +=
                        `<tr><th scope="row">${data.students[i].id}</th><td>${data.students[i].name}</td>
                <td id="student${[i]}"><button type="button" class="btn btn-primary" onclick="viewStudent(${i})">View</button></td>`;
                }

            }

            document.getElementById("table").innerHTML += `<button type="button" class="btn btn-success"  style="margin:10px;" onclick="enrollNewStudent()">Enroll New Student</button>`;

        });
}
function enrollNewStudent() {

    document.getElementById("editForms").innerHTML =
        `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <h4>Enroll New Student</h4>
        <div class="form-group">
            <label for="studentName">Student Name</label>
            <input type="text" class="form-control" id="enterName" placeholder="Enter Student Name (Last, First)">
         </div>
         <div class="form-group">
            <label for="studentAge">Student Age</label>
            <input type="text" class="form-control" id="enterAge" placeholder="Enter Student Age">
        </div>
        <button type="button" onclick="saveNewStudent()" data-bs-toggle="modal" data-bs-target="#saveStudentModal"
        class="btn btn-primary">Submit</button>
        <button type="button" class="btn btn-secondary" onclick="closeForm()">Cancel</button>
    </form>`;
}
function saveNewStudent() {

    newStudentName = document.getElementById("enterName").value;
    newStudentAge = document.getElementById("enterAge").value;

    let courseArray = new Array();

    if (newStudentAge === "" || newStudentName === "") {

        document.getElementById("saveStudentBody").innerText = "One or more fields were left blank. Please try again."

    }
    else if(newStudentAge === NaN || newStudentAge < 1){

        document.getElementById("saveStudentBody").innerText = "Age must be a number above zero."

    }
    else {
        var APIRequest = {

            Id: -1,
            Name: newStudentName,
            Age: Number(newStudentAge),
            Courses: courseArray,
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
                
                document.getElementById("saveStudentTitle").innerText = "Add New Student";
                document.getElementById("saveStudentBody").innerText = data.message;
            });
    }

}
function viewStudent(i) {

    selectedStudent = students[i];

    document.getElementById("editForms").innerText = "";

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

    for (let j = 0; j < students[i].courses.length; j++) {

        document.getElementById("courses").innerHTML +=
            `<li class="list-group-item">${students[i].courses[j].courseName} ID: ${students[i].courses[j].courseId}</li>`;
    }
}
function editStudent() {

    document.getElementById("editForms").innerHTML =
        `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <h4>Update Student Information</h4>
        <div class="form-group">
            <label for="name">Name</label>
            <input type="text" class="form-control" id="studentName" placeholder="Enter Name (Last, First)">
         </div>
         <div class="form-group">
            <label for="age">Age</label>
            <input type="text" class="form-control" id="studentAge" placeholder="Enter Student Age">
        </div>
        <button type="button" onclick="saveStudentInfo()" data-bs-toggle="modal" data-bs-target="#studentInfoModal"
        class="btn btn-primary">Submit</button>
        <button type="button" onclick="deleteStudent()" data-bs-toggle="modal" data-bs-target="#warnDeleteStudentModal"
        class="btn btn-outline-danger">Delete Student</button>
        <button type="button" class="btn btn-secondary" onclick="closeForm()">Cancel</button>
    </form>`;

}
function saveStudentInfo() {

    var name = document.getElementById("studentName").value;
    var age = document.getElementById("studentAge").value;

    if (age === NaN || age < 1) {
        document.getElementById("studentInfoBody").innerText = "Age must be a number above zero."
    }
    else if (name === "") {
        document.getElementById("studentInfoBody").innerText = "Name field cannot be blank."
    }
    else {
        var APIRequest = {

            StudentId: selectedStudent.id,
            Name: name,
            Age: Number(age),
        }

        fetch(`${api}/student/editstudent`, {
            method: 'POST',
            body: JSON.stringify(APIRequest),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {

                document.getElementById("studentInfoTitle").innerText = "Edit Student";
                document.getElementById("studentInfoBody").innerText = data.message;
                document.getElementById("editForms").innerHTML = "";
            });
    }



}
function deleteStudent() {

    document.getElementById("warnDeleteStudentTitle").innerText = "Delete Student"
    document.getElementById("warnDeleteStudentBody").innerText = `Are you sure you want to delete ${selectedStudent.name}?`;

}
function confirmDeleteStudent() {

    var toDelete = selectedStudent.id;

    fetch(`${api}/student/s${toDelete}`, {
        method: 'DELETE'
    })
        .then((response) => response.json())
        .then((data) => {

            document.getElementById("deleteStudentTitle").innerText = "Delete Student";
            document.getElementById("deleteStudentBody").innerText = data.message;
        });

}
function addCourseToStudent() {

    document.getElementById("editForms").innerHTML =

        `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
    <h4>Add A Course</h4>

         <div class="dropdown">
            <button class="btn btn-primary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            Available Courses
            </button>
            <ul class="dropdown-menu" id="availableCourses">

            </ul>
        </div>
        <div id="addCourseToStudentForm"></div>
        </br>
        <button type="button" onclick="confirmAddCourseToStudent()" data-bs-toggle="modal" data-bs-target="#addCourseModal"
        class="btn btn-primary">Submit</button>
        <button type="button" class="btn btn-secondary" onclick="closeForm()">Cancel</button>
    </form>`;

    var availableCourses = new Array();

    if (selectedStudent.courses.count === 0) {

        availableCourses = courses;

    }
    else {

        for (let i = 0; i < courses.length; i++) {

            availableCourses[i] = courses[i];

            for (let j = 0; j < selectedStudent.courses.length; j++) {

                if (selectedStudent.courses[j].courseId === courses[i].courseId) {
                    availableCourses[i].courseId = -1;
                }
            }
        }
    }

    document.getElementById("availableCourses").innerHTML = "";

    for (let j = 0; j < availableCourses.length; j++) {

        if (availableCourses[j].courseId !== -1) {

            document.getElementById("availableCourses").innerHTML += `<li onclick="selectCourseToAdd(${j})">${availableCourses[j].courseId}: ${availableCourses[j].courseName}</li>`;
        }
    }
}
function selectCourseToAdd(j) {

    selectedCourseID = courses[j].courseId;

    document.getElementById("addCourseToStudentForm").innerHTML = `</br>Selected: ${courses[j].courseName}</br>`;

}
function confirmAddCourseToStudent() {

    var APIRequest = {
        StudentId: selectedStudent.id,
        CourseId: selectedCourseID,
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

            document.getElementById("editForms").innerText = "";

            document.getElementById("addCourseTitle").innerText = `Student ${selectedStudent.id}`;

            document.getElementById("addCourseBody").innerText = data.message;

        });
}
function dropCourseFromStudent() {

    document.getElementById("editForms").innerHTML =
        `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <h4>Drop a Course</h4>
        <div class="form-group">
            <label for="courseID">Course ID</label>
            <input type="text" class="form-control" id="courseIDToDrop" placeholder="Enter course ID to drop">
         </div>
        <button type="button" onclick="confirmDropCourseFromStudent()" data-bs-toggle="modal" data-bs-target="#dropCourseModal"
        class="btn btn-primary">Submit</button>
        <button type="button" class="btn btn-secondary" onclick="closeForm()">Cancel</button>
    </form>`;
}
function confirmDropCourseFromStudent() {

    var courseDropID = document.getElementById("courseIDToDrop").value;
    var castedCourseID = Number(courseDropID);

    var APIRequest = {
        StudentId: selectedStudent.id,
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

            document.getElementById("editForms").innerText = "";

            document.getElementById("dropCourseTitle").innerText = `Student ${selectedStudent.id}`;

            document.getElementById("dropCourseBody").innerText = data.message;

        });

}
function showCourseView() {

    document.getElementById("editForms").innerText = "";

    fetch(`${api}/student/courses`)
        .then((response) => response.json())
        .then((data) => {

            courses = data.courses;

            document.getElementById("table").innerHTML =
                `<div class="table-wrapper-scroll-y my-custom-scrollbar">
        <table class="table table-responsive table-bordered table-fixed table-hover" style="background-color:lightskyblue;" id="coursesTable">`;

            document.getElementById("coursesTable").innerHTML =
                `<colgroup><col style="width: 1%;"><col><col style="width: 1%;"></colgroup>
        </thead><tr><th scope="col">ID</th>
        <th scope="col">Title</th><th scope="col"></th></tr></thead><tbody></tbody></table></div>`;

            if (data.courses !== null) {

                for (let i = 0; i < data.courses.length; i++) {

                    document.getElementById("coursesTable").innerHTML +=
                        `<tr><th scope="row">${data.courses[i].courseId}</th><td>${data.courses[i].courseName}</td>
                <td><button type="button" class="btn btn-primary" onclick="viewCourse(${i})">View</button></td>`;
                }
            }

            document.getElementById("table").innerHTML += `<button type="button" class="btn btn-success"  style="margin:10px;" onclick="addNewCourse()">Add a New Course</button>`;
        });
}
function viewCourse(i) {

    selectedCourse = courses[i];

    document.getElementById("editForms").innerText = "";

    document.getElementById("view").innerHTML = `<div class="card" style="width: 18rem;">
    <div class="card-body">
      <h5 class="card-title">${courses[i].courseName}</h5>
      <h6 class="card-subtitle mb-2 text-muted">Prof: ${courses[i].professor}</h6>
      <h6 class="card-subtitle mb-2 text-muted">ID: ${courses[i].courseId}</h6>
      <p class="card-text">
      <button type="button" class="btn btn-outline-info" style="margin:10px;"  onclick="editCourse()">Edit</button>
      </br></br>Description: ${courses[i].description}</p>
    </div>
    <ul class="list-group list-group-flush" id="courses"></ul>
    </div>`;

}
function editCourse() {

    document.getElementById("editForms").innerHTML =
        `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <h4>Update Course Information</h4>
        <div class="form-group">
            <label for="title">Title</label>
            <input type="text" class="form-control" id="updateCourseTitle" placeholder="Enter Title">
         </div>
         <div class="form-group">
            <label for="Professor">Professor</label>
            <input type="text" class="form-control" id="updateProfessor" placeholder="Enter Professor Name">
        </div>
        <div class="form-group">
            <label for="Description">Description</label>
            <input type="text" class="form-control" id="updateDescription" placeholder="Enter Course Description">
        </div>
        <button type="button" onclick="saveCourseInfo()" data-bs-toggle="modal" data-bs-target="#courseInfoModal"
        class="btn btn-primary">Submit</button>
        <button type="button" onclick="deleteCourse()" data-bs-toggle="modal" data-bs-target="#warnDeleteCourseModal"
        class="btn btn-outline-danger">Delete Course</button>
        <button type="button" class="btn btn-secondary" onclick="closeForm()">Cancel</button>
    </form>`;


}
function saveCourseInfo() {

    var title = document.getElementById("updateCourseTitle").value;
    var professor = document.getElementById("updateProfessor").value;
    var description = document.getElementById("updateDescription").value;

    if (title === "" || professor === "" || description === "") {
        document.getElementById("courseInfoBody").innerText = "One or more fields were left blank. Please try again."
    }
    else {

        var APIRequest = {

            CourseId: selectedCourse.courseId,
            CourseName: title,
            Professor: professor,
            Description: description,
        }

        fetch(`${api}/student/editcourse`, {
            method: 'POST',
            body: JSON.stringify(APIRequest),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {
                document.getElementById("courseInfoTitle").innerText = "Edit Course";
                document.getElementById("courseInfoBody").innerText = data.message;
                document.getElementById("editForms").innerHTML = "";
            });
    }
}
function deleteCourse() {

    document.getElementById("warnDeleteCourseTitle").innerText = "Delete Course"
    document.getElementById("warnDeleteCourseBody").innerText = `Are you sure you want to delete course ${selectedCourse.courseName}?`;

}
function confirmDeleteCourse() {

    var toDelete = selectedCourse.courseId;

    fetch(`${api}/student/c${toDelete}`, {
        method: 'DELETE'
    })
        .then((response) => response.json())
        .then((data) => {

            document.getElementById("deleteCourseTitle").innerText = "Delete Course";
            document.getElementById("deleteCourseBody").innerText = data.message;
        });

}
function addNewCourse() {

    document.getElementById("editForms").innerHTML =
        `<form style="padding: 20px; border:2px solid; border-radius: 5px; border-color:lightgray;">
        <h4>Add New Course</h4>
        <div class="form-group">
            <label for="courseTitle">Course Title</label>
            <input type="text" class="form-control" id="courseTitle" placeholder="Enter Title of Course">
         </div>
        <div class="form-group">
            <label for="professor">Professor</label>
            <input type="text" class="form-control" id="professor" placeholder="Enter Name of Professor">
        </div> 
        <div class="form-group">
            <label for="description">Description</label>
            <input type="text" class="form-control" id="description" placeholder="Enter Course Description">
        </div> 
        <button type="button" onclick="saveNewCourse()" data-bs-toggle="modal" data-bs-target="#saveCourseModal"
        class="btn btn-primary">Submit</button>
        <button type="button" onclick="closeForm()" class="btn btn-secondary">Cancel</button>
    </form>`;
}
function saveNewCourse() {

    var newCourseTitle = document.getElementById("courseTitle").value;
    var professor = document.getElementById("professor").value;
    var description = document.getElementById("description").value;

    if (newCourseTitle === "" || professor === "" || description === "") {
        document.getElementById("saveCourseBody").innerText = "One or more fields were left blank. Please try again."
    }
    else {

        var APIRequest = {

            CourseId: -1,
            CourseName: newCourseTitle,
            Professor: professor,
            Description: description,
        }

        fetch(`${api}/student/addcourse`, {
            method: 'POST',
            body: JSON.stringify(APIRequest),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {

                document.getElementById("saveCourseTitle").innerText = "Add New Course";
                document.getElementById("saveCourseBody").innerText = data.message;
            });
    }
}
function closeForm() {

    document.getElementById("editForms").innerHTML = "";
}