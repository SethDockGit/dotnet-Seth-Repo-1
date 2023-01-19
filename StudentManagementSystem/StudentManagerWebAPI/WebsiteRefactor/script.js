var api = `https://localhost:44398`;

var students = new Array();

function showStudentView(){

    fetch(`${api}/student/students`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);

        students = data.students;

        document.getElementById("table").innerHTML = 
        '<table class="table table-bordered w-auto" style="background-color:lightskyblue;" id="studentsTable">';

        document.getElementById("studentsTable").innerHTML = `</thead><tr><th scope="col">ID</th>
          <th scope="col">Name (Last, First)</th><th scope="col"></th></tr></thead>`;

        for(let i = 0; i < data.students.length; i++){

            document.getElementById("studentsTable").innerHTML += 
            `<tr><th scope="row">${data.students[i].id}</th><td>${data.students[i].name}</td>
            <td id="student${[i]}"><button type="button" class="btn btn-primary" onclick="viewStudent(${[i]})">View</button></td>`;
        } 
    });
}
function viewStudent(i){


    document.getElementById(`editor`).innerHTML = `<td id="student${[i]}"><button type="button" class="btn btn-secondary" onclick="viewStudent(${[i]})">View</button></td>`;
    document.getElementById(`editor`).innerHTML += `</br> Age: ${students[i].age} </br> Courses:`;

    for (let j = 0; j < students[i].courses.length; j++){
                    
        document.getElementById(`editor`).innerHTML += `- Course: ${students[i].courses[j].courseName}
        CourseID: ${students[i].courses[j].courseId} 
        Professor: ${students[i].courses[j].professor}
        Description: ${students[i].courses[j].description}`;
    }  

    document.getElementById(`student${[i]}`).innerHTML += `</br> <button type="button" class="btn btn-secondary" onclick="editStudent(${[i]})">Edit</button>`;

}
function editStudent(i){

    document.getElementById(`student${[i]}`).innerHTML += `<form>
    <div class="mb-3">
      <label for="exampleInputEmail1" class="form-label">Email address</label>
      <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp">
      <div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
    </div>
    <div class="mb-3">
      <label for="exampleInputPassword1" class="form-label">Password</label>
      <input type="password" class="form-control" id="exampleInputPassword1">
    </div>
    <div class="mb-3 form-check">
      <input type="checkbox" class="form-check-input" id="exampleCheck1">
      <label class="form-check-label" for="exampleCheck1">Check me out</label>
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
  </form>`;
}