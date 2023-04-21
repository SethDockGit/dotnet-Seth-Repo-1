import ListItem from "@mui/material/ListItem";
import List from "@mui/material/List";
import {Typography } from "@mui/material";
import Grid from '@mui/material/Unstable_Grid2';


export default function AmenitiesList({
    listing,
}){


const Amenities = () => {

    return listing.amenities.map(function(val, index){
        return(      
            <ListItem key={index}>
                {val}
            </ListItem>           
        )        
    })
}

    return(

        <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}> 
            <Grid item xs={4}>
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
                        <Amenities/>
                </List>
            </Grid>
        </Grid>
    )
}