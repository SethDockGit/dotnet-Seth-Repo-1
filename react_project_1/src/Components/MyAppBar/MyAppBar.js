import * as React from 'react';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import { AppBar } from '@mui/material';
import { Link } from 'react-router-dom';
import Typography from '@mui/material/Typography';



function MyAppBar() {

    return (
      <>
          <AppBar position="static">
            <Toolbar>
              <Typography variant="h4" sx={{marginRight: 5}}>Little Homie Rescue</Typography>
              <Link to={'/home'}>
                  <Button variant="contained" sx={{marginRight: 3}} color="secondary">Home</Button>    
              </Link>
              <Link to={'/pets'}>
                  <Button variant="contained" sx={{marginRight: 3}} color="secondary">View Pets</Button>    
              </Link>
              <Link to={'/pets/add'}>
                  <Button variant="contained" sx={{marginRight: 3}} color="secondary">Add A Pet</Button>    
              </Link>
            </Toolbar>
          </AppBar>
      </>
    );
  }

export default MyAppBar;
