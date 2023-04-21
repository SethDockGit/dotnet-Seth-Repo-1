import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import { useParams } from "react-router-dom";
import dayjs from "dayjs";
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import ImageGallery from "../Subcomponents/ImageGallery/ImageGallery";
import AmenitiesList from "../Subcomponents/AmenitiesList/AmenitiesList";
import FaveIcon from "../Subcomponents/FaveIcon/FaveIcon";
import ReviewCard from "../Subcomponents/ReviewCard/ReviewCard";
import BookingDrawer from "../Subcomponents/BookingDrawer/BookingDrawer";
import Error from "../Subcomponents/Error/Error";

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
const {user, setUser, isLoggedIn, setIsLoggedIn} = useContext(UserContext);
const [listing, setListing] = useState();
const [modalOpen, setModalOpen] = useState(false);
const [requiresLogin, setRequiresLogin] = useState(false);
const [loginErrorMessage, setLoginErrorMessage] = useState('');
const [failBooking, setFailBooking] = useState(false);
const [failBookingMessage, setFailBookingMessage] = useState('');
const [drawerOpen, setDrawerOpen] = useState(false);
const [checkin, setCheckin] = useState('');
const [checkout, setCheckout] = useState('');
const [isFavorite, setIsFavorite] = useState(false);

const getUser = (id) => {

    fetch(`${api}/bnb/user/${id}`)
    .then((response) => response.json())
    .then((data) => {
        setUser(data.user);
    })
    .then(() => {
        setIsLoggedIn(true);
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
            setIsLoggedIn(false);
        }
    }
    else{
        if(dayjs().isAfter(dayjs(user.logTime).add(6, 'hour'))){
            setIsLoggedIn(false);
        }
        else{ 
            setIsLoggedIn(true);
        }
    } 
}
useEffect(() => {
    verifyLogin();
}, []);

const getListing = () => {

    fetch(`${api}/bnb/listing/${id}`)
    .then((response) => response.json())
    .then((data) => {
        setListing(data.listing);
        console.log(data);
    })
    .then(() => {
        setListingLoaded(true);
    });
}
  useEffect(() => {
    getListing();
}, []);

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
                setUser(data.user);
                setDrawerOpen(false);
                setModalOpen(true);
            });
    }
}
const Reviews = () => {
    
    return listing.stays.map(function(val, index){

        return(
            <ReviewCard stay={val} key={index}/>
        )
    })
}
const handleCheckinChange = (newValue) => {
    setCheckin(newValue);
}
const handleCheckoutChange = (newValue) => {
    setCheckout(newValue);
}
const handleClickBooking = () => {

    if(!isLoggedIn){
        setRequiresLogin(true);
        setLoginErrorMessage("You must log in to book a stay.");
    }
    else{
        setDrawerOpen(true);
    }
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
                    getUser(user.id);
                }
            });
    }
}
const handleClickUnFavorite = () => {

        var APIRequest = {
            UserId: user.id,
            ListingId: listing.id
        };

        fetch(`${api}/bnb/removefavorite`, {
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
                    var elements = document.cookie.split('=');
                    var id = Number(elements[1]);
                    getUser(id);
                }
            });
}

    return(

        <div>     

            {listingLoaded && 

            <div>

            <ImageGallery listing={listing}/>

            <FaveIcon 
            user={user}
            isLoggedIn={isLoggedIn} 
            handleClickFavorite={handleClickFavorite}
            handleClickUnFavorite={handleClickUnFavorite}
            id={id} 
            isFavorite={isFavorite}
            setIsFavorite={setIsFavorite}/>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={12} sx={{justifyContent: 'center', display: 'flex'}}>
                    <Error message={loginErrorMessage} bool={showLoginError}/>
                </Grid>
                <Grid item xs={3}>
                    <Typography sx={{mt:1}} variant='h6'>{listing.title}</Typography>
                    <Typography variant='subtitle1'>{listing.location}</Typography>
                    <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, mt:2, backgroundColor:"lightsalmon"}}
                        onClick={handleClickBooking}>
                        Book A Stay
                    </Button>    

                    <BookingDrawer 
                    listing={listing}
                    confirmBooking={confirmBooking}
                    checkin={checkin}
                    checkout={checkout}
                    handleCheckinChange={handleCheckinChange}
                    handleCheckoutChange={handleCheckoutChange}
                    drawerOpen={drawerOpen}
                    setDrawerOpen={setDrawerOpen}
                    failBooking={failBooking}
                    failBookingMessage={failBookingMessage} 
                    />
                    

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

            <AmenitiesList listing={listing}/>

            <Divider sx={{backgroundColor:'peachpuff'}}/>


            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                <Grid item xs={4}>
                    <Typography sx={{mt:2}} variant='h6'>Reviews</Typography>
                </Grid>
                <Grid item xs={12}/>
                <Reviews/>
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