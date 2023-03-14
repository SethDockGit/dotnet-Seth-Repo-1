import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import dayjs from "dayjs";
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import TextField from "@mui/material/TextField";

export default function ViewListings(){

const api = `https://localhost:44305`;

const startListings = [];

const [checkin, setCheckin] = useState('');
const [checkout, setCheckout] = useState('');
const [minPrice, setMinPrice] = useState(0);
const [maxPrice, setMaxPrice] = useState(500);
const [listings, setListings] = useState(startListings);
const [listingsMessage, setListingsMessage] = useState();

const getListings = () => {

    fetch(`${api}/bnb/listings`)
        .then((response) => response.json())
        .then((data) => {

            setListingsMessage(data.message);
            setListings(data.listings);
            console.log(data);
        });
}
getListings();

const showListings = () => {

    return listings.map(function(val, index) {

        return(

            <Grid item xs={2} key={index}>
                <Card sx={{ maxWidth: 300, margin:3}}>
                    <CardContent>
                      <Typography sx={{ fontSize: 14 }}>
                        {val.title}
                      </Typography>
                      <Typography variant="body1" color="text.secondary">
                        {val.location}
                      </Typography>
                      <Typography sx={{ mb: 1.5 }}>
                        {val.price}
                      </Typography>
                      <Typography variant="body2">
                      </Typography>
                    </CardContent>
                </Card>
            </Grid>
        )
    });
}
const handleCheckinChange = (newValue) => {
    setCheckin(newValue);
}
const handleCheckoutChange = (newValue) => {
    setCheckout(newValue);
}

    return(

        <div>
            <Grid container sx={{mt:5, ml:4, mb:2}}>
                <Grid item xs={2}>
                    <Typography variant="h4">{listingsMessage}</Typography>
                </Grid>
                <Grid item xs={.5}>
                    <Typography variant="h5" sx={{color:'gray'}}>Filters:</Typography>
                </Grid>
                <Grid item xs={3.5}>
                    <DesktopDatePicker
                        label="Check-in"
                        inputFormat="MM/DD/YYYY"
                        value={checkin}
                        onChange={handleCheckinChange}
                        renderInput={(params) => <TextField {...params} error={false} />}
                        />
                    <DesktopDatePicker
                        label="Check-out"
                        inputFormat="MM/DD/YYYY"
                        value={checkout}
                        onChange={handleCheckoutChange}
                        renderInput={(params) => <TextField {...params} error={false} />}
                        />
                </Grid>
                <Grid item xs={2}>
                    {/*Price header. Min and max dropdown select textfields. Just try it with the MUI select*/}
                </Grid>
                <Grid item xs={2}>
                    {/*Amenities dropdown with checkmarks*/}
                </Grid>
                <Grid item xs={2}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, mt:2, backgroundColor:"lightsalmon"}}>Apply</Button>    
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                {showListings()}
            </Grid>
        </div>
    )
}