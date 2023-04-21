import dayjs from "dayjs";
import { Typography } from "@mui/material";
import ListingsCard from "../ListingsCard/ListingsCard";
import { Drawer } from "@mui/material";
import ReviewDrawer from "../../ReviewDrawer/ReviewDrawer";

export default function PastStays({
    stays,
    listings,
    showReview,
    drawerOpen,
    setDrawerOpen,
    rating,
    setRating,
    handleChangeReviewText,
    failReviewMessage,
    failReview,
    submitReview,
    cancelReview
}){
    
    var past = stays.filter(s => dayjs(s.endDate).isBefore(dayjs()));

    var stayListings = past.map(function(val) {
        return(
            {
                listing: listings.find(l => l.id == val.listingId),
                startDate: val.startDate,
                endDate: val.endDate
            }
        )
    });

    const jsx = stayListings.map(function(val, index){

        return(
            <div key={index}>
                <Typography variant="subtitle1" sx={{mt:2, ml:8}}>
                    {dayjs(val.startDate).format('MM/DD/YYYY').toString()} - {dayjs(val.endDate).format('MM/DD/YYYY').toString()}
                </Typography>
                <ListingsCard listing={val.listing}/>
                {showReview(past[index])}
                <Drawer open={drawerOpen} anchor={"left"} onClose={() => setDrawerOpen(false)}>
                    <ReviewDrawer 
                    listing={val.listing}
                    stay={past[index]}
                    rating={rating}
                    setRating={setRating}
                    handleChangeReviewText={handleChangeReviewText}
                    failReviewMessage={failReviewMessage}
                    failReview={failReview}
                    submitReview={submitReview}
                    cancelReview={cancelReview}
                    />
                </Drawer>
            </div>
        )
    });

    return jsx;

}