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
import { ListingsContext } from "../../Contexts/ListingsContext";
import { useContext } from "react";

export default function CreateListing(){

const api = `https://localhost:44305`;

const hostId = 1; //THIS WILL NEED TO BE SET AND PASSED IN AT LOGIN. NEEDED TO FINALIZE LISTING

//const {listings, setListings} = useContext(ListingsContext);
const [title, setTitle] = useState('');
const [rate, setRate] = useState();
const [location, setLocation] = useState('');
const [description, setDescription] = useState('');
const [availableAmenities, setAvailableAmenities] = useState([]);
const [amenitiesLoaded, setAmenitiesLoaded] = useState(false);
const [listingAmenities, setListingAmenities] = useState([]);
const [customAmenity, setCustomAmenity] = useState('');
const [failMessage, setFailMessage] = useState('');
const [failCreateListing, setFailCreateListing] = useState(false);

const getAmenities = () => {

    fetch(`${api}/bnb/amenities`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
        setAvailableAmenities(data.amenities);
    })
    .then(() =>{
        setAmenitiesLoaded(true);
    });
}

const stopRerender = () => {

    !amenitiesLoaded && getAmenities();
}

stopRerender();

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
const handleListingChange = () => {
     
    let rateNumber = parseFloat(rate);
    
    if (isNaN(rateNumber)){
        setFailCreateListing(true);

        setFailMessage("Error: Rate must be a number or decimal.");
    } 
    else if (title == "" || location == "" || description == ""){
        setFailCreateListing(true);

        setFailMessage("Error: One or more fields were left blank.");
    }
    else {

        let stayArray = [];

        //*get hostID thru context or pass down from login? 0 is OK for now but need to change
        var APIRequest = {
            Id: 0,
            HostId: hostId,
            Title: title,
            Rate: Number(rate),
            Location: location,
            Description: description,
            Amenities: listingAmenities,
        };

        //setListings([...listings, APIRequest]);

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
            //user will be navigated to MyStuff to see their listing
        }   
}
const showFailMessage = () => {

    return (
        <div>
        {
        failCreateListing &&   
            <div style={{margin:'auto'}}>
                <Typography color="red" variant="h6">{failMessage}</Typography>
            </div>    
        }
        </div>
    )
}
    return(

        <div>
            {amenitiesLoaded && 
            <div>
            <Typography variant="h4" sx={{justifyContent: 'center', display: 'flex', margin:2}}>Create Your Listing</Typography>
            {/*here go the pics*/}
            <Divider sx={{backgroundColor:'peachpuff'}}/>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Listing Title</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Title' onChange={handleTitleChange}/>
                </Grid>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Nightly Rate($)</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Rate' onChange={handleRateChange}/>
                </Grid>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Location</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Location' onChange={handleLocationChange}/>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={6}>
                    <Typography sx={{mt:2}} variant='h6'>Description
                    </Typography>
                    <TextField fullWidth multiline rows={6} sx={{justifyContent: 'center', display: 'flex', mb:2}} placeholder='Describe the property...' onChange={handleDescriptionChange}/>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Amenities</Typography>
                    <Box sx={{ maxWidth: 180 }}>
                        <FormControl fullWidth>
                            <InputLabel>Amenities</InputLabel>
                            <Select
                                id="amenity-select"
                                label="Amenities"
                                value={""}
                                onChange={handleClickAmenity}
                            >
                                {showAvailableAmenities()}
                            </Select>
                        </FormControl>
                     </Box>
                </Grid>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Add Custom Amenity</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Amenity' onChange={handleCustomAmenityChange}/>
                    <Button sx={{color:'lightsalmon'}} onClick={addCustomAmenity}>Add</Button>
                </Grid>
                <Grid item xs={2}>
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
                <Grid item xs={5}/>
                <Grid item xs={2}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, backgroundColor:'lightsalmon', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={handleListingChange}>Save</Button>
                </Grid>
                <Grid item xs={5}>
                    {showFailMessage()} 
                </Grid>
            </Grid>

            </div>
            }
            
        </div>

    )
}