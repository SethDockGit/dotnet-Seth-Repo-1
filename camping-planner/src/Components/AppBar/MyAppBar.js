import { AppBar, Toolbar } from "@mui/material";
import { Typography } from "@mui/material";
import { Link } from "react-router-dom";
import Button from "@mui/material/Button";

export default function MyAppBar(){

    return(

        <div>
            <AppBar position="static">
                <Toolbar sx={{backgroundColor:"green"}}>
                  <Typography variant="h4" sx={{marginRight: 5}}>CampPlan</Typography>
                  <Link  style={{ textDecoration: 'none' }} to={'/home'}>
                    <Button variant="contained" sx={{marginRight: 3, backgroundColor:"gray"}}>Home</Button>    
                  </Link>
                  <Link  style={{ textDecoration: 'none' }} to={'/trips'}>
                    <Button variant="contained" sx={{marginRight: 3, backgroundColor:"gray"}}>My Trips</Button>    
                  </Link>
                  <Link  style={{ textDecoration: 'none' }} to={'trips/create'}>
                    <Button variant="contained" sx={{marginRight: 3, backgroundColor:"gray"}}>Create A Trip</Button>    
                  </Link>
                </Toolbar>
            </AppBar>
        </div>
    )
}