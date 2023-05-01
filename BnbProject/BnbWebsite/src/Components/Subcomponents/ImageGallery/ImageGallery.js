import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';
import Grid from '@mui/material/Unstable_Grid2';
import { Typography } from "@mui/material";

export default function ImageGallery({
  listing
}) {


    const Picture = ({ data }) => <img src={`data:image/jpeg;base64,${data}`} alt=""
    width="500" height="450"/>

    return (

      (listing.pictures.length == 0)
      ?      
      <Grid container sx={{justifyContent: 'center', display: 'flex'}}>
          <Typography variant="caption" sx={{m:5}}>No image available</Typography>
      </Grid>           
      : 
      <Grid container sx={{justifyContent: 'center', display: 'flex'}}>
        <ImageList sx={{ width: 500, height: 450 }} cols={1} rowHeight={450}>
          {listing.pictures.map((val, index) => (
            <ImageListItem key={index}>
              <Picture data={val.data}/>
            </ImageListItem>
          ))}
        </ImageList>
      </Grid>
    );
  }