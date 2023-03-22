import { AppBar, Toolbar } from "@mui/material";
import { Typography } from "@mui/material";
import { Link } from "react-router-dom";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";

export default function MyAppBar(){

    return(

        //the login link button will be username or mystuff when logged in. Some sort of function
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
                    <Grid item xs={2}>
                      <Link  style={{ textDecoration: 'none' }} to={'/listings'}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"},justifyContent:'right', marginRight: 3, backgroundColor:"lightsalmon"}}>View Listings</Button>    
                      </Link>
                      <Link style={{ textDecoration: 'none' }} to={'/user/login'}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon"}}>Login</Button>    
                      </Link>
                    </Grid>
                  </Grid>
                </Toolbar>
            </AppBar>
        </div>
    )

}