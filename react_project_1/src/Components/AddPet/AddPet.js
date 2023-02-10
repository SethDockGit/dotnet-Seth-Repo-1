import { TextField, Typography } from "@mui/material";
import Button from '@mui/material/Button';
import { useState } from "react";
import { useContext } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { PetsContext } from "../../Contexts/PetsContext";
import { Box } from "@mui/material";

function AddPet(){

const [name, setName] = useState("");
const [age, setAge] = useState();
const [species, setSpecies] = useState("");

const { pets, setPets } = useContext(PetsContext);
const navigate = useNavigate();


const handleNameChange = (e) => {

    setName(e.target.value);
  
  }
  const handleAgeChange = (e) => {
  
    setAge(e.target.value);
  
  }
  const handleSpeciesChange = (e) => {
  
    setSpecies(e.target.value);
  
  }
  const handlePetChange = () => {
  
    var newArray = pets.map(function(val, index){
        return(
            val.Id
        )
    });

    var nextId = Math.max(...newArray);

    let pet = 
      {
        Name: name,
        Age: age,
        Species: species,
        Id: nextId +1,
      }
    
    setPets([...pets, pet]);
    navigate('/pets');
  }
  
    return(

        <div>
            <Box ml={1} height={300} width={300} 
            sx={{ flexGrow: 1}}>
              <Typography sx={{margin:2}} variant="h5">Add New Pet</Typography>
              <div sx={{margin:2}}>
                <Typography variant="caption">Name</Typography>
                <br/>
                <TextField placeholder="Enter Name" sx={{margin:1}} variant="outlined" onChange={handleNameChange}/>
              </div>
              <div>
                <Typography variant="caption">Age</Typography>
                <br/>
                <TextField placeholder="Enter Age" sx={{margin:1}} variant="outlined" onChange={handleAgeChange}/>
              </div>
              <div>
                <Typography variant="caption">Species</Typography>
                <br/>
                <TextField placeholder="Enter Species" sx={{margin:1}} variant="outlined" onChange={handleSpeciesChange}/>
              </div>
              <div>
                <Button sx={{margin:2}} variant="outlined" onClick={handlePetChange}>Add</Button>
              </div>
            </Box>
        </div>
    )
}

export default AddPet;