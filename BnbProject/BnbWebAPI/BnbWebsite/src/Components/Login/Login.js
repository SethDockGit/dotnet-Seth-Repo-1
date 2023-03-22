import TextField from "@mui/material/TextField";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import { Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function Login(){

const [username, setUsername] = useState('');
const [Password, setPassword] = useState('');

const handleUsernameChange = (e) => {
    setUsername(e.target.value);
}
const handlePasswordChange = (e) => {
    
}
const displayMessage = () => {

}
const attemptLogin = () => {

}

    return(
        <div>
            <Grid container sx={{justifyContent:'center', display:'flex', alignItems: 'center', m:16}}>
                <Grid item xs={5}/>
                <Grid item xs={2}>
                    <Typography variant="h5" sx={{mb:2}}>Log In</Typography>
                    <Typography variant="caption">Enter Username</Typography>
                    <TextField sx={{width: 265, mb:2}} placeholder="Username" onChange={handleUsernameChange}/>
                    <Typography variant="caption">Enter Password</Typography>
                    <TextField sx={{width: 265, mb:2}} placeholder="Password" onChange={handlePasswordChange}/>
                    {displayMessage()}
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon"}}
                    onClick={attemptLogin}>Submit</Button>
                    <br/>    
                    <Typography variant="caption">Don't have an account?</Typography>
                    <Link style={{ textDecoration: 'none' }} to={'/user/create'}>
                        <Button>Create Account</Button>
                    </Link>
                </Grid>
                <Grid item xs={5}/>
            </Grid>
        </div>
    )
}