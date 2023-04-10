import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { CardActionArea, Typography } from "@mui/material";
import { Link } from 'react-router-dom';
import Grid from '@mui/material/Unstable_Grid2';

export default function ListingsCard({listing}){


//var blob = new Blob( [ listing.picture ], { type: "image/jpeg" } );
//var imageUrl = URL.createObjectURL( blob );
//
//const showListingPic = () => {
//
//    (listing.picture == null)
//    ? <image src={imageUrl}/>
//    : <div></div>
//}

    return(

        <Grid item s={3}>
            <Link style={{ textDecoration: 'none' }} underline='none' to={`/listings/${listing.id}`}>
                <Card sx={{minWidth:280, ml:3, mr:3, mb:3}}>
                    <CardActionArea>
                        <CardContent>
                            {/*showListingPic()*/}
                            <Grid container>
                                <Grid item xs={8}>
                                    <Typography sx={{ fontSize: 20 }}>
                                      {listing.title}
                                    </Typography>
                                    <Typography variant="body1" color="text.secondary">
                                      {listing.location}
                                    </Typography>
                                </Grid>
                                <Grid item xs={4}>
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