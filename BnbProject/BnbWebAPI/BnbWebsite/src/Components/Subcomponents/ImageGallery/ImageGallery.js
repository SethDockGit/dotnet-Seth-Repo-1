import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';
import Grid from '@mui/material/Unstable_Grid2';

export default function ImageGallery({listing}) {


    const Picture = ({ data }) => <img src={`data:image/jpeg;base64,${data}`} alt=""
    width="500" height="450"/>

    return (

      (listing.pictures.length == 0)
      ? <div></div>
      : 
      <Grid container sx={{justifyContent: 'center', display: 'flex'}}>
        <ImageList sx={{ width: 500, height: 450 }} cols={1} rowHeight={450}>
          {listing.pictures.map((val, index) => (
            <ImageListItem key={index}>
              <Picture data={val}/>
            </ImageListItem>
          ))}
        </ImageList>
      </Grid>
    );
  }