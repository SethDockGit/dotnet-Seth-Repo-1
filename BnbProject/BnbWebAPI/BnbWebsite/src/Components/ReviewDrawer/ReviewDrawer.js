import { Divider, TextField, Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';
import Error from "../Subcomponents/Error/Error";
import Button from "@mui/material/Button";
import Rating from '@mui/material/Rating';

export default function ReviewDrawer({
    listing,
    stay,
    rating,
    setRating,
    handleChangeReviewText,
    failReviewMessage,
    failReview,
    submitReview,
    cancelReview
}){

    return(

        <Grid container sx={{justifyContent: 'center', display: 'flex', width:400}}>
            <Grid item xs={10}>
                <Typography variant="h4" sx={{mb:3, mt:3}}>Leave a Review for- {listing.title}</Typography>
                <Divider sx={{backgroundColor:'peachpuff'}}/>
                <Typography variant="h6" sx={{mt:4}}>Your Rating</Typography>
                <br/>
                <Rating
                    name="user rating"
                    value={rating}
                    onChange={(event, newValue) => {
                    setRating(newValue);}}
                />
                <Typography variant="h6" sx={{mt:4}}>How was your stay?</Typography>
                <TextField multiline minRows={5} maxRows={8} placeholder="..." 
                sx={{width:300}} inputProps={{maxLength: 300}} onChange={handleChangeReviewText}/>
                <br/>
                <Error message={failReviewMessage} bool={failReview}/>
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