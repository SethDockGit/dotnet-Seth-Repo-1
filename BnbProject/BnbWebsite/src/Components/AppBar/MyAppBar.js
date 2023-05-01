import { AppBar, Toolbar } from "@mui/material";
import { Typography } from "@mui/material";
import { Link } from "react-router-dom";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";
import dayjs from "dayjs";
import { useState } from "react";
import IconButton from '@mui/material/IconButton';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import { useNavigate } from 'react-router-dom';
import { useEffect } from "react";


export default function MyAppBar(){

const {user, setUser, isLoggedIn, setIsLoggedIn} = useContext(UserContext);
 
const navigate = useNavigate();
const [anchorEl, setAnchorEl] = useState();

const open = Boolean(anchorEl);
const handleClick = (event) => {
  setAnchorEl(event.currentTarget);
};
const handleClose = () => {
  setAnchorEl();
};

const verifyLogin = () => {

  if(!user){

      var elements = document.cookie.split('=');
      var id = Number(elements[1]);

      if(!isNaN(id)){
          setIsLoggedIn(true);
      }
      else{
        setIsLoggedIn(false);
      }
  }
  else{
      if(dayjs().isAfter(dayjs(user.logTime).add(6, 'hour'))){
        setIsLoggedIn(false);
      }
  } 
}
useEffect(() => {
  verifyLogin();
}, [])

const handleClickLogout = () => {
  setUser(null);
  setIsLoggedIn(false);
  let now = String(dayjs());
  document.cookie = `id=;expires=${now}UTC;path=/`;
  //this should overwrite any cookie so that it expires.

  navigate("/user/login");
}
const displayButton = () => {

  if(user != null && dayjs().isAfter(dayjs(user.logTime).add(6, 'hour'))){
    setIsLoggedIn(false);
  }

  return(

    <div>
      {isLoggedIn ?
      <div>
        <IconButton
          onClick={handleClick}
        >
          <AccountCircleIcon fontSize="large"/>
        </IconButton>
        <Menu
          anchorEl={anchorEl}
          open={open}
          onClose={handleClose}
        >
          <Link style={{ textDecoration: 'none', color:"GrayText" }} to={'/mystuff'}>
            <MenuItem>My Stuff</MenuItem>
          </Link>
          <MenuItem onClick={handleClickLogout}>Logout</MenuItem>
        </Menu>
      </div>
      : 
      <Link style={{ textDecoration: 'none' }} to={'/user/login'}>
        <Button variant="contained" sx={{":hover": {
        bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon"}}>Login
        </Button>    
      </Link>}
    </div>
  )
}
    return(

        <div>
            <AppBar position="static">
                <Toolbar sx={{backgroundColor:"sandybrown"}}>
                  <Grid container sx={{justifyContent: 'center', display: 'flex',
                  alignItems: 'center'}}>
                    <Grid item xs={2}/>
                    <Grid item sx={{justifyContent: 'center', display: 'flex',
                      alignItems: 'center'}} xs={5}>
                      <Typography variant="h4" sx={{margin:2}}>SpotFinder</Typography>
                    </Grid>
                    <Grid item xs={1.1}>
                      <Link  style={{ textDecoration: 'none' }} to={'/listings'}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"},justifyContent:'right', marginRight: 3, backgroundColor:"lightsalmon"}}>View Listings</Button>    
                      </Link>
                    </Grid>
                    <Grid item xs={.8}>
                      {displayButton()}
                    </Grid>
                  </Grid>
                </Toolbar>
            </AppBar>
        </div>
    )
}