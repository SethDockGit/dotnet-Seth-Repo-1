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
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { useNavigate } from 'react-router-dom';



export default function CreateTrip(){

    const startCrew = []
    const startGear = []

    const { trips, setTrips } = useContext(TripsContext);

    const [startDate, setStartDate] = React.useState('');
    const [endDate, setEndDate] = React.useState('');
    const [location, setLocation] = useState("");
    const [crew, setCrew] = useState(startCrew);
    const [crewName, setCrewName] = useState("");
    const [crewPhone, setCrewPhone] = useState("");
    const [crewEmail, setCrewEmail] = useState("");
    const [gear, setGear] = useState(startGear);
    const [gearName, setGearName] = useState("");
    const [gearQuantity, setGearQuantity] = useState(0);
    const [dateMessage, setDateMessage] = useState("No dates selected");
    const [failCreateTrip, setFailCreateTrip] = useState(false);
    const navigate = useNavigate();
    

    const handleStartDateChange = (newValue) => {

        if (dayjs(newValue).isAfter(dayjs(endDate))){
            setDateMessage("Start date cannot be later than end date.");
        }
        else if(dayjs(newValue).isBefore(dayjs(endDate))){

            setDateMessage(`${dayjs(newValue).format('MM/DD/YYYY')} to ${dayjs(endDate).format('MM/DD/YYYY')}`);
            setStartDate(newValue);

        }
        else{
            setStartDate(newValue);
        }
    }
    const handleEndDateChange = (newValue) => {

        if (dayjs(newValue).isBefore(dayjs(startDate))){
            setDateMessage("End date cannot be earlier than start date.");
        }
        else if (dayjs(newValue).isAfter(dayjs(startDate))){

            setDateMessage(`${dayjs(startDate).format('MM/DD/YYYY')} to ${dayjs(newValue).format('MM/DD/YYYY')}`);
            setEndDate(newValue);

        }
        else{
            setEndDate(newValue);
        }
    }
    const showDateMessage = () => {

        return (
            <div>
            <Typography id="showDate" style={{margin:15, fontWeight:'light', fontSize: 16}} variant="h5">
                {dateMessage}
            </Typography> 
            </div>
        )
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

        var nextId;

        if(crew.length === 0){
            nextId = 0;
        }
        else{
            var newArray = crew.map(function(val, index){
                return(
                    val.id
                )
            });
            nextId = Math.max(...newArray);
        }

        let crewMember = 
            {
                id: nextId +1,
                name: crewName,
                phone: crewPhone,
                email: crewEmail,
            }
        
        setCrew([...crew, crewMember]);
    }
    const handleClickRemoveCrew = (e) => {

        const value1 = e.currentTarget.getAttribute("data-value1")
        
        var newCrew = crew.filter(c => c.id != value1);

        setCrew(newCrew);
            
    }
    const handleGearNameChange = (e) => {
        setGearName(e.target.value);
    }
    const handleGearQuantityChange = (e) => {
        setGearQuantity(e.target.value);
    }
    const handleGearChange = () => {

        var nextId;

        if(gear.length === 0){
            nextId = 0;
        }
        else{
            var newArray = gear.map(function(val, index){
                return(
                    val.id
                )
            });
            nextId = Math.max(...newArray);
        }

        let gearToAdd = 
            {
                id: nextId +1,
                name: gearName,
                quantity: gearQuantity,
            }
        
        setGear([...gear, gearToAdd]);
    }
    const handleClickRemoveGear = (e) => {

        const value1 = e.currentTarget.getAttribute("data-value1")

        var newGear = gear.filter(g => g.id != value1);

        setGear(newGear);
    }
    const handleTripChange = () => {

        if (location == "" || startDate == "" || endDate == ""){

            setFailCreateTrip(true);

        }
        else{
            
            var newArray = trips.map(function(val, index){
                return(
                    val.id
                )
            });

            var nextId = Math.max(...newArray);

            let trip = {
                id: nextId +1,
                location: location,
                startDate: startDate,
                endDate: endDate,
                crew: crew,
                gear: gear,
            }

            setTrips([...trips, trip]);
            navigate("/trips");

        }
    }
    const failMessage = () => {
        
        return (
            <div>
            {
            failCreateTrip &&   
                <div style={{marginTop:15}}>
                    <Typography color="red" variant="h6">Error: Must enter a date and location</Typography>
                </div>    
            }
            </div>
        )
    }
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
                    <Button type="button" onClick={handleClickRemoveCrew} data-value1={val.id}>x</Button>
                </ListItem>           
            )        
        })
    }
    const showGear = () => {     
        return gear.map(function(val, index){
            return(
                <TableRow key={index}>
                    <TableCell size="small">{val.name}</TableCell>
                    <TableCell size="small" align="center">{val.quantity}</TableCell>
                    <TableCell size="small" align="right">
                        <Button type="button" onClick={handleClickRemoveGear} data-value1={val.id}>Remove</Button>
                    </TableCell>
                </TableRow>
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
            renderInput={(params) => <TextField {...params} error={false} />}
            />

            <DesktopDatePicker
            label="Trip End Date"
            inputFormat="MM/DD/YYYY"
            value={endDate}
            onChange={handleEndDateChange}
            renderInput={(params) => <TextField {...params} error={false} />}
            />
        </div> 
        <div>
        {showDateMessage()}
        </div>
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

                <TableContainer sx={{ maxWidth: 484, backgroundColor:'mediumaquamarine', marginTop:2 }} component={Paper}>
                  <Table sx={{ maxWidth: 484 }}>
                    <TableHead sx={{ backgroundColor:'lightsteelblue' }}>
                      <TableRow>
                        <TableCell>Item</TableCell>
                        <TableCell align="center">Quantity</TableCell>
                        <TableCell align="right"/>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                    {showGear()}
                    </TableBody>
                  </Table>
                </TableContainer>
        <div style={{marginTop:15}}>
            <Typography variant="caption">Add Gear</Typography><br/>
            <TextField placeholder='Name' onChange={handleGearNameChange}></TextField>
            <TextField placeholder='Quantity' onChange={handleGearQuantityChange}></TextField>
            <Button variant="contained" sx={{backgroundColor:"gray"}} onClick={handleGearChange}>Add</Button>
        </div>
        </div>
        <Divider light />
        <Button style={{margin:15}} variant="contained" sx={{backgroundColor:"gray"}} onClick={handleTripChange}>Create New Trip</Button>
        {failMessage()}
        </>

    )

}