import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import TextField from "@mui/material/TextField";
import { useEffect, useState } from "react";
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
const [userLoaded, setUserLoaded] = useState(false);  
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
const [files, setFiles] = useState([]);
const navigate = useNavigate();

const reRoute = () => {
    let now = String(dayjs());
    document.cookie = `id=;expires=${now}UTC;path=/`;
    //this should overwrite any cookie so that it expires.
    navigate("/user/login");
}

const getUser = (id) => {

    fetch(`${api}/bnb/user/${id}`)
    .then((response) => response.json())
    .then((data) => {
        setUser(data.user);
    })
    .then(() => {
        setUserLoaded(true);
    });
}

const verifyLogin = () => {

    if(!user){
        //if user is null, parse the cookie. If there's no cookie, id will be NaN. So, either get user by Id if Id has value, or reroute to login.
        var elements = document.cookie.split('=');
        var id = Number(elements[1]);

        if(!isNaN(id)){
            getUser(id);
        }
        else{
            reRoute();
        }
    }
    else{
        if(dayjs().isAfter(dayjs(user.logTime).add(6, 'hour'))){
            reRoute();
        }
        else{ 
            setUserLoaded(true); 
        }
    } 
}
useEffect(() => {
    verifyLogin();
}, []);

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
useEffect(() => {
    getAmenities();
}, []);

const getListing = () => {

    fetch(`${api}/bnb/listing/${id}`)
    .then((response) => response.json())
    .then((data) => {
  
        setListing(data.listing);
        setTitle(data.listing.title);
        setRate(data.listing.rate);
        setLocation(data.listing.location);
        setDescription(data.listing.description);
        setListingAmenities(data.listing.amenities);
        console.log(data);

            if(!user || user.id != data.listing.hostId){
                navigate("/listings"); 
            }
        
    })
    .then(() => {
        setListingLoaded(true);
    });
}
useEffect(() => {
    getListing();
}, []);

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
const handleClickSaveListing = () => {

    var file = files[0];
    var fileType = file['type'];
    const validImageTypes = ['image/jpeg', 'image/png'];

    var rateNumber = parseFloat(rate);

    if (!validImageTypes.includes(fileType)) {
        setFailSaveListing(true);
        setFailMessage("Error: File type must be jpeg or png.");
        setFiles([]);
    }
    else if (isNaN(rateNumber)){
        setFailSaveListing(true);
        setFailMessage("Error: Rate must be a number or decimal.");
    } 
    else if (title == "" || location == "" || description == ""){
        setFailSaveListing(true);
        setFailMessage("Error: One or more fields were left blank.");
    }
    else {

        var APIRequest = {
            Id: listing.id,
            HostId: user.id,  
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

        if(!!files[0]){

            var data = new FormData()
            data.append('file', files[0])

            var APIRequest = {
                ImageFile: data,
                ListingId: id
            }
        
            fetch(`${api}/bnb/editfile/${id}`, {
                method: 'POST',
                body: APIRequest,
                })
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
    
                    //if(!data.success)
                    //show an error message, likely related to type of file.
                    //check what happens when you upload mp3
                }); 
        }
       
}
const cancelEditListing = () => {
    navigate("/mystuff");
}

const fileSelectedHandler = (e) => {
    setFiles([...files, ...e.target.files]);
}

const showPic = () => {

    var data = listing.picture;
    
    const Picture = ({ data }) => <img src={`data:image/jpeg;base64,${data}`} />
    
    return (

        (data != null)
        ? <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
            <Picture data={data}/>
          </Grid>
        : <div></div>
    )
}
    return(

        <div>
            {listingLoaded && amenitiesLoaded && userLoaded &&
            <div>
                <Typography variant="h2" sx={{justifyContent: 'center', display: 'flex', margin:2, fontSize:50}}>Edit Listing...</Typography>
                {showPic()}
                <Divider sx={{backgroundColor:'peachpuff'}}/>

                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <form>
                      <div><Typography variant="h6" sx={{mb:1}}>Select Image</Typography></div>
                      <input type="file" multiple onChange={fileSelectedHandler} />
                    </form>
                </Grid>

                <Divider sx={{backgroundColor:'peachpuff'}}/>

                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid item xs={2}>
                        <Typography sx={{mt:2}} variant='h6'>Title: {title}</Typography>
                        <TextField sx={{mb:2}} placeholder='New Title' onChange={handleTitleChange}/>
                    </Grid>
                    <Grid item xs={2}>
                        <Typography sx={{mt:2}} variant='h6'>Nightly Rate: ${rate}</Typography>
                        <TextField sx={{mb:2}} placeholder='New Rate' onChange={handleRateChange}/>
                    </Grid>
                    <Grid item xs={2}>
                        <Typography sx={{mt:2}} variant='h6'>Location: {location}</Typography>
                        <TextField sx={{mb:2}} placeholder='New Location' onChange={handleLocationChange}/>
                    </Grid>
                </Grid>
    
                <Divider sx={{backgroundColor:'peachpuff'}}/>
    
                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid item xs={6}>
                        <Typography sx={{mt:2}} variant='h6'>Description: </Typography>
                        <Typography variant='body1'>"{description}"</Typography>
                        <TextField fullWidth multiline rows={6} sx={{justifyContent: 'center', display: 'flex', mb:2}} placeholder='New Description...' onChange={handleDescriptionChange}/>
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
                            maxHeight: 200,
                            '& ul': { padding: 0 },
                            }}>
                            {showListingAmenities()}
                        </List>
                    </Grid>
                </Grid>
    
                <Divider sx={{backgroundColor:'peachpuff'}}/>
    
                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid item xs={5}/>
                    <Grid item xs={1}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "gray"}, backgroundColor:'lightgray', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={cancelEditListing}>Cancel</Button>
                    </Grid>
                    <Grid item xs={1}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, backgroundColor:'lightsalmon', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={handleClickSaveListing}>Save Changes</Button>
                    </Grid>
                    <Grid item xs={5}>
                        {showFailMessage()} 
                    </Grid>

                    <Modal
                      open={modalOpen}
                      onClose={() => navigate('/mystuff')}
                    >
                        <Box sx={style}>
                            <Typography variant="h6">Changes saved!</Typography>
                            <Link style={{ textDecoration: 'none' }} to={`/mystuff`}>
                                <Button>Back to MyStuff</Button>
                            </Link>
                        </Box>
                    </Modal>
                </Grid>
            </div>
            }

        </div>
    )
}