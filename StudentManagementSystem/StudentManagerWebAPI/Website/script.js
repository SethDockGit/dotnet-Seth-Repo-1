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

    document.getElementById("edit").innerHTML = "";
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="addStudent()">Add a Student</button><button type="button" class="btn btn-primary" 
    onclick="removeStudent()">Remove a Student</button><button type="button" class="btn btn-primary" 
    onclick="editStudent()">Edit a Student</button><button type="button" class="btn btn-primary" 
    onclick="cancelEditStudents()">Cancel</button>`;

}

var newStudentName = "";
var newStudentAge = 0;
let courseIDs = new Array();

function addStudent(){

    document.getElementById("edit").innerHTML += `</br><input id="enterName" type="text" placeholder="Enter name: (Last, First)">`;
    document.getElementById("edit").innerHTML += `<input id="enterAge" type="text" placeholder="Enter Age">`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="saveNewStudent()">Save New Student</button>`;
    document.getElementById("edit").innerHTML += `<button type="button" class="btn btn-primary" 
    onclick="cancelAddStudent()">Cancel</button>`;


    //here I'll add to the "edit" div inputs to add for new student, turn into an object,
    //and give over to C#. Have an option to cancel where any global variables will be reset.
}

function saveNewStudent(){

    newStudentName = document.getElementById("enterName").value; 
    newStudentAge = document.getElementById("enterAge").value;

    var obj = {

        Id: -1,  
        Name: newStudentName,
        Age: newStudentAge,
        Courses: undefined,
        CourseIDArray: undefined,
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

        //document.getElementById("Message").innerText = "";
        //document.getElementById("Message").innerText += data.message;
        //make a button to continue/reset
    })
}

function cancelAddStudent(){

}

function removeStudent(){

}
function editStudent(){

}
function cancelEditStudents(){

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