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
import dayjs from "dayjs";
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";
import { useNavigate } from 'react-router-dom';
import { useEffect } from "react";
import ImageUpload from "../Subcomponents/ImageUpload/ImageUpload";
import Error from "../Subcomponents/Error/Error";
import LinkModal from "../Subcomponents/LinkModal/LinkModal";

export default function CreateListing(){

const api = `https://localhost:44305`;

const {user, setUser} = useContext(UserContext);
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
const [modalOpen, setModalOpen] = useState(false);
const [files, setFiles] = useState([]);
const [finishAddFiles, setFinishAddFiles] = useState(false);
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
const handleClickCreateListing = () => {
     
    var fail = false;

    for(let i = 0; i < files.length; i++){

        var file = files[i].file;
        var fileType = file['type'];
        const validImageTypes = ['image/jpeg', 'image/png'];

        if(!validImageTypes.includes(fileType)){
            fail = true;
        }
    }

    var rateNumber = parseFloat(rate);

    if (fail) {
        setFailCreateListing(true);
        setFailMessage("Error: File type must be jpeg or png.");
        setFiles([]);
    }
    else if (isNaN(rateNumber)){
        setFailCreateListing(true);
        setFailMessage("Error: Rate must be a number or decimal.");
    } 
    else if (title == "" || location == "" || description == ""){
        setFailCreateListing(true);
        setFailMessage("Error: One or more fields were left blank.");
    }
    else {

        var APIRequest = {
            Id: 0,
            HostId: user.id,
            Title: title,
            Rate: Number(rate),
            Location: location,
            Description: description,
            Amenities: listingAmenities,      
        };
        debugger;
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

                if(data.success){

                    if(!!files){

                        for(let i =0; i < files.length; i++){
            
                            var form = new FormData();
                            form.append('file', files[i].file)
            
                            fetch(`${api}/bnb/file/${data.listing.id}`, {
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
                setModalOpen(true);
            });
    }   
}

useEffect(() => {
    if(finishAddFiles){
    
        fetch(`${api}/bnb/user/${user.id}`)
        .then((response) => response.json())
        .then((data) => {
            setUser(data.user);
            setModalOpen(true);
        });
    }
}, [finishAddFiles]);

const cancelCreateListing = () => {
    navigate("/mystuff");
}
const handleClickRemoveFile = (e) => {

    const value1 = e.currentTarget.getAttribute("data-value1");

    var newFiles = files.filter(f => f.id != value1);

    setFiles(newFiles);
}

    return(

        <div>
            {amenitiesLoaded && 
            <div>
            <Typography variant="h2" sx={{justifyContent: 'center', display: 'flex', margin:2, fontSize:50}}>New Listing...</Typography>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <ImageUpload files={files} setFiles={setFiles} handleClickRemoveFile={handleClickRemoveFile}/>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Listing Title</Typography>
                    <TextField sx={{mb:2}} inputProps={{ maxLength: 28 }}
                    placeholder='Enter Title' onChange={handleTitleChange}/>
                </Grid>
                <Grid item xs={2}>
                    <Typography sx={{mt:2}} variant='h6'>Nightly Rate($)</Typography>
                    <TextField sx={{mb:2}} inputProps={{ maxLength: 6 }} placeholder='Enter Rate' onChange={handleRateChange}/>
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
                        {ListingAmenities()}
                    </List>
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={5}/>
                <Grid item xs={.75}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "gray"}, backgroundColor:'lightgray', m:'auto', justifyContent: 'center', display: 'flex',}} onClick={cancelCreateListing}>Cancel</Button>
                </Grid>
                <Grid item xs={.75}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, backgroundColor:'lightsalmon', m:'auto', justifyContent: 'center', display: 'flex'}} onClick={handleClickCreateListing}>Save</Button>
                </Grid>
                <Grid item xs={5}>
                    <Error message={failMessage} bool={failCreateListing}/> 
                </Grid>
                <LinkModal 
                message={"Listing Saved!"}
                messageTwo={"Back to MyStuff"}
                modalOpen={modalOpen}
                modalClose={() => navigate(`/mystuff`)}/>
            </Grid>
            </div>
            } 
        </div>
    )
}