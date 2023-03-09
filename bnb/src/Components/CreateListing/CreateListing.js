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

export default function CreateListing(){

let testAmenities = [
    "hot tub", "grill", "pool table"
]
const startListingAmenities = [];


const [title, setTitle] = useState('');
const [rate, setRate] = useState();
const [location, setLocation] = useState('');
const [description, setDescription] = useState('');
const [availableAmenities, setAvailableAmenities] = useState(testAmenities);
const [listingAmenities, setListingAmenities] = useState(startListingAmenities);
const [customAmenity, setCustomAmenity] = useState('');

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
    setListingAmenities(...listingAmenities, e.target.value);
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
    setListingAmenities(...listingAmenities, customAmenity);
}
const showListingAmenities = () => {
    debugger;
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

}
    return(

        <div>
            <Typography variant="h4" sx={{justifyContent: 'center', display: 'flex', margin:2}}>Create Your Listing</Typography>
            {/*here go the pics*/}
            <Divider sx={{backgroundColor:'peachpuff'}}/>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Listing Title</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Title' onChange={handleTitleChange}/>
                </Grid>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Nightly Rate</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Rate' onChange={handleRateChange}/>
                </Grid>
                <Grid xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Location</Typography>
                    <TextField sx={{mb:2}} placeholder='Enter Location' onChange={handleLocationChange}/>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid xs={6}>
                    <Typography sx={{mt:2}} variant='h6'>Description
                    </Typography>
                    <TextField fullWidth multiline rows={6} sx={{justifyContent: 'center', display: 'flex', mb:2}} placeholder='Describe the property...' onChange={handleDescriptionChange}/>
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

            <Grid container sx={{justifyContent: 'center', display: 'flex',
            alignItems: 'center', margin:2}}>
                <Button variant="contained" sx={{":hover": {
                bgcolor: "peachpuff"}, backgroundColor:'lightsalmon', m:2}} onClick={handleListingChange}>Save</Button>
            </Grid>
        </div>

    )
}