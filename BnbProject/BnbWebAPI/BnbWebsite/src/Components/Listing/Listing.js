import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import FavoriteIcon from '@mui/icons-material/Favorite';
import IconButton from '@mui/material/IconButton';
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { useParams } from "react-router-dom";
import Rating from '@mui/material/Rating';
import dayjs from "dayjs";
import ListItem from "@mui/material/ListItem";
import List from "@mui/material/List";
import Drawer from "@mui/material/Drawer";
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";


export default function Listing(){

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

const {id} = useParams();
const [listingLoaded, setListingLoaded] = useState(false);
const {user, setUser} = useContext(UserContext);
const [listing, setListing] = useState();
const [drawerOpen, setDrawerOpen] = useState(false);
const [checkin, setCheckin] = useState('');
const [checkout, setCheckout] = useState('');
const [failBooking, setFailBooking] = useState(false);
const [failBookingMessage, setFailBookingMessage] = useState('');
const [modalOpen, setModalOpen] = useState(false);
const [requiresLogin, setRequiresLogin] = useState(false);
const [loginErrorMessage, setLoginErrorMessage] = useState('');
const [isLoggedIn, setIsLoggedIn] = useState(false);
const [loginChecked, setLoginChecked] = useState(false);
const [addToFavorites, setAddtoFavorites] = useState(false);

const getListing = () => {

    fetch(`${api}/bnb/listing${id}`)
    .then((response) => response.json())
    .then((data) => {
  
        setListing(data.listing);
        console.log(data);
    })
    .then(() => {
        setListingLoaded(true);
    });
  }

if(!listingLoaded){
    getListing();
}

const checkLogin = () => {

    if(!loginChecked){

        if(user != null && dayjs().isBefore(dayjs(user.logTime).add(6, 'hour'))){
            setIsLoggedIn(true);
        }
        setLoginChecked(true);
    }
}
checkLogin();



const showListingAmenities = () => {
    
    return listing.amenities.map(function(val, index){
        return(      
            <ListItem key={index}>
                • {val}
            </ListItem>           
        )        
    })
}
const showBookingDrawer = () => {

    const isDisabled = (date) => {

        for(let i = 0; i < listing.stays.length; i++){

            if(dayjs(date).isBetween(dayjs(listing.stays[i].startDate), dayjs(listing.stays[i].endDate), 'day', '[)')){
                return true;
            }
            else{
                return false;
            }
        }
    }

    return(
        <Grid container sx={{justifyContent: 'center', display: 'flex', width:400}}>
            <Grid item xs={7}>
                <Typography variant="h4" sx={{mb:3, mt:3}}>Book a Stay</Typography>
                <Divider sx={{backgroundColor:'peachpuff'}}/>

                <Typography vairant="h6" sx={{mt:4, mb:1}}>When would you like to stay?</Typography>

                <Typography sx={{mt:3, mb:1}}>Check-in</Typography>
                <DesktopDatePicker
                    label="Check-in"
                    inputFormat="MM/DD/YYYY"
                    value={checkin}
                    shouldDisableDate={isDisabled}
                    onChange={handleCheckinChange}
                    />
                <Typography sx={{mt:3, mb:1}}>Check-out</Typography>
                <DesktopDatePicker
                    label="Check-out"
                    inputFormat="MM/DD/YYYY"
                    value={checkout}
                    shouldDisableDate={isDisabled}
                    onChange={handleCheckoutChange}
                    />
                {showBookingMessage()}
                <br/>
                <Button variant="contained" sx={{":hover": {
                bgcolor: "peachpuff"}, mt:3, mr:2, backgroundColor:"lightsalmon"}} 
                onClick={confirmBooking}>Confirm
                </Button>
                <Button variant="contained" sx={{":hover": {
                bgcolor: "gray"}, mt:3, backgroundColor:'lightgray'}} 
                onClick={() => setDrawerOpen(false)}>Cancel</Button>
            </Grid>  
        </Grid>
    )
}
const handleCheckinChange = (newValue) => {
    setCheckin(newValue);
}
const handleCheckoutChange = (newValue) => {
    setCheckout(newValue);
}
const showBookingMessage = () => {

    return(
        failBooking &&
        <Typography variant="caption" color="red">{failBookingMessage}</Typography>
    )
}
const confirmBooking = () => {

    if(dayjs(checkin).isAfter(dayjs(checkout))){

        setFailBooking(true);
        setFailBookingMessage("Check-in date must be before check-out date.");
    }
    else if(dayjs(checkin).isBefore(dayjs()) || dayjs(checkin).isBefore(dayjs())){

        setFailBooking(true);
        setFailBookingMessage("Check-in and check-out must be future dates.");
    }
    else if((checkin == "" && checkout != "") || (checkin == null && checkout != null)){
        setFailBooking(true);
        setFailBookingMessage("Please select a check-in date.");
    }
    else if((checkout == "" && checkin != "") || (checkout == null && checkin != null)){
        setFailBooking(true);
        setFailBookingMessage("Please select a check-out date.");
    }
    else{

        const APIRequest = {
            GuestId: user.id,
            HostId: listing.hostId,
            ListingId: Number(id),
            StartDate: checkin,
            EndDate: checkout,
        }

        fetch(`${api}/bnb/addstay`, {
            method: 'POST',
            body: JSON.stringify(APIRequest),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                setDrawerOpen(false);
                setModalOpen(true);
            });
    }
}
const showReviews = () => {
    
    return listing.stays.map(function(val, index){

        return(

            val.review != null &&
            <Grid item s={4} key={index}>
                <Card sx={{ maxWidth: 300, margin:3}}>
                    <CardContent>
                      <Typography sx={{ fontSize: 14 }}>
                        {val.review.username}
                      </Typography>
                      <Typography variant="body1" color="text.secondary">
                        {dayjs(val.startDate).format('MM/DD/YYYY')} - {dayjs(val.endDate).format('MM/DD/YYYY')}
                      </Typography>
                          <Rating
                          name="user rating"
                          value={val.review.rating}
                          disabled
                          />
                      <Typography variant="body2">
                        "{val.review.text}"
                      </Typography>
                    </CardContent>
                </Card>
            </Grid>

        )
    })
}
const handleClickBooking = () => {

    if(!isLoggedIn){
        setRequiresLogin(true);
        setLoginErrorMessage("You must log in to book a stay.");
    }
    else{
        setDrawerOpen(true);
    }
    //before changing it, onClick for booking button was {() => setDrawerOpen(true)}
}
const showLoginError = () => {
    return(
        requiresLogin &&
        <Typography variant="caption" color="red">{loginErrorMessage}</Typography>
    )
}
const handleClickFavorite = () => {

    if(!isLoggedIn){
        setRequiresLogin(true);
        setLoginErrorMessage("You must log in to add to favorites.");
    }
    else{

        var APIRequest = {
            UserId: user.id,
            ListingId: listing.id
        };

        fetch(`${api}/bnb/favorite`, {
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
                    setAddtoFavorites(true);
                }
            });

    }
}
const displayFavoriteIcon = () => {

    var isFavorite = false;

    if(isLoggedIn){

        for(let i = 0; i < user.favorites.length; i++){

            if(user.favorites[i] == id || addToFavorites){
                isFavorite = true;
            }
        }
    }

    return(
        isFavorite 
        ?   <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <IconButton>
                    <FavoriteIcon sx={{m:2, color:"pink"}}/>
                </IconButton>
            </Grid>
        :   <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <IconButton onClick={handleClickFavorite}>
                    <FavoriteIcon sx={{m:2}}/>
                </IconButton>
            </Grid>
    )
}
    return(

        <div>

            {/*here go the pics*/}
            {listingLoaded && 
            <div>
            {displayFavoriteIcon()}

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={12} sx={{justifyContent: 'center', display: 'flex'}}>
                    {showLoginError()}
                </Grid>
                <Grid item xs={3}>
                    <Typography sx={{mt:1}} variant='h6'>{listing.title}</Typography>
                    <Typography variant='subtitle1'>{listing.location}</Typography>
                    <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, mt:2, backgroundColor:"lightsalmon"}}
                        onClick={handleClickBooking}>
                        Book A Stay
                    </Button>    

                    <Drawer open={drawerOpen} anchor={"left"} onClose={() => setDrawerOpen(false)}>
                        {showBookingDrawer()}
                    </Drawer>
                </Grid>
                <Grid item xs={1}>
                    <Typography sx={{mt:1}} variant='h6'>${listing.rate}/Night</Typography>
                </Grid>
            </Grid>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={4}>
                    <Typography variant='body1'>"{listing.description}"</Typography>           
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                <Grid item xs={4}>
                    <Typography sx={{mt:2}} variant='h6'>Amenities</Typography>
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
                <Grid item xs={4}>
                    <Typography sx={{mt:2}} variant='h6'>Reviews</Typography>
                </Grid>
                <Grid item xs={12}/>
                {showReviews()}
            </Grid>
            <Modal
              open={modalOpen}
              onClose={() => setModalOpen(false)}
            >
                <Box sx={style}>
                    <Typography variant="h5">Booking confirmed! Enjoy your stay</Typography>
                </Box>
            </Modal>

            </div>}

        </div>
    )
}