import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import FavoriteIcon from '@mui/icons-material/Favorite';
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import BookingDrawer from '../BookingDrawer/BookingDrawer';


export default function EditListing(){

var testListing = {
    id: 0,
    title: "Cozy 2BR Cabin Up North",
    rate: 205,
    location: "Crosby, MN",
    description: "Come get away from it all in our sunny 2BR cabin on the lake. Paddle in the canoe or take a stroll around the woods. Pet friendly.",
    listingAmenities: ["firepit", "dishwasher", "laundry"] 
};
var testAmenities = [
    "hot tub", "grill", "pool table"
];

const api = `https://localhost:44305`;

const testReviews = [
    {
        id: 0,
        rating: 5,
        text: "We had an excellent time here. The host was friendly and the cabin was beautiful."
    },
    {
        id: 1,
        rating: 5,
        text: "We had an excellent time here. The host was friendly and the cabin was beautiful."
    },
    {
        id: 2,
        rating: 4,
        text: "We enjoyed our stay at the cabin. Would reccomend."
    }
];

const [listing, setListing] = useState(testListing);
const [title, setTitle] = useState(listing.title);
const [rate, setRate] = useState(listing.rate);
const [location, setLocation] = useState(listing.location);
const [description, setDescription] = useState(listing.description);
const [listingAmenities, setListingAmenities] = useState(listing.listingAmenities);
const [reviews, setReviews] = useState(testReviews);


const showListingAmenities = () => {
    
    return listingAmenities.map(function(val, index){
        return(      
            <Typography variant='body2' key={index}>
                • {val}
            </Typography>           
        )        
    })
}
const handleClickFavorite = () => {
    //will add listing to list of user favorites or pop-up with "you need to login to do that"
}
const showReviews = () => {
    
    return reviews.map(function(val, index){

        //you should just loop through the array of stays so you have access to the date and the username too

        return(

            <Grid item xs={3} key={index}>
                <Card sx={{ maxWidth: 300, margin:3}}>
                    <CardContent>
                      <Typography sx={{ fontSize: 14 }}>
                        User
                      </Typography>
                      <Typography variant="body1" color="text.secondary">
                        01/02/2022 to /01/03/2022
                      </Typography>
                      <Typography sx={{ mb: 1.5 }}>
                        star rating here
                      </Typography>
                      <Typography variant="body2">
                        Great job
                      </Typography>
                    </CardContent>
                </Card>
            </Grid>

        )
    })
}
    return(

        <div>

            {/*here go the pics*/}

            <FavoriteIcon sx={{m:2}}onClick={handleClickFavorite}/>
            <Divider sx={{backgroundColor:'peachpuff'}}/>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={3}>
                    <Typography sx={{mt:1}} variant='h6'>{title}</Typography>
                    <Typography variant='subtitle1'>{location}</Typography>
                    <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, mt:2, backgroundColor:"lightsalmon"}}>Book A Stay</Button>    
                </Grid>
                <Grid item xs={2}>
                    <Typography sx={{mt:1}} variant='h6'>${rate}/Night</Typography>
                </Grid>
            </Grid>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={5}>
                    <Typography variant='body1'>"{description}"</Typography>           
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                <Grid item xs={5}>
                    <Typography sx={{mt:2}} variant='h6'>Amenities:</Typography>
                    {showListingAmenities()}
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                <Grid item xs={5}>
                    <Typography sx={{mt:2}} variant='h6'>Reviews</Typography>
                </Grid>
                <Grid item xs={12}/>
                {showReviews()}
            </Grid>

        </div>
    )
}