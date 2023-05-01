import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
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
import ListingsCard from "../Subcomponents/ListingsCard/ListingsCard";
import Error from "../Subcomponents/Error/Error";


export default function ViewListings(){

const api = `https://localhost:44305`;

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


const [listings, setListings] = useState();
const [unfiltered, setUnfiltered] = useState([]);
const [checkin, setCheckin] = useState('');
const [checkout, setCheckout] = useState('');
const [minRate, setMinRate] = useState(0);
const [maxRate, setMaxRate] = useState(Number.MAX_VALUE);
const [amenities, setAmenities] = useState([]);
const [selectedAmenities, setSelectedAmenities] = useState([]);
const [failRateFilters, setFailRateFilters] = useState(false);
const [failDateFilters, setFailDateFilters] = useState(false);
const [rateMessage, setRateMessage] = useState('');
const [dateMessage, setDateMessage] = useState('');


useEffect(() => {

    fetch(`${api}/bnb/listings`)
    .then((response) => response.json())
    .then((data) => {
    
        setListings(data.listings);
        setUnfiltered(data.listings);
        console.log(data);
    })
}, []);


useEffect(() => {

    fetch(`${api}/bnb/amenities`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
        setAmenities(data.amenities);
    })
}, []);


const Listings = () => {

    return listings.map(function(val, index) {
        
        return(
            <ListingsCard listing={val} key={index}/>
        )
    });
}
const handleCheckinChange = (newValue) => {
    setCheckin(newValue);
}
const handleCheckoutChange = (newValue) => {
    setCheckout(newValue);
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
    setSelectedAmenities(
        typeof value === 'string' ? value.split(',') : value,
    );
};
const applyFilters = () => {

    setFailDateFilters(false);
    setFailRateFilters(false);    
    
    if(isNaN(minRate) || isNaN(maxRate))
    {
        setFailRateFilters(true);
        setRateMessage("Min and Max rates must be numbers or decimals.");
    }
    else if(dayjs(checkin).isAfter(dayjs(checkout))){

        setFailDateFilters(true);
        setDateMessage("Check-in date must be before check-out date.");
    }
    else if(dayjs(checkin).isBefore(dayjs()) || dayjs(checkin).isBefore(dayjs())){

        setFailDateFilters(true);
        setDateMessage("Check-in and check-out dates must be future dates.");
    }
    else if((checkin == "" && checkout != "") || (checkin == null && checkout != null)){
        setFailDateFilters(true);
        setDateMessage("Please select a check-in date.");
    }
    else if((checkout == "" && checkin != "") || (checkout == null && checkin != null)){
        setFailDateFilters(true);
        setDateMessage("Please select a check-out date.");
    }
    else{

        var filtered = unfiltered.map(function(val) {
            return(
                {
                    listing: val,
                    display: Boolean(true)
                }
            )
        });
        
        filtered = filtered.filter(l => l.listing.rate < maxRate && l.listing.rate > minRate);

        for(let i=0; i < filtered.length; i++){

            for(let j=0; j < selectedAmenities.length; j++){

                if(!filtered[i].listing.amenities.includes(selectedAmenities[j])){

                    filtered[i].display = false;
                }
            }
        }


        if(checkin != "" && checkin != null && checkout != "" && checkout != null){

            for(let i=0; i < filtered.length; i++){

                for(let j=0; j < filtered[i].listing.stays.length; j++){

                    if(dayjs(checkin).isBetween(dayjs(filtered[i].listing.stays[j].startDate), dayjs(filtered[i].listing.stays[j].endDate), 'day', '[]')){

                        filtered[i].display = false;
                    }
                    else if(dayjs(checkout).isBetween(dayjs(filtered[i].listing.stays[j].startDate), dayjs(filtered[i].listing.stays[j].endDate), 'day', '[]')){
                        
                        filtered[i].display = false;
                    }
                    else if(dayjs(filtered[i].listing.stays[j].startDate).isBetween(dayjs(checkin), dayjs(checkout), 'day', '[]')){
                        
                        filtered[i].display = false;
                    }
                    else if(dayjs(filtered[i].listing.stays[j].endDate).isBetween(dayjs(checkin), dayjs(checkout), 'day', '[]')){
                        
                        filtered[i].display = false;
                    }
                }             
            }    
        }

        filtered = filtered.filter(l => l.display != false);

        filtered = filtered.map(function(val) {
            return(
                val.listing
            )
        });

        setListings(filtered);
    }
}

    return(
        
        <div>
            {(!!listings && !!amenities) && 
            <div>
            <Grid container sx={{mt:5, ml:4, mb:2, alignItems: 'center'}}>
                <Grid item xs={1.5}>
                    <Typography variant="h4">{listings.length} Listings</Typography>
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
                        <Error message={dateMessage} bool={failDateFilters}/> 
                </Grid>
                <Grid item xs={2.5}>
                    <Typography variant='h6'>Rate($)</Typography>
                    <TextField sx={{maxWidth:150}} placeholder="Min Rate" onChange={handleMinRateChange}/>
                    <TextField sx={{maxWidth:150}} placeholder="Max Rate" onChange={handleMaxRateChange}/>
                    <Error message={rateMessage} bool={failRateFilters}/> 
                </Grid>
                <Grid item xs={2}>
                    <Typography variant='h6'>Amenities</Typography>
                    <FormControl sx={{ width: 300 }}>
                        <InputLabel id="amenities">Amenities</InputLabel>
                        <Select
                          labelId="amenities-checkbox-label"
                          id="amenities-checkbox"
                          multiple
                          value={selectedAmenities}
                          onChange={handleAmenitiesChange}
                          input={<OutlinedInput label="Tag" />}
                          renderValue={(selected) => selected.join(', ')}
                          MenuProps={MenuProps}
                        >
                          {amenities.map((val) => (
                            <MenuItem key={val} value={val}>
                              <Checkbox checked={selectedAmenities.indexOf(val) > -1} />
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
                {Listings()}
            </Grid>
            </div>}
        </div>
    )
}