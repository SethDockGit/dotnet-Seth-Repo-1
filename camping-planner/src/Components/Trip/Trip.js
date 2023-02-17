import { useParams } from "react-router-dom";
import { useContext } from "react";
import { TripsContext } from "../../Contexts/TripsContext";
import { useState } from "react";
import { Typography } from "@mui/material";
import Divider from "@mui/material/Divider";
import dayjs from "dayjs";
import ListItem from "@mui/material/ListItem";
import Button from "@mui/material/Button";
import List from "@mui/material/List";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import TextField from "@mui/material/TextField";
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import React from "react";

export default function Trip(){

    const {trips} = useContext(TripsContext)
    const {id} = useParams();
    const [trip, setTrip] = useState(trips.find(t => t.id == id));
    const [crew, setCrew] = useState(trip.crew);
    const [crewName, setCrewName] = useState("");
    const [crewPhone, setCrewPhone] = useState("");
    const [crewEmail, setCrewEmail] = useState("");
    const [displayCrewForm, setDisplayCrewForm] = useState();
    const [gear, setGear] = useState(trip.gear);
    const [location, setLocation] = useState(trip.location);
    const [displayLocationForm, setDisplayLocationForm] = useState();
    const [startDate, setStartDate] = React.useState(dayjs(trip.startDate));
    const [endDate, setEndDate] = React.useState(dayjs(trip.endDate));

    const handleClickRemoveCrew = (e) => {

        const value1 = e.currentTarget.getAttribute("data-value1")
        
        var newCrew = trip.crew.filter(c => c.id != value1);

        trip.crew = newCrew;
        setCrew(newCrew);
        setTrip(trip);    
    }
    const handleClickRemoveGear = (e) => {

        const value1 = e.currentTarget.getAttribute("data-value1")
    
        var newGear = trip.gear.filter(g => g.id != value1);
    
        trip.gear = newGear;
        setGear(newGear);
        setTrip(trip);
    }
    const showCrewList = () => {

        return trip.crew.map(function(val, index){
            return( 
                <ListItem key={index}>
                    {val.name} • {val.phone} • {val.email}
                    <Button type="button" onClick={handleClickRemoveCrew} data-value1={val.id}>x</Button>
                </ListItem>                
            )        
        })
    }
    const showCrewForm = () => {

        setDisplayCrewForm(

            <div>
                <Typography variant="caption">Add Crew Member</Typography><br/>
                <TextField placeholder='Name' onChange={handleCrewNameChange}></TextField>
                <TextField placeholder='Phone' onChange={handleCrewPhoneChange}></TextField>
                <TextField placeholder='Email' onChange={handleCrewEmailChange}></TextField>
                <Button variant="contained" sx={{backgroundColor:"gray"}} onClick={handleCrewChange}>Add</Button>
            </div>
        )
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
        trip.crew = crew;
        debugger;
        setTrip(trip);
    }
    const showGear = () => {  
        return trip.gear.map(function(val, index){
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
    const showLocationForm = () => {

        setDisplayLocationForm(

            <div>
                <Typography variant="caption">Name a Location</Typography>
                <br/>
                <TextField placeholder='Enter Location' onChange={handleLocationChange}></TextField>
                <Button type="button" onClick={saveLocationChange} data-value1={location}>Save</Button>
            </div>
        )    
    }
    const handleLocationChange = (e) => {
        setLocation(e.target.value);
    }
    const saveLocationChange = (e) => {
        trip.location = e.currentTarget.getAttribute("data-value1")
        setTrip(trip);
        debugger;
        setDisplayLocationForm(<div></div>);
    }

    const handleStartDateChange = (newValue) => {

        if (dayjs(newValue).isAfter(dayjs(endDate))){
            document.getElementById("showDate").innerText = "Start date cannot be later than end date.";
        }
        else if(dayjs(newValue).isBefore(dayjs(endDate))){

            document.getElementById("showDate").innerText = `${dayjs(newValue).format('MM/DD/YYYY')} to ${dayjs(endDate).format('MM/DD/YYYY')}`;
            setStartDate(newValue);
        }
        else{
            setStartDate(newValue);
        }
    }
    const handleEndDateChange = (newValue) => {

        if (dayjs(newValue).isBefore(dayjs(startDate))){
            document.getElementById("showDate").innerText = "End date cannot be earlier than start date."
        }
        else if (dayjs(newValue).isAfter(dayjs(startDate))){

            document.getElementById("showDate").innerText = `${dayjs(startDate).format('MM/DD/YYYY')} to ${dayjs(newValue).format('MM/DD/YYYY')}`;
            setEndDate(newValue);
        }
        else{
            setEndDate(newValue);
        }
    }
    const saveDatesChange = () => {

        trip.startDate = startDate;
        trip.endDate = endDate;

        setTrip(trip);

        document.getElementById("showDate").innerHTML = `Saved! </br> New Dates:  ${dayjs(startDate).format('MM/DD/YYYY')} to ${dayjs(endDate).format('MM/DD/YYYY')}`;
        
    }
    return (
        <>
            <Typography variant="h4" style={{margin:15}}>{trip.location}</Typography>
            <Divider light />
            <div style={{margin:15, marginTop:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Dates
                </Typography>
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
                <Button size="small" variant="contained" sx={{backgroundColor:"gray", ml:2}} onClick={saveDatesChange}>Save</Button>
                <Typography id="showDate" style={{marginTop:15, fontWeight:'light', fontSize: 16}} variant="h5">
                    {dayjs(trip.startDate).format('MM/DD/YYYY')} to {dayjs(trip.endDate).format('MM/DD/YYYY')}
                </Typography>
            </div>
            <Divider light />
            <div style={{margin:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Location -
                    <Button size="small" variant="contained" sx={{backgroundColor:"gray", ml:2}} onClick={showLocationForm}>Edit</Button>
                </Typography>
                <Typography variant="h4" sx={{fontSize:30}}>{trip.location}</Typography>
                {displayLocationForm}
            </div>
            <Divider light />
            <div style={{margin:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Crew
                    <Button size="small" variant="contained" sx={{backgroundColor:"gray", ml:2}} onClick={showCrewForm}>Add</Button>
                </Typography>
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
                {displayCrewForm}
            </div>
            <Divider light />
            <div style={{margin:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Gear
                    <Button size="small" variant="contained" sx={{backgroundColor:"gray", ml:2}}>Add</Button>
                </Typography>
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
            </div>
        </>
    )
        
        
}