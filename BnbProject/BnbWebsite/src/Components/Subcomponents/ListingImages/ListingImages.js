import { Typography } from "@mui/material";
import Box from '@mui/material/Box';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import Grid from '@mui/material/Unstable_Grid2';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';

export default function ListingImages({
  pictures, 
  handleClickRemovePic}){

const Pics = () => {

    const Picture = ({ data }) => <img src={`data:image/jpeg;base64,${data}`} alt=""
    width="250" height="225"/>

    return pictures.map(function(val, index) {
        return(
            <ListItem key={index}>
                <Picture data={val.data}/>
                <ListItemButton type="button" onClick={handleClickRemovePic} data-value1={val.id}>
                <ListItemIcon>
                  <DeleteForeverIcon/>
                </ListItemIcon>
              </ListItemButton>
            </ListItem>
        )
    })
}

    return(

        <Grid container sx={{justifyContent: 'center', display: 'flex', margin:2}}>
            <Typography variant="h6" sx={{mb:1, justifyContent: 'center', display: 'flex'}}>Your Pictures</Typography>
            <Box sx={{ width: '100%', bgcolor: 'background.paper' }}>
              <List>
                <Pics/>
              </List>
            </Box>
        </Grid>
    )
}