import TextField from "@mui/material/TextField";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import { Typography } from "@mui/material";
import { Link } from "react-router-dom";
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import InputAdornment from '@mui/material/InputAdornment';
import IconButton from '@mui/material/IconButton';
import OutlinedInput from '@mui/material/OutlinedInput';

export default function CreateAccount(){

const [username, setUsername] = useState('');
const [passwordOne, setPasswordOne] = useState('');
const [passwordTwo, setPasswordTwo] = useState('');
const [email, setEmail] = useState('');
const [showPasswordOne, setShowPasswordOne] = useState(false);
const [showPasswordTwo, setShowPasswordTwo] = useState(false);
const [message, setMessage] = useState('');

const handleUsernameChange = (e) => {
    setUsername(e.target.value);
}
const handleEmailChange = (e) => {
    setEmail(e.target.value);
}
const handlePasswordOneChange = (e) => {
    
}

const handleClickShowPasswordOne = () => setShowPasswordOne((show) => !show)

const handleMouseDownPasswordOne = (event) => {
    event.preventDefault();
};

const handlePasswordTwoChange = (e) => {

}
const handleClickShowPasswordTwo = () => setShowPasswordTwo((show) => !show)

const handleMouseDownPasswordTwo = (event) => {
    event.preventDefault();
};

const displayMessage = () => {

  <Typography variant="caption" color="red">{message}</Typography>
}
const meetsCriteria = () => {

}
const attemptCreateUser = () => {

  if(username == "" || email == "" || passwordOne == "" || passwordTwo == ""){
    setMessage("One or more fields were left blank.");
  }
  else if(passwordOne != passwordTwo){
    setMessage("Passwords must match.");
  }
  else if(!meetsCriteria()){
    setMessage("Password must include...");
  }
}
    return(
        <div>
            <Grid container sx={{justifyContent:'center', display:'flex', alignItems: 'center', m:16}}>
                <Grid item xs={5}/>
                <Grid item xs={2}>
                    <Typography variant="h5" sx={{mb:2}}>Create Account</Typography>
                    <Typography variant="caption">Enter Username</Typography>
                    
                    <TextField sx={{width: 265, mb:2}} placeholder="Username" onChange={handleUsernameChange}/>
                    
                    <Typography variant="caption">Enter E-mail</Typography>
                    
                    <TextField sx={{width: 265, mb:2}} placeholder="E-mail" onChange={handleEmailChange}/>
                    
                    <Typography variant="caption">Enter Password</Typography>
                    
                    <FormControl sx={{ width: 265, mb:2 }} variant="outlined">
                        <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
                        <OutlinedInput
                          id="outlined-adornment-password"
                          type={showPasswordOne ? 'text' : 'password'}
                          endAdornment={
                            <InputAdornment position="end">
                              <IconButton                              
                                onClick={handleClickShowPasswordOne}
                                onMouseDown={handleMouseDownPasswordOne}
                                edge="end"
                              >
                                {showPasswordOne ? <VisibilityOff /> : <Visibility />}
                              </IconButton>
                            </InputAdornment>
                          }
                          label="Password"
                          onChange={handlePasswordOneChange}
                        />
                    </FormControl>
                    
                    <Typography variant="caption">Confirm Password</Typography>
                    
                    <FormControl sx={{ width: 265, mb:2 }} variant="outlined">
                        <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
                        <OutlinedInput
                          id="outlined-adornment-password"
                          type={showPasswordTwo ? 'text' : 'password'}
                          endAdornment={
                            <InputAdornment position="end">
                              <IconButton                              
                                onClick={handleClickShowPasswordTwo}
                                onMouseDown={handleMouseDownPasswordTwo}
                                edge="end"
                              >
                                {showPasswordTwo ? <VisibilityOff /> : <Visibility />}
                              </IconButton>
                            </InputAdornment>
                          }
                          label="Password"
                          onChange={handlePasswordTwoChange}
                        />
                    </FormControl>
                    
                    {displayMessage()}
                    
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon"}}
                    onClick={attemptCreateUser}>Submit</Button>    
                </Grid>
                <Grid item xs={5}/>
            </Grid>
        </div>
    )
}
