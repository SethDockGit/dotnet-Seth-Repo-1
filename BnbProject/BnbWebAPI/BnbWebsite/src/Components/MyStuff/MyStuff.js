import { Divider, TextField, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import ListingsCard from "../ListingsCard/ListingsCard";
import { Link } from "react-router-dom";
import dayjs from "dayjs";
import Rating from '@mui/material/Rating';
import Drawer from "@mui/material/Drawer";
import Modal from '@mui/material/Modal';

export default function MyStuff(){


const api = `https://localhost:44305`;

const [user, setUser] = useState();
const [userLoaded, setUserLoaded] = useState(false);
const [listings, setListings] = useState();
const [listingsLoaded, setListingsLoaded] = useState(false);
const [drawerOpen, setDrawerOpen] = useState(false);
const [rating, setRating] = useState();
const [reviewText, setReviewText] = useState('');
const [failSubmitReview, setFailSubmitReview] = useState(false); //this isn't actually used anywhere
const [failReviewMessage, setFailReviewMessage] = useState('');
const [modalOpen, setModalOpen] = useState(false);


const id = 1;   ///will change later when login stuff is hooked up.

const getUser = () => {

    fetch(`${api}/bnb/user${id}`)
    .then((response) => response.json())
    .then((data) => {
        setUser(data.user);
    })
    .then(() => {
        setUserLoaded(true);
    });
}
const getListings = () => {

    fetch(`${api}/bnb/listings`)
    .then((response) => response.json())
    .then((data) => {
  
        setListings(data.listings);
        console.log(data);
    })
    .then(() => {
        setListingsLoaded(true);
    });
}
const checkForData = () => {

    !userLoaded && getUser();
    !listingsLoaded && getListings();
}
checkForData();

const showMyListings = () => {

    return user.listings.map(function(val, index) {

        return(
            <div key={index}>
                <ListingsCard listing={val}/>
                <Link  style={{ textDecoration: 'none' }} to={`/listings/edit/${val.id}`}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon", ml:3}}>Edit</Button>
                </Link>
            </div>
        )
    });
}
const showFavorites = () => {

    return user.favorites.map(function(val, index) {

        return(
            <div key={index}>
                <ListingsCard listing={val}/>
            </div>
        )
    });
}
const showUpcomingStays = () => {

    var upcomingStays = user.stays.filter(s => dayjs(s.endDate).isAfter(dayjs()));

    var stayListings = upcomingStays.map(function(val, index) {
        return(
            listings.find(l => l.id == val.listingId)
        )
    });

    return stayListings.map(function(val, index){

        return(
            <div key={index}>
                <ListingsCard listing={val}/>
            </div>
        )
    });
}
const showPastStays = () => {

    var past = user.stays.filter(s => dayjs(s.endDate).isBefore(dayjs()));

    var stayListings = past.map(function(val, index) {
        return(
            listings.find(l => l.id == val.listingId)
        )
    });

    return stayListings.map(function(val, index){

        return(
            <div key={index}>
                <ListingsCard listing={val}/>
                {showReview(past[index])}
                <Drawer open={drawerOpen} anchor={"left"} onClose={() => setDrawerOpen(false)}>
                    {showReviewDrawer(val, past[index])} {/*double-check that this is solid logic. If not there must be some way to save the stay id*/}
                </Drawer>
            </div>
        )
    });
}
const showReview = (stay) => {

    var reviewExists = false;

    if(stay.review.username != null){
        reviewExists = true;
    }
    //the mechanism here checks for a username associated with the review, and if there is none, assumes the review to be blank
    //**just make sure a stay with no review still comes up here with username null. Stay.review should be instantiated
    //with a new blank review by the manager after pulling from DB.
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
                <Typography variant="caption">{failReviewMessage}</Typography>
                <Button variant="contained" sx={{":hover": {
                bgcolor: "peachpuff"}, mt:3, mr:2, backgroundColor:"lightsalmon"}} 
                onClick={submitReview(stay)}>Submit
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

    if (rating == undefined || reviewText == ""){
        //fail review somehow without breaking.
        
    }
    else{

        if (rating == null){
            setRating(0);
        }

        //need the user here to satisfy this request. remember the id it passes is the stayID.
        var APIRequest = {
            Id: stay.id,
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

            setModalOpen(true);
        });
    }
    
}
const cancelReview = () => {
    
}

    return(
        <div>
            {userLoaded && listingsLoaded &&
                <div>
                    <Typography variant="h2" sx={{justifyContent: 'center', display: 'flex', m:3}}>My Stuff</Typography>
                    <Typography variant="h5" sx={{ml:3}}>Listings</Typography>
                    <Grid container sx={{mb:2}}>
                    {showMyListings()}
                    </Grid>
                    {/*show list of booked stays as well?*/}
                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Favorites</Typography>
                    <Grid container sx={{mb:2}}>
                    {showFavorites()}
                    </Grid>

                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Coming Up</Typography>
                    <Grid container sx={{mb:2}}>
                    {showUpcomingStays()}
                    </Grid>

                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Past Stays</Typography>
                    <Grid container sx={{mb:2}}>
                    {showPastStays()}
                    </Grid>
                </div>
            }
        </div>
    )
}