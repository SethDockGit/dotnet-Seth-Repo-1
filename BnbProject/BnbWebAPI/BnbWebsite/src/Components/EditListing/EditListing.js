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
import { useParams } from "react-router-dom";
import Modal from '@mui/material/Modal';
import { Link } from "react-router-dom";
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";
import { useNavigate } from 'react-router-dom';
import dayjs from "dayjs";

export default function EditListing(){

const api = `https://localhost:44305`;

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    borderRadius:3,
    boxShadow: 24,
    p: 4,
  };

const {user, setUser} = useContext(UserContext);
const {id} = useParams();
const [listing, setListing] = useState();
const [title, setTitle] = useState('');
const [rate, setRate] = useState();
const [location, setLocation] = useState('');
const [description, setDescription] = useState('');
const [availableAmenities, setAvailableAmenities] = useState();
const [listingAmenities, setListingAmenities] = useState([]);
const [customAmenity, setCustomAmenity] = useState('');
const [failSaveListing, setFailSaveListing] = useState(false);
const [failMessage, setFailMessage] = useState('');
const [listingLoaded, setListingLoaded] = useState(false);
const [amenitiesLoaded, setAmenitiesLoaded] = useState(false);
const [modalOpen, setModalOpen] = useState(false);
const navigate = useNavigate();

const getListing = () => {

    fetch(`${api}/bnb/listing${id}`)
    .then((response) => response.json())
    .then((data) => {
  
        setListing(data.listing);
        setTitle(data.listing.title);
        setRate(data.listing.rate);
        setLocation(data.listing.location);
        setDescription(data.listing.description);
        setListingAmenities(data.listing.amenities);
        console.log(data);

        if(user.id != data.listing.hostId){
            navigate("/listings"); //I think this is causing my error. How to fix?
        }
    })
    .then(() => {
        setListingLoaded(true);
    });
}
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

if(!listingLoaded){
    getListing();
}
if(!amenitiesLoaded){
    getAmenities();
}
const reRoute = () => {
    navigate("/user/login");
}
if(user == null || dayjs().isAfter(dayjs(user.logTime).add(6, 'hour'))){
    reRoute();
}


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

        //*get hostID thru context or pass down from login? 0 is OK for now but need to change
        var APIRequest = {
            Id: listing.id,
            HostId: user.id,  //can get from listing or from context/user
            Title: title,
            Rate: Number(rate),
            Location: location,
            Description: description,
            Amenities: listingAmenities,
        };

        fetch(`${api}/bnb/editlisting`, {
            method: 'POST',
            body: JSON.stringify(APIRequest),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);

                setModalOpen(true);
            });
        }   
}
const cancelEditListing = () => {
    //*will navigate back to MyStuff
}
    return(

        <div>
            {listingLoaded && amenitiesLoaded &&
            <div>
                <Typography variant="h2" sx={{justifyContent: 'center', display: 'flex', margin:2, fontSize:50}}>Edit Listing...</Typography>
                {/*here go the pics*/}
                <Divider sx={{backgroundColor:'peachpuff'}}/>
                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid xs={2}>
                        <Typography sx={{mt:2}} variant='h6'>Title: {title}</Typography>
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
                        <Typography sx={{mt:2}} variant='h6'>Description: </Typography>
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
                                    value={""}
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
                            maxHeight: 200,
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

                    <Modal
                      open={modalOpen}
                      onClose={() => setModalOpen(false)}
                    >
                        <Box sx={style}>
                            <Typography variant="h6">Changes saved!</Typography>
                            <Link style={{ textDecoration: 'none' }} to={`/listings/${listing.id}`}>
                                <Button>Go to your listing</Button>
                            </Link>
                        </Box>
                    </Modal>
                </Grid>
            </div>
            }

        </div>
    )
}