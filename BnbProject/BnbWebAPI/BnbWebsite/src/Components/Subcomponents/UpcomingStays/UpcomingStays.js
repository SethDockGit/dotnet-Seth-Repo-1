import dayjs from "dayjs";
import { Typography } from "@mui/material";
import ListingsCard from "../../ListingsCard/ListingsCard";

export default function UpcomingStays({
    stays,
    listings
}){

    var upcomingStays = stays.filter(s => dayjs(s.endDate).isAfter(dayjs()));

    var stayListings = upcomingStays.map(function(val) {
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
                <Typography variant="h6">{dayjs(val.startDate)} - {dayjs(val.endDate)}</Typography>
                <ListingsCard listing={val.listing}/>
            </div>
        )
    });

    return jsx;
}