import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import TextField from "@mui/material/TextField";
import { useState } from "react";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import Button from "@mui/material/Button";
import ListItem from "@mui/material/ListItem";
import List from "@mui/material/List";

export default function EditListing(){

var testListing = {
    id: 0,
    title: "Cozy 2BR Cabin Up North",
    rate: 205,
    location: "Crosby, MN",
    description: "Come get away from it all in our sunny 2BR cabin on the lake. Paddle in the canoe or take a stroll around the woods. Pet friendly.",
    listingAmenities: ["firepit", "dishwasher", "laundry"] 
}
let testAmenities = [
    "hot tub", "grill", "pool table"
]
const api = '';

const [listing, setListing] = useState(testListing);
const [title, setTitle] = useState(listing.title);
const [rate, setRate] = useState(listing.rate);
const [location, setLocation] = useState(listing.location);
const [description, setDescription] = useState(listing.description);
const [availableAmenities, setAvailableAmenities] = useState(testAmenities);
const [listingAmenities, setListingAmenities] = useState(listing.listingAmenities);
const [customAmenity, setCustomAmenity] = useState('');
const [failSaveListing, setFailSaveListing] = useState(false);
const [failMessage, setFailMessage] = useState('');


const handleTitleChange = (e) => {
    setTitle(e.target.value);
}
const handleRateChange = (e) => {
    setRate(e.target.value);
}
const handleLocationChange = (e) => {
    setLocation(e.target.value);
}
const handleDescriptionChange = (e) => {
    setDescription(e.target.value);
}
const handleClickAmenity = (e) => {
    setListingAmenities([...listingAmenities, e.target.value]);
}
const showAvailableAmenities = () => {

    return availableAmenities.map(function(val, index){
        return(
            <MenuItem key={index} value={val}>{val}</MenuItem>
        )
    })
}
const handleCustomAmenityChange = (e) => {
    setCustomAmenity(e.target.value);
}
const addCustomAmenity = () => {
    setListingAmenities([...listingAmenities, customAmenity]);
}
const showListingAmenities = () => {
    
    return listingAmenities.map(function(val, index){
        return(      
            <ListItem key={index}>
                • {val}
                <Button type="button" onClick={handleClickRemoveAmenity} data-value1={val}>x</Button>
            </ListItem>           
        )        
    })
}
const handleClickRemoveAmenity = (e) => {

    const value1 = e.currentTarget.getAttribute("data-value1")
        
    var newAmenities = listingAmenities.filter(a => a != value1);

    setListingAmenities(newAmenities);
}
const showFailMessage = () => {

    return (
        <div>
        {
        failSaveListing &&   
            <div style={{margin:'auto'}}>
                <Typography color="red" variant="h6">{failMessage}</Typography>
            </div>    
        }
        </div>
    )
}
const handleListingChange = () => {

    let rateNumber = parseFloat(rate);
    
    if (isNaN(rateNumber)){
        setFailSaveListing(true);

        setFailMessage("Error: Rate must be a number or decimal.");
    } 
    else if (title == "" || location == "" || description == ""){
        setFailSaveListing(true);

        setFailMessage("Error: One or more fields were left blank.");
    }
    else {

        let stayArray = new Array();

        //*get hostID thru context or pass down from login? 0 is OK for now but need to change
        var APIRequest = {
            Id: 0,
            HostId: 0,
            Title: title,
            Location: location,
            Description: description,
            Amenities: listingAmenities,
            Stays: stayArray
        };

        fetch(`${api}/bnb/addlisting`, {
            method: 'POST',
            body: JSON.stringify(APIRequest),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
            });
            //*user will be navigated to MyStuff to see their listing
        }   
}
const cancelEditListing = () => {
    //*will navigate back to MyStuff
}
    return(

        <div>
            <Typography variant="h4" sx={{justifyContent: 'center', display: 'flex', margin:2}}>Edit Your Listing</Typography>
            {/*here go the pics*/}
            <Divider sx={{backgroundColor:'peachpuff'}}/>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid xs={2.5}>
                    <Typography sx={{mt:2}} variant='h6'>Listing Title: {title}</Typography>
                    <TextField sx={{mb:2}} placeholder='New Title' onChange={handleTitleChange}/>
                </Grid>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Nightly Rate: ${rate}</Typography>
                    <TextField sx={{mb:2}} placeholder='New Rate' onChange={handleRateChange}/>
                </Grid>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Location: {location}</Typography>
                    <TextField sx={{mb:2}} placeholder='New Location' onChange={handleLocationChange}/>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid xs={6}>
                    <Typography sx={{mt:2}} variant='h6'>Your Description: </Typography>
                    <Typography variant='body1'>"{description}"</Typography>
                    <TextField fullWidth multiline rows={6} sx={{justifyContent: 'center', display: 'flex', mb:2}} placeholder='New Description...' onChange={handleDescriptionChange}/>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Amenities</Typography>
                    <Box sx={{ maxWidth: 180 }}>
                        <FormControl fullWidth>
                            <InputLabel>Amenities</InputLabel>
                            <Select
                                id="amenity-select"
                                label="Amenities"
                                onChange={handleClickAmenity}
                            >
                                {showAvailableAmenities()}
                            </Select>
                        </FormControl>
                     </Box>
                </Grid>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Add Custom Amenity</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Amenity' onChange={handleCustomAmenityChange}/>
                    <Button sx={{color:'lightsalmon'}} onClick={addCustomAmenity}>Add</Button>
                </Grid>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Your Amenities:</Typography>
                    <List sx={{
                        width: '100%',
                        maxWidth: 500,
                        bgcolor: 'background.white',
                        position: 'relative',
                        overflow: 'auto',
                        maxHeight: 300,
                        '& ul': { padding: 0 },
                        }}>
                        {showListingAmenities()}
                    </List>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid xs={5}/>
                <Grid xs={1}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "gray"}, backgroundColor:'lightgray', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={cancelEditListing}>Cancel</Button>
                </Grid>
                <Grid xs={1}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, backgroundColor:'lightsalmon', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={handleListingChange}>Save Changes</Button>
                </Grid>
                <Grid xs={5}>
                    {showFailMessage()} 
                </Grid>
            </Grid>
        </div>
    )
}