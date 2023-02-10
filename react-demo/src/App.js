import { TextField, Typography } from "@mui/material";
import { useState } from 'react';
import Button from '@mui/material/Button';
import PeopleTable from "./Components/PeopleTable/PeopleTable";


function App() {

  let initialPeople = [
    {
      id: 0,
      firstName: "Doug",
      lastName: "Marlin",
      age: 42,
    },
    {
      id: 1,
      firstName: "Henry",
      lastName: "Rollins",
      age: 23
    },
    {
      id: 2,
      firstName: "Sarah",
      lastName: "Johnson",
      age: 4
    },
    {
      id: 3,
      firstName: "Bart",
      lastName: "Rogarts",
      age: 100
    },
    {
      id: 4,
      firstName: "Gorno",
      lastName: "Deeker",
      age: 55
    },
  ];

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [age, setAge] = useState();
  const [people, setPeople] = useState (initialPeople);

  const handleFirstNameChange = (e) => {
    setFirstName(e.target.value);
  }
  const handleLastNameChange = (e) => {
    setLastName(e.target.value);
  }
  const handleAgeChange = (e) => {
    setAge(e.target.value);
  }
  const handleAddPerson = () => {
    var highestId = Math.max(...people.map(person => person.id));

    var person = {
      id: highestId +1,
      firstName: firstName,
      lastName: lastName,
      age: age
    }

    setPeople([...people, person]);
  }

  return (
    <div className="App">
      <PeopleTable people={people}/>

      <Typography variant='h3'>Add Person</Typography>
      <div>
        <Typography variant="caption">First Name:</Typography>
        <TextField label="First Name" variant="outlined" onChange={handleFirstNameChange}/>
      </div>
      <div>
        <Typography variant="caption">Last Name:</Typography>
        <TextField label="Last Name" variant="outlined" onChange={handleLastNameChange}/>
      </div>
      <div>
        <Typography variant="caption">Age:</Typography>
        <TextField label="Age" variant="outlined" onChange={handleAgeChange}/>
      </div>
      <div>
        <Button variant='outlined' onClick={handleAddPerson}>Add Person</Button>
      </div>
    </div>
  );
}

export default App;
