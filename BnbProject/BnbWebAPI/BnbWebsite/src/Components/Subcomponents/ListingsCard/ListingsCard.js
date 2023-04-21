import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { CardActionArea, Typography } from "@mui/material";
import { Link } from 'react-router-dom';
import Grid from '@mui/material/Unstable_Grid2';
import Box from '@mui/material/Box';

export default function ListingsCard({
    listing
}){


    const showPic = () => {

        if(!!listing.pictures[0]){

            var data = listing.pictures[0].data;

            const Picture = ({ data }) => <img src={`data:image/jpeg;base64,${data}`} alt=""
            width="225" height="250"/>

            return(
                <Picture data={data}/>
            )
        }
        else{
            return(
                <Box sx={{width:225, height:250}}>
                    <Typography variant="caption">No image available</Typography>
                </Box>
            )
        }
    }

    return(

        <Grid item s={3}>
            <Link style={{ textDecoration: 'none' }} underline='none' to={`/listings/${listing.id}`}>
                <Card sx={{maxWidth:300, minWidth:250, minHeight:370, ml:3, mr:3, mb:3}}>
                    <CardActionArea>
                        <CardContent>
                            {showPic()}
                            <Grid container>
                                <Grid item xs={7}>
                                    <Typography sx={{ fontSize: 20 }}>
                                      {listing.title}
                                    </Typography>
                                    <Typography variant="body1" color="text.secondary">
                                      {listing.location}
                                    </Typography>
                                </Grid>
                                <Grid item xs={5}>
                                    <Typography variant="body1" sx={{mt:1}}>
                                      ${listing.rate}/Night
                                    </Typography>
                                </Grid>
                            </Grid>
                        </CardContent>
                    </CardActionArea>
                </Card>
            </Link>
        </Grid>
    )
}