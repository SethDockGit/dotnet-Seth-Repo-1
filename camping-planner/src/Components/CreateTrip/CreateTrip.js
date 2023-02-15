import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { ListItem, TextField, Typography } from '@mui/material';
import dayjs from 'dayjs';
import { TripsContext } from '../../Contexts/TripsContext';
import { useContext } from 'react';
import React from 'react';
import Divider from '@mui/material/Divider';
import { useState } from 'react';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';



export default function CreateTrip(){

    const startCrew = []

    const { trips, setTrips } = useContext(TripsContext);

    const [startDate, setStartDate] = React.useState(dayjs(''));
    const [endDate, setEndDate] = React.useState(dayjs(''));
    const [location, setLocation] = useState("");
    const [crew, setCrew] = useState(startCrew);
    const [crewName, setCrewName] = useState("");
    const [crewPhone, setCrewPhone] = useState("");
    const [crewEmail, setCrewEmail] = useState("");

    //const [trip, setTrip] = useState();
    //this will be used for editing an existing trip, not creating a new one.

    const handleStartDateChange = (newValue) => {
      setStartDate(newValue);
    }
    const handleEndDateChange = (newValue) => {
        setEndDate(newValue);
    }
    const handleLocationChange = (e) => {
        setLocation(e.target.value);
    }
    const handleCrewNameChange = (e) => {
        setCrewName(e.target.value);
    }
    const handleCrewPhoneChange = (e) => {
        setCrewPhone(e.target.value);
    }
    const handleCrewEmailChange = (e) => {
        setCrewEmail(e.target.value);
    }
    const handleCrewChange = () => {

        var newArray = crew.map(function(val, index){
            return(
                val.Id
            )
        });
    
        var nextId = Math.max(...newArray);

        let crewMember = 
            {
                id: nextId +1,
                name: crewName,
                phone: crewPhone,
                email: crewEmail,
            }
        debugger;
        
        setCrew([...crew, crewMember]);
    }
    const handleTripChange = () => {

        var newArray = trips.map(function(val, index){
            return(
                val.Id
            )
        });

        var nextId = Math.max(...newArray);

        let trip = {
            id: nextId,
            location: location,
            startDate: startDate,
            endDate: endDate,
            crew: null,
            gear: null,
        }

        setTrips([...trips, trip]);
    }
    const handleClickRemoveCrew = (e) => {

        const value1 = e.currentTarget.getAttribute("data-value1")
        
        var newCrew = crew.filter(c => c.id !== value1);
        
        setCrew(newCrew);
            
    }

    const displayDate = () => {

        return(
        <Typography style={{margin:15, fontWeight:'light', fontSize: 16}} variant="h5">
            {dayjs(startDate).format('MM/DD/YYYY')} to {dayjs(endDate).format('MM/DD/YYYY')}
        </Typography>
        )
    }
    //I want it to only display the date once an end date has been determined.

    const displayLocation = () => {

        return(
            <Typography style={{margin:15, fontWeight:'light', fontSize: 16}} variant="h5">
                {location}
            </Typography>
        )
    }
    const showCrewList = () => {

        return crew.map(function(val, index){

                return(
                    
                    <ListItem key={index}>
                        {val.name} • {val.phone} • {val.email}
                        <Button type="button" class="close" onClick={handleClickRemoveCrew} data-value1={val.id}>x</Button>
                    </ListItem>           
                    
                )        
        })
    }

    return(
        <>
        <Typography variant="h4" style={{margin:15}}>New Trip</Typography>
        <Divider light />
        <div style={{margin:15, marginTop:15}}>
            <Typography variant="h4" sx={{fontSize:30}}>Dates</Typography>
            <Typography variant="caption">Select a Date Range</Typography>
            <br/>
            <DesktopDatePicker
            label="Trip Start Date"
            inputFormat="MM/DD/YYYY"
            value={startDate}
            onChange={handleStartDateChange}
            renderInput={(params) => <TextField {...params} />}
            />

            <DesktopDatePicker
            label="Trip End Date"
            inputFormat="MM/DD/YYYY"
            value={endDate}
            onChange={handleEndDateChange}
            renderInput={(params) => <TextField {...params} />}
            />
        </div> 
        {displayDate()}
        <Divider light />
        <div style={{margin:15}}>
            <Typography variant="h4" sx={{fontSize:30}}>Location</Typography>
            <Typography variant="caption">Name a Location</Typography>
            <br/>
            <TextField placeholder='Enter Location' onChange={handleLocationChange}></TextField>
        </div>
        {displayLocation()}
        <Divider light />
        <div style={{margin:15}}>
            <Typography variant="h4" sx={{fontSize:30}}>Crew</Typography>
            <br/>
            <List sx={{
                    width: '100%',
                    maxWidth: 500,
                    bgcolor: 'background.oldlace',
                    position: 'relative',
                    overflow: 'auto',
                    maxHeight: 300,
                    '& ul': { padding: 0 },
                  }}>
                    {showCrewList()}
            </List>
            <div>
                <Typography variant="caption">Add Crew Member</Typography><br/>
                <TextField placeholder='Name' onChange={handleCrewNameChange}></TextField>
                <TextField placeholder='Phone' onChange={handleCrewPhoneChange}></TextField>
                <TextField placeholder='Email' onChange={handleCrewEmailChange}></TextField>
                <Button variant="contained" sx={{backgroundColor:"gray"}} onClick={handleCrewChange}>Add</Button>
            </div>
            <br/><br/>
        </div>
        <Divider light />
        <div style={{margin:15}}>
            <Typography variant="h4" sx={{fontSize:30}}>Gear</Typography>

                <TableContainer sx={{ maxWidth: 650 }} component={Paper}>
                  <Table sx={{ maxWidth: 650 }}>
                    <TableHead>
                      <TableRow>
                        <TableCell>Item</TableCell>
                        <TableCell align="right">Quantity</TableCell>
                        <TableCell align="right">Who's bringing it?</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                    </TableBody>
                  </Table>
                </TableContainer>
        </div>
        <Divider light />
        <Button style={{margin:15}} variant="contained" sx={{backgroundColor:"gray"}} onClick={handleTripChange}>Create New Trip</Button>
        </>

    )

}