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
import LinkModal from "../Subcomponents/LinkModal/LinkModal";
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";
import { useNavigate } from 'react-router-dom';
import dayjs from "dayjs";
import ImageUpload from "../Subcomponents/ImageUpload/ImageUpload";
import ListingImages from "../Subcomponents/ListingImages/ListingImages";
import WarningModal from "../WarningModal/WarningModal";
import Error from "../Subcomponents/Error/Error";

export default function EditListing(){

const api = `https://localhost:44305`;

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
const [editModalOpen, setEditModalOpen] = useState(false);
const [deleteModalOpen, setDeleteModalOpen] = useState(false);
const [warningModalOpen, setWarningModalOpen] = useState(false);
const [files, setFiles] = useState([]);
const [pictures, setPictures] = useState([]);
const [deletePicIds, setDeletePicIds] = useState([]);
const [finishDeletePics, setFinishDeletePics] = useState(false);
const [finishAddFiles, setFinishAddFiles] = useState(false);
const navigate = useNavigate();

const reRoute = () => {
    let now = String(dayjs());
    document.cookie = `id=;expires=${now}UTC;path=/`;
    //this should overwrite any cookie so that it expires.
    navigate("/user/login");
}

const getUser = (userId) => {

    fetch(`${api}/bnb/user/${userId}`)
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

        var elements = document.cookie.split('=');
        var userId = Number(elements[1]);

        if(!isNaN(id)){
            getUser(userId);
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
        setPictures(data.listing.pictures);
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
const AvailableAmenities = () => {

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
const ListingAmenities = () => {
    
    return listingAmenities.map(function(val, index){
        return(      
            <ListItem key={index}>
                {val}
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
const handleClickSaveListing = () => {

    var fail = false;

    if(files.length != 0){

        for(let i = 0; i < files.length; i++){
    
            var file = files[i].file;
            var fileType = file['type'];
            const validImageTypes = ['image/jpeg', 'image/png'];
    
            if(!validImageTypes.includes(fileType)){
                fail = true;
            }
        }
    }

    var rateNumber = parseFloat(rate);

    if (fail) {
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

                if(data.success){

                    if(deletePicIds.length != 0){

                        fetch(`${api}/bnb/deletePicIds`, {
                            method: 'POST',
                            body: JSON.stringify(deletePicIds),
                            headers: {
                                'Content-Type': 'application/json'
                            }
                            })
                            .then((response) => response.json())
                            .then((data) => {
                                console.log(data);   
                                setFinishDeletePics(true);         
                            });
                    }
                    else{
                        setFinishDeletePics(true);
                    }
                    if(!!files){

                        for(let i =0; i < files.length; i++){
            
                            var form = new FormData();
                            form.append('file', files[i].file)
            
                            fetch(`${api}/bnb/file/${listing.id}`, {
                                method: 'POST',
                                body: form,
                                })
                                .then((response) => response.json())
                                .then((data) => {
                                    console.log(data);
                                    setFinishAddFiles(true);
                                }); 
                        }
                    }
                    else{
                        setFinishAddFiles(true);
                    }
                }
            });
        }  
}
if(finishAddFiles && finishDeletePics){

    fetch(`${api}/bnb/user/${user.id}`)
    .then((response) => response.json())
    .then((data) => {
        setUser(data.user);
        setEditModalOpen(true);
    });
}
const cancelEditListing = () => {
    navigate("/mystuff");
}
const handleClickRemoveFile = (e) => {

    const value1 = e.currentTarget.getAttribute("data-value1");

    var newFiles = files.filter(f => f.id != value1);

    setFiles(newFiles);
}
const handleClickRemovePic = (e) => {

    const value1 = e.currentTarget.getAttribute("data-value1");
 
    var tempPics = pictures.filter(p => p.id != value1);

    setPictures(tempPics);
    setDeletePicIds([...deletePicIds, Number(value1)]);
}
const handleClickDeleteListing = () => {

    setWarningModalOpen(false);

    fetch(`${api}/bnb/deletelisting/${id}/${user.id}`, {
        method: 'DELETE' 
    })
        .then((response) => response.json())
        .then((data) => {
            console.log(data);

            if(data.success){

                setUser(data.user);
                setDeleteModalOpen(true);
            }
        });
}

    return(

        <div>
            {listingLoaded && amenitiesLoaded && userLoaded &&
            <div>
                <Typography variant="h2" sx={{justifyContent: 'center', display: 'flex', margin:2, fontSize:50}}>Edit Your Listing...</Typography>

                <Divider sx={{backgroundColor:'peachpuff'}}/>

                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid item xs={3}>
                        <ListingImages pictures={pictures} handleClickRemovePic={handleClickRemovePic}/>
                    </Grid>

                    <Grid item xs={3}>
                        <ImageUpload files={files} setFiles={setFiles} handleClickRemoveFile={handleClickRemoveFile}/>
                    </Grid>
                </Grid>

                <Divider sx={{backgroundColor:'peachpuff'}}/>

                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid item xs={2}>
                        <Typography sx={{mt:2}} variant='h6'>Title: {title}</Typography>
                        <TextField sx={{mb:2}} inputProps={{ maxLength: 28 }}
                        placeholder='New Title' onChange={handleTitleChange}/>
                    </Grid>
                    <Grid item xs={2}>
                        <Typography sx={{mt:2}} variant='h6'>Nightly Rate: ${rate}</Typography>
                        <TextField sx={{mb:2}} inputProps={{ maxLength: 6 }} placeholder='New Rate' onChange={handleRateChange}/>
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
                                    {AvailableAmenities()}
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
                            <ListingAmenities/>
                        </List>
                    </Grid>
                </Grid>
    
                <Divider sx={{backgroundColor:'peachpuff'}}/>
    
                <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                    <Grid item xs={4}/>
                    <Grid item xs={1}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "gray"}, backgroundColor:'lightgray', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={cancelEditListing}>Cancel</Button>
                    </Grid>
                    <Grid item xs={1}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, backgroundColor:'lightsalmon', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={handleClickSaveListing}>Save Changes</Button>
                    </Grid>
                    <Grid item xs={1.25}>
                        <Button variant="contained" sx={{":hover": {
                        bgcolor: "darkred"}, backgroundColor:'red', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={() => setWarningModalOpen(true)}>Delete Listing</Button>
                    </Grid>
                    <Grid item xs={4}>
                        <Error message={failMessage} bool={failSaveListing}/> 
                    </Grid>

                    <WarningModal 
                    modalOpen={warningModalOpen}
                    modalClose={() => setWarningModalOpen(false)}
                    message={"Are you sure you want to delete this listing?"}
                    handleClickDeleteListing={handleClickDeleteListing}
                    />
                    <LinkModal 
                    modalOpen={editModalOpen} 
                    modalClose={() => navigate('/mystuff')} 
                    message={"Changes Saved"} 
                    messageTwo={"Back to MyStuff"}/>
                    <LinkModal 
                    modalOpen={deleteModalOpen} 
                    modalClose={() => navigate('/mystuff')} 
                    message={"Listing Deleted"} 
                    messageTwo={"Back to MyStuff"}/>

                </Grid>
            </div>
            }

        </div>
    )
}