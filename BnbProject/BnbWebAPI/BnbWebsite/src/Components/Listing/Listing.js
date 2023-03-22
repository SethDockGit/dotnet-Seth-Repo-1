import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import { useState } from "react";
import Button from "@mui/material/Button";
import FavoriteIcon from '@mui/icons-material/Favorite';
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


export default function Listing(){

const api = `https://localhost:44305`;

const userId = 1; //change once user login setup is configured!!****** check references too

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
const [listing, setListing] = useState();
const [drawerOpen, setDrawerOpen] = useState(false);
const [checkin, setCheckin] = useState('');
const [checkout, setCheckout] = useState('');
const [failBooking, setFailBooking] = useState(false);
const [failBookingMessage, setFailBookingMessage] = useState('');
const [modalOpen, setModalOpen] = useState(false);

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

const checkForData = () => {

    !listingLoaded && getListing();
}

checkForData();

const showListingAmenities = () => {
    
    return listing.amenities.map(function(val, index){
        return(      
            <ListItem key={index}>
                • {val}
            </ListItem>           
        )        
    })
}
const handleClickFavorite = () => {
    //will add listing to list of user favorites or pop-up with "you need to login to do that"
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

                <Typography vairant="caption" sx={{mt:3, mb:1}}>Check-in</Typography>
                <DesktopDatePicker
                    label="Check-in"
                    inputFormat="MM/DD/YYYY"
                    value={checkin}
                    shouldDisableDate={isDisabled}
                    onChange={handleCheckinChange}
                    />
                <Typography vairant="caption" sx={{mt:3, mb:1}}>Check-out</Typography>
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
            GuestId: userId,
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

            val.review.username != null &&
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
            {listingLoaded && 
            <div>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <FavoriteIcon sx={{m:2}}onClick={handleClickFavorite}/>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={3}>
                    <Typography sx={{mt:1}} variant='h6'>{listing.title}</Typography>
                    <Typography variant='subtitle1'>{listing.location}</Typography>
                    <Button variant="contained" sx={{":hover": {
                        bgcolor: "peachpuff"}, mt:2, backgroundColor:"lightsalmon"}}
                        onClick={() => setDrawerOpen(true)}>
                        Book A Stay
                    </Button>    

                    <Drawer open={drawerOpen} anchor={"left"} onClose={() => setDrawerOpen(false)}>
                        {showBookingDrawer()}
                    </Drawer>
                </Grid>
                <Grid item xs={2}>
                    <Typography sx={{mt:1}} variant='h6'>${listing.rate}/Night</Typography>
                </Grid>
            </Grid>
            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
                <Grid item xs={5}>
                    <Typography variant='body1'>"{listing.description}"</Typography>           
                </Grid>
            </Grid>

            <Divider sx={{backgroundColor:'peachpuff'}}/>

            <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
                <Grid item xs={5}>
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
                <Grid item xs={5}>
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