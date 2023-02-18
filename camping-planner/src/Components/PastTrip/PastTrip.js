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
import { useNavigate } from "react-router-dom";


export default function Trip(){

    const {trips, setTrips} = useContext(TripsContext);
    const {id} = useParams();
    const [trip, setTrip] = useState(trips.find(t => t.id == id));
    const navigate = useNavigate();

    const showCrewList = () => {

        return trip.crew.map(function(val, index){
            return( 
                <ListItem key={index}>
                    {val.name} • {val.phone} • {val.email}
                </ListItem>                
            )        
        })
    }
    const showGear = () => {  
        return trip.gear.map(function(val, index){
            return(
                <TableRow key={index}>
                    <TableCell size="small">{val.name}</TableCell>
                    <TableCell size="small" align="center">{val.quantity}</TableCell>
                    <TableCell size="small" align="right">
                    </TableCell>
                </TableRow>
            )
        })
    }
    const deleteTrip = () => {

        setTrips(trips.filter(t => t.id != trip.id))
        navigate("/trips");
    }
    return (
        <>
            <Typography variant="h4" style={{margin:15}}>{trip.location}</Typography>
            <Divider light />
                <div style={{margin:15, marginTop:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Dates
                </Typography>
                <Typography style={{margin:15, fontWeight:'light', fontSize: 16}} variant="h5">
                    {dayjs(trip.startDate).format('MM/DD/YYYY')} to {dayjs(trip.endDate).format('MM/DD/YYYY')}
                </Typography>
            </div>
            <Divider light />
            <div style={{margin:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Location -
                </Typography>
                <Typography variant="h4" sx={{fontSize:30}}>{trip.location}</Typography>
            </div>
            <Divider light />
            <div style={{margin:15}}>
                <Typography variant="h4" sx={{fontSize:30}}>
                    Crew
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
            </div>
            <Divider light />
            <div style={{margin:15}}>
            <Typography variant="h4" sx={{fontSize:30}}>
                Gear
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
            <Button style={{margin:15}} variant="contained" sx={{backgroundColor:"gray"}} onClick={deleteTrip}>Delete Trip</Button>
        </>
    )
        
        
}