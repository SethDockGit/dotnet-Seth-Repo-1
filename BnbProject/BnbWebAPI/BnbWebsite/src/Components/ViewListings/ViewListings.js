import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import dayjs from "dayjs";
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import TextField from "@mui/material/TextField";
import OutlinedInput from '@mui/material/OutlinedInput';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import ListItemText from '@mui/material/ListItemText';
import Select from '@mui/material/Select';
import Checkbox from '@mui/material/Checkbox';

export default function ViewListings(){

const api = `https://localhost:44305`;

const testAmenities = [
    "hot tub", "grill", "pool table"
];

const startListings = [];

const [checkin, setCheckin] = useState('');
const [checkout, setCheckout] = useState('');
const [minRate, setMinRate] = useState(0);
const [maxRate, setMaxRate] = useState(Number.MAX_VALUE);
const [listings, setListings] = useState(startListings);
const [listingsLoaded, setListingsLoaded] = useState(false);
const [listingsMessage, setListingsMessage] = useState();
const [amenities, setAmenities] = useState([]);
const [failApplyFilters, setFailApplyFilters] = useState(false);
const [failMessage, setFailMessage] = useState('');

const getListings = () => {

    fetch(`${api}/bnb/listings`)
        .then((response) => response.json())
        .then((data) => {

            setListingsMessage(data.message);
            setListings(data.listings);
            console.log(data);
        })
        .then(() => {
            setListingsLoaded(true);
        });
}
getListings();

const showListings = () => {

    return listings.map(function(val, index) {
        debugger;
        return(

            <Grid item xs={2} key={index}>
                <Card sx={{minWidth:280, margin:3}}>
                    <CardContent>
                        {/*pic goes here*/}
                        <Grid container>
                            <Grid item xs={8}>
                                <Typography sx={{ fontSize: 20 }}>
                                  {val.title}
                                </Typography>
                                <Typography variant="body1" color="text.secondary">
                                  {val.location}
                                </Typography>
                            </Grid>
                            <Grid item xs={4}>
                                <Typography variant="body1" sx={{mt:1}}>
                                  ${val.rate}/Night
                                </Typography>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
            </Grid>
        )
    });
}
const handleCheckinChange = (newValue) => {
    setCheckin(newValue);
    //**add error messages for erroneous date ranges!
}
const handleCheckoutChange = (newValue) => {
    setCheckout(newValue);
        //**add error messages for erroneous date ranges!
}
const handleMinRateChange = (e) => {
    setMinRate(e.target.value);
}
const handleMaxRateChange = (e) => {
    setMaxRate(e.target.value);
}
const handleAmenitiesChange = (e) => {
    const {
        target: {value},
    } = e;
    setAmenities(
        typeof value === 'string' ? value.split(',') : value,
    );
};
const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 250,
      },
    },
  };
const applyFilters = () => {
    
    if(isNaN(minRate) || isNaN(maxRate))
    {
        setFailApplyFilters(true);
        setFailMessage("Min and Max rates must be numbers or decimals.");
    }
    else{

        setFailApplyFilters(false);

        var rateFiltered = listings.filter(l => l.rate < maxRate && l.rate > minRate);

        var dateFiltered = rateFiltered;

        for(let i=0; i <listings.length; i++){

            for(let j=0; j < listings[i].stays.length; j++){
                 
                if(dayjs(checkin).isBetween(dayjs(listings[i].stays[j].startDate), dayjs(listings[i].stays[j].endDate), 'day', '[]')){
                    
                    dateFiltered = rateFiltered.filter(l => l.id != listings[i].id);
                }
                else if(dayjs(checkout).isBetween(dayjs(listings[i].stays[j].startDate), dayjs(listings[i].stays[j].endDate), 'day', '[]')){

                    dateFiltered = rateFiltered.filter(l => l.id != listings[i].id);
                }
            }
        };    
        
        setListings(dateFiltered);
    }
}
const showFailMessage = () => {

    return (
        <div>
        {
        failApplyFilters &&   
            <div style={{margin:'auto'}}>
                <Typography color="red" variant="caption">{failMessage}</Typography>
            </div>    
        }
        </div>
    )
}
    return(

        <div>
            <Grid container sx={{mt:5, ml:4, mb:2, alignItems: 'center'}}>
                <Grid item xs={1.5}>
                    <Typography variant="h4">{listingsMessage}</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography variant="h4" sx={{color:'gray'}}>Filters:</Typography>
                </Grid>
                <Grid item xs={3.5}>
                    <Typography variant='h6'>Availability</Typography>
                    <DesktopDatePicker
                        label="Check-in"
                        inputFormat="MM/DD/YYYY"
                        value={checkin}
                        onChange={handleCheckinChange}
                        />
                    <DesktopDatePicker
                        label="Check-out"
                        inputFormat="MM/DD/YYYY"
                        value={checkout}
                        onChange={handleCheckoutChange}
                        />
                </Grid>
                <Grid item xs={2.5}>
                    <Typography variant='h6'>Rate($)</Typography>
                    <TextField sx={{maxWidth:150}} placeholder="Min Rate" onChange={handleMinRateChange}/>
                    <TextField sx={{maxWidth:150}} placeholder="Max Rate" onChange={handleMaxRateChange}/>
                    {showFailMessage()} 
                </Grid>
                <Grid item xs={2}>
                    <Typography variant='h6'>Amenities</Typography>
                    <FormControl sx={{ width: 300 }}>
                    <InputLabel id="amenities">Amenities</InputLabel>
                        <Select
                          labelId="amenities-checkbox-label"
                          id="amenities-checkbox"
                          multiple
                          value={amenities}
                          onChange={handleAmenitiesChange}
                          input={<OutlinedInput label="Tag" />}
                          renderValue={(selected) => selected.join(', ')}
                          MenuProps={MenuProps}
                        >
                          {testAmenities.map((val) => (
                            <MenuItem key={val} value={val}>
                              <Checkbox checked={amenities.indexOf(val) > -1} />
                              <ListItemText primary={val} />
                            </MenuItem>
                          ))}
                        </Select>
                </FormControl>
                </Grid>
                <Grid item xs={1.5}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, mt:2, backgroundColor:"lightsalmon", ml:2}} onClick={applyFilters}>Apply</Button>    
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                {showListings()}
            </Grid>
        </div>
    )
}