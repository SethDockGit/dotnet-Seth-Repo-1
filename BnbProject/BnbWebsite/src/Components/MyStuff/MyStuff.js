import { Divider, Typography } from "@mui/material";
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
import { Drawer } from "@mui/material";
import ReviewDrawer from "../ReviewDrawer/ReviewDrawer";


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
const [listings, setListings] = useState();
const [drawerOpen, setDrawerOpen] = useState(false);
const [rating, setRating] = useState(5);
const [reviewText, setReviewText] = useState('');
const [failReviewMessage, setFailReviewMessage] = useState('');
const [failReview, setFailReview] = useState(false);
const [modalOpen, setModalOpen] = useState(false);
const [stayToReview, setStayToReview] = useState();
const [listingToReview, setListingToReview] = useState();
const navigate = useNavigate();

const reRoute = () => {
    let now = String(dayjs());
    document.cookie = `id=;expires=${now}UTC;path=/`;
    navigate("/user/login");
}
const getListings = () => {

    fetch(`${api}/bnb/listings`)
    .then((response) => response.json())
    .then((data) => {
    
        setListings(data.listings);
        console.log(data);
    });
}
const getUser = (id) => {

    fetch(`${api}/bnb/user/${id}`)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
        setUser(data.user);
    });
}
const verifyLogin = () => {

    if(!user){

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
}, [user])
    
useEffect(() => {
    getListings();
}, [])


const openReviewDrawer = (stay, listing) => {
    setStayToReview(stay);
    setListingToReview(listing);
    setDrawerOpen(true);
}
const showReview = (stay, listing) => {

    var reviewExists = false;

    if(stay.review != null){
        reviewExists = true;
    }

    return(
        <div>
            {!reviewExists    
            ?   <Button variant="contained" sx={{":hover": {
                bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon", ml:3}}
                onClick={() => openReviewDrawer(stay, listing)}>Leave a Review!</Button>   
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
const handleChangeReviewText = (e) => {
    setReviewText(e.target.value);
}
const submitReview = (stay) => {

    if (reviewText == ""){
        setFailReview(true);
        setFailReviewMessage("You must enter text for the review.");
    }
    else{   
        setFailReview(false);

        if (rating == null){
            setRating(0);
        }

        var APIRequest = {

            StayId: stay.id,
            Rating: rating,
            Text: reviewText,
            Username: user.username,
        }
        debugger;
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
            setUser(data.user);
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
            {(!!listings && !!user) && 
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

                    <Divider sx={{backgroundColor:'peachpuff'}}/>

                    <Typography variant="h5" sx={{ml:3, mt:2}}>Your Favorites</Typography>
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
                    <Grid container sx={{mb:2}}>
                        <PastStays
                        stays={user.stays}
                        listings={listings}
                        showReview={showReview}
                        drawerOpen={drawerOpen}
                        setDrawerOpen={setDrawerOpen}
                        handleChangeReviewText={handleChangeReviewText}
                        failReviewMessage={failReviewMessage}
                        failReview={failReview}
                        rating={rating}
                        setRating={setRating}
                        submitReview={submitReview}
                        cancelReview={cancelReview}
                        />
                    </Grid>

                    <Drawer open={drawerOpen} anchor={"left"} onClose={() => setDrawerOpen(false)}>
                        <ReviewDrawer 
                        listing={listingToReview}
                        stay={stayToReview}
                        rating={rating}
                        setRating={setRating}
                        handleChangeReviewText={handleChangeReviewText}
                        failReviewMessage={failReviewMessage}
                        failReview={failReview}
                        submitReview={submitReview}
                        cancelReview={cancelReview}
                        />
                    </Drawer>

                    <Divider sx={{backgroundColor:'peachpuff'}}/>

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