import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import FavoriteIcon from '@mui/icons-material/Favorite';
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { useParams } from "react-router-dom";
import BookingDrawer from '../BookingDrawer/BookingDrawer';
import { ListingsContext } from "../../Contexts/ListingsContext";
import { useContext } from "react";
import Rating from '@mui/material/Rating';
import dayjs from "dayjs";


export default function Listing(){

const api = `https://localhost:44305`;

const {id} = useParams();
const {listings, setListings} = useContext(ListingsContext);
const [listing, setListing] = useState(listings.find(l => l.id == id));
const [title, setTitle] = useState(listing.title);
const [rate, setRate] = useState(listing.rate);
const [location, setLocation] = useState(listing.location);
const [description, setDescription] = useState(listing.description);
const [listingAmenities, setListingAmenities] = useState(listing.amenities);

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
    
    return listing.stays.map(function(val, index){

        return(

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
                    <Typography sx={{mt:2}} variant='h6'>Amenities</Typography>
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