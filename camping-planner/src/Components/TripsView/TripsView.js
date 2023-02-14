import { TripsContext } from "../../Contexts/TripsContext";
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { useContext } from "react";
import { CardActionArea } from '@mui/material';
import { Link } from 'react-router-dom';


export default function TripsView(){

    const {trips} = useContext(TripsContext);

    const mapTrips = () => {

        return trips.map(function(val, index){

            return(
                <Link style={{ textDecoration: 'none' }} underline='none' to={`/trips/${val.id}`} key={index}>
                    <Card sx={{margin:4, bgcolor: 'burlywood'}}>
                        <CardActionArea>
                            <CardContent>
                                <Typography variant="h5">
                                    {val.location}
                                </Typography>
                            </CardContent>
                        </CardActionArea>
                    </Card>
                </Link>
            )})};

    return(
        <div>
            {mapTrips()}
        </div>
    )
}