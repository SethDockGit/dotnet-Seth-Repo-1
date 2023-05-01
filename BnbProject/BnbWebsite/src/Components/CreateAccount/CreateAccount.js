import TextField from "@mui/material/TextField";
import Grid from '@mui/material/Unstable_Grid2';
import { useContext, useState } from "react";
import Button from "@mui/material/Button";
import { Typography } from "@mui/material";
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import InputAdornment from '@mui/material/InputAdornment';
import IconButton from '@mui/material/IconButton';
import OutlinedInput from '@mui/material/OutlinedInput';
import PasswordChecklist from "react-password-checklist";
import { UserContext } from '../../Contexts/UserContext/UserContext';
import { Link } from "react-router-dom";
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import dayjs from "dayjs";
import { useNavigate } from 'react-router-dom';
import Error from "../Subcomponents/Error/Error";

export default function CreateAccount(){

const api = `https://localhost:44305`;

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  borderRadius:3,
  boxShadow: 24,
  p: 4,
};

const {user, setUser, isLoggedIn, setIsLoggedIn} = useContext(UserContext);
const [username, setUsername] = useState('');
const [passwordOne, setPasswordOne] = useState('');
const [passwordTwo, setPasswordTwo] = useState('');
const [email, setEmail] = useState('');
const [showPasswordOne, setShowPasswordOne] = useState(false);
const [showPasswordTwo, setShowPasswordTwo] = useState(false);
const [errorMessage, setErrorMessage] = useState('');
const [valid, setValid] = useState(false);
const [modalOpen, setModalOpen] = useState(false);
const [fail, setFail] = useState(false);
const navigate = useNavigate();


const handleUsernameChange = (e) => {
    setUsername(e.target.value);
}
const handleEmailChange = (e) => {
    setEmail(e.target.value);
}
const handlePasswordOneChange = (e) => {
    setPasswordOne(e.target.value);
}

const handleClickShowPasswordOne = () => setShowPasswordOne((show) => !show)

const handleMouseDownPasswordOne = (event) => {
    event.preventDefault();
};

const handlePasswordTwoChange = (e) => {
  setPasswordTwo(e.target.value);
}
const handleClickShowPasswordTwo = () => setShowPasswordTwo((show) => !show)

const handleMouseDownPasswordTwo = (event) => {
    event.preventDefault();
};

const attemptCreateUser = () => {
  
  if(username == "" || email == "" || passwordOne == "" || passwordTwo == ""){
    setFail(true);
    setErrorMessage("One or more fields were left blank.");
    
  }
  else if(!valid){
    setFail(true);
    setErrorMessage("Password must meet criteria.");
  }
  else{

    var APIRequest = {
      Username: username,
      Email: email,
      Password: passwordTwo,
    }

    fetch(`${api}/bnb/newaccount`, {
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
            setFail(true);
            setErrorMessage(data.message);
          }
          else{

            var userObject = {
              id: data.user.id,
              username: data.user.username,
              listings: data.user.listings,
              favorites: data.user.favorites,
              stays: data.user.stays,
              logTime: dayjs()
            };

            setUser(userObject);
            setIsLoggedIn(true);

            let expireTime = String(dayjs().add(6,'hour'));

            document.cookie = `id=${data.user.id};expires=${expireTime}UTC;path=/`;

            setModalOpen(true);
          }
      });
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

                    <PasswordChecklist
			              	rules={["minLength","specialChar","number","capital","match"]}
			              	minLength={5}
			              	value={passwordOne}
			              	valueAgain={passwordTwo}
			              	onChange={(isValid) => {setValid(isValid)}}
			              />
                    
                    <Error message={errorMessage} bool={fail}/>

                    <br/>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon", mt:2}}
                    onClick={attemptCreateUser}>Submit</Button>    
                </Grid>
                <Grid item xs={5}/>
                <Modal
                  open={modalOpen}
                  onClose={() => navigate("/mystuff")}
                  >
                  <Box sx={style}>
                      <Typography variant="h5">Success!</Typography>
                      <Link style={{ textDecoration: 'none' }} to={'/mystuff'}>
                        <Button>Go To Your Account</Button>
                      </Link>
                  </Box>
                </Modal>
            </Grid>
        </div>
    )
}
