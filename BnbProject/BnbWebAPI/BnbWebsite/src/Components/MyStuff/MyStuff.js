import { Divider, TextField, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";
import dayjs from "dayjs";
import Rating from '@mui/material/Rating';
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import { UserContext } from "../../Contexts/UserContext/UserContext";
import { useContext } from "react";
import { useNavigate } from 'react-router-dom';
import MyListings from "../Subcomponents/MyListings/MyListings";
import UpcomingStays from "../Subcomponents/UpcomingStays/UpcomingStays";
import PastStays from "../Subcomponents/PastStays/PastStays";
import Favorites from "../Subcomponents/Favorites/Favorites";


export default function MyStuff(){


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
const [userLoaded, setUserLoaded] = useState(false);
const [listings, setListings] = useState();
const [listingsLoaded, setListingsLoaded] = useState(false);
const [drawerOpen, setDrawerOpen] = useState(false);
const [rating, setRating] = useState(5);
const [reviewText, setReviewText] = useState('');
const [failReviewMessage, setFailReviewMessage] = useState('');
const [modalOpen, setModalOpen] = useState(false);
const navigate = useNavigate();

const reRoute = () => {
    let now = String(dayjs());
    document.cookie = `id=;expires=${now}UTC;path=/`;
    //this should overwrite any cookie so that it expires.
    navigate("/user/login");
}

useEffect(() => {

    fetch(`${api}/bnb/listings`)
    .then((response) => response.json())
    .then((data) => {
    
        setListings(data.listings);
        console.log(data);
    })
    .then(() => {
        setListingsLoaded(true);
    });

}, [])

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
}, [])


const showReview = (stay) => {

    var reviewExists = false;

    if(stay.review != null){
        reviewExists = true;
    }

    return(
        <div>
            {!reviewExists    
            ?   <Button variant="contained" sx={{":hover": {
                bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon", ml:3}}
                onClick={() => setDrawerOpen(true)}>Leave a Review!</Button>   
            : <Typography variant="subtitle1" sx={{ml:3}}>
                Your review:       
                    <Rating
                    name="user rating"
                    value={stay.review.rating}
                    disabled
                    />
                </Typography>
            }
        </div>
    )
}
const showReviewDrawer = (listing, stay) => {

    return(
        <Grid container sx={{justifyContent: 'center', display: 'flex', width:400}}>
            <Grid item xs={10}>
                <Typography variant="h4" sx={{mb:3, mt:3}}>Leave a Review for- {listing.title}</Typography>
                <Divider sx={{backgroundColor:'peachpuff'}}/>
                <Typography vairant="h6" sx={{mt:4}}>Your Rating</Typography>
                <br/>
                <Rating
                    name="user rating"
                    value={rating}
                    onChange={(event, newValue) => {
                    setRating(newValue);}}
                />
                <Typography vairant="h6" sx={{mt:4}}>How was your stay?</Typography>
                <TextField multiline minRows={5} maxRows={8} placeholder="..." 
                sx={{width:300}} inputProps={{maxLength: 300}} onChange={handleChangeReviewText}/>
                <br/>
                <Typography variant="caption" color="red">{failReviewMessage}</Typography>
                <br/>
                <Button variant="contained" sx={{":hover": {
                bgcolor: "peachpuff"}, mt:3, mr:2, backgroundColor:"lightsalmon"}} 
                onClick={() => submitReview(stay)}>Submit
                </Button>
                <Button variant="contained" sx={{":hover": {
                bgcolor: "gray"}, mt:3, backgroundColor:'lightgray'}} 
                onClick={cancelReview}>Cancel</Button>
            </Grid>
            
        </Grid>
    )
}
const handleChangeReviewText = (e) => {
    setReviewText(e.target.value);
}
const submitReview = (stay) => {

    if (reviewText == ""){
        setFailReviewMessage("You must enter text for the review.");
    }
    else{
        
        setFailReviewMessage("");

        if (rating == null){
            setRating(0);
        }

        var APIRequest = {

            StayId: stay.id,
            Rating: rating,
            Text: reviewText,
            Username: user.username,
        }

        fetch(`${api}/bnb/review`, {
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
const cancelReview = () => {
    setRating(undefined);
    setReviewText("");
    setDrawerOpen(false);
}

    return(
        <div>
            {userLoaded && listingsLoaded && 
                <div>
                    <Typography variant="h2" sx={{justifyContent: 'center', display: 'flex', m:3}}>Welcome {user.username}</Typography>

                    <Grid container sx={{mb:2}}>
                        <Typography variant="h5" sx={{ml:3}}>Your Listings</Typography>
                        <Link style={{ textDecoration: 'none' }} to={`/listings/create`}>
                            <Button variant="contained" sx={{":hover": {
                            bgcolor: "peachpuff"}, ml: 3, backgroundColor:"lightsalmon"}} 
                            >Create New
                            </Button>
                        </Link>
                    </Grid>

                    <Grid container sx={{mb:2}}>
                        <MyListings listings={user.listings}/>
                    </Grid>
                    {/*show list of booked stays as well?*/}
                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Favorites</Typography>
                    <Grid container sx={{mb:2}}>
                        <Favorites favorites={user.favorites} listings={listings}/>
                    </Grid>

                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Coming Up</Typography>
                    <Grid container sx={{mb:2}}>
                        <UpcomingStays stays={user.stays} listings={listings}/>
                    </Grid>

                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Past Stays</Typography>
                    <Grid container>
                        <PastStays
                        stays={user.stays}
                        listings={listings}
                        showReview={showReview}
                        drawerOpen={drawerOpen}
                        setDrawerOpen={setDrawerOpen}
                        showReviewDrawer={showReviewDrawer}
                        handleChangeReviewText={handleChangeReviewText}
                        submitReview={submitReview}
                        cancelReview={cancelReview}
                        />
                    </Grid>
                    <Modal
                      open={modalOpen}
                      onClose={() => setModalOpen(false)}
                    >
                        <Box sx={style}>
                            <Typography variant="h5">Added Review!</Typography>
                        </Box>
                    </Modal>
                </div>
            }
        </div>
    )
}