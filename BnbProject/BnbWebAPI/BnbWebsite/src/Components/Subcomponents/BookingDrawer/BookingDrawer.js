import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { Divider, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import Button from "@mui/material/Button";
import dayjs from "dayjs";
import Drawer from "@mui/material/Drawer";

export default function BookingDrawer({

    listing, 
    confirmBooking,
    checkin,
    checkout,
    handleCheckinChange,
    handleCheckoutChange,
    drawerOpen,
    setDrawerOpen,
    failBooking,
    failBookingMessage
}){

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
const BookingMessage = () => {

    return(
        failBooking &&
        <Typography variant="caption" color="red">{failBookingMessage}</Typography>
    )
}

    return(
        <Drawer open={drawerOpen} anchor={"left"} onClose={() => setDrawerOpen(false)}>
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
                    <BookingMessage/>
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
        </Drawer>
    )

}