import TextField from "@mui/material/TextField";
import Grid from '@mui/material/Unstable_Grid2';
import { useContext, useState } from "react";
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
import { useNavigate } from 'react-router-dom';
import { UserContext } from "../../Contexts/UserContext/UserContext";
import dayjs from "dayjs";
import MyStuff from '../MyStuff/MyStuff';

export default function Login(){

const api = `https://localhost:44305`;

const {user, setUser} = useContext(UserContext);
const [username, setUsername] = useState('');
const [password, setPassword] = useState('');
const [errorMessage, setErrorMessage] = useState('');
const [showPassword, setShowPassword] = useState(false);
const navigate = useNavigate();

const handleUsernameChange = (e) => {
    setUsername(e.target.value);
}
const handlePasswordChange = (e) => {
    setPassword(e.target.value);
}
const handleClickShowPassword = () => setShowPassword((show) => !show)

const handleMouseDownPassword = (event) => {
    event.preventDefault();
};
const displayErrorMessage = () => {

    return(
        <Typography variant="caption" color="red">{errorMessage}</Typography>
    )
}
const attemptLogin = () => {

    var APIRequest = {
        Username: username,
        Password: password
    };

    fetch(`${api}/bnb/authenticate`, {
        method: 'POST',
        body: JSON.stringify(APIRequest),
        headers: {
            'Content-Type': 'application/json'
        }
        })
        .then((response) => response.json())
        .then((data) => {
            console.log(data);

            if(!data.success){
                setErrorMessage(data.message);
            }
            else{

                var user = {
                    id: data.user.id,
                    username: data.user.username,
                    listings: data.user.listings,
                    favorites: data.user.favorites,
                    stays: data.user.stays,
                    isLoggedIn: true,
                    logTime: dayjs()
                  };
      
                setUser(user);

                navigate("/mystuff");
            }
        });
}

    return(
        <div>
            <Grid container sx={{justifyContent:'center', display:'flex', alignItems: 'center', m:16}}>
                <Grid item xs={5}/>
                <Grid item xs={2}>
                    <Typography variant="h5" sx={{mb:2}}>Log In</Typography>
                    <Typography variant="caption">Enter Username</Typography>
                    <TextField sx={{width: 265, mb:2}} placeholder="Username" onChange={handleUsernameChange}/>

                    <FormControl sx={{ width: 265, mb:2 }} variant="outlined">
                        <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
                        <OutlinedInput
                          id="outlined-adornment-password"
                          type={showPassword ? 'text' : 'password'}
                          endAdornment={
                            <InputAdornment position="end">
                              <IconButton                              
                                onClick={handleClickShowPassword}
                                onMouseDown={handleMouseDownPassword}
                                edge="end"
                              >
                                {showPassword ? <VisibilityOff /> : <Visibility />}
                              </IconButton>
                            </InputAdornment>
                          }
                          label="Password"
                          onChange={handlePasswordChange}
                        />
                    </FormControl>

                    {displayErrorMessage()}
                    <br/>
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