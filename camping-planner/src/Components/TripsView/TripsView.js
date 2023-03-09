import { TripsContext } from "../../Contexts/TripsContext";
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { useContext } from "react";
import { CardActionArea } from '@mui/material';
import { Link } from 'react-router-dom';
import dayjs from "dayjs";


export default function TripsView(){

    const {trips} = useContext(TripsContext);

    const showUpcomingTrips = () => {

        return trips.map(function(val, index){

            if(dayjs(val.endDate).isAfter(dayjs())){

                return(

                    <Link style={{ textDecoration: 'none' }} underline='none' to={`/trips/${val.id}`} key={index}>
                        <Card sx={{margin:4, bgcolor: 'burlywood'}}>
                            <CardActionArea>
                                <CardContent>
                                    <Typography variant="h5">
                                        <i>
                                        {val.location} ---- {dayjs(val.startDate).format('MM/DD/YYYY')} to {dayjs(val.endDate).format('MM/DD/YYYY')}
                                        </i>
                                    </Typography>
                                </CardContent>
                            </CardActionArea>
                        </Card>
                    </Link>
                )

            }})};

    const showPastTrips = () => {

        return trips.map(function(val, index){

            if(dayjs(val.endDate).isBefore(dayjs())){

                return(

                    <Link style={{ textDecoration: 'none' }} underline='none' to={`/trips/${val.id}/past`} key={index}>
                        <Card sx={{margin:4, bgcolor: 'moccasin'}}>
                            <CardActionArea>
                                <CardContent>
                                    <Typography variant="h5">
                                        <i>
                                        {val.location} ---- {dayjs(val.startDate).format('MM/DD/YYYY')} to {dayjs(val.endDate).format('MM/DD/YYYY')}
                                        </i>
                                    </Typography>
                                </CardContent>
                            </CardActionArea>
                        </Card>
                    </Link>
                )
            }})};

    return(
        <div>
            <Typography sx={{margin:4}} variant="h5">Upcoming</Typography>
            {showUpcomingTrips()}
            <Typography sx={{margin:4}} variant="h5">Past</Typography>
            {showPastTrips()}
        </div>
    )
}
