import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';

export default function ImageGallery({listing}) {


    const Picture = ({ data }) => <img src={`data:image/jpeg;base64,${data}`} alt=""
    width="500" height="450"/>

    return (
      <ImageList sx={{ width: 500, height: 450 }} cols={1} rowHeight={450}>
        {listing.pictures.map((val, index) => (
          <ImageListItem key={index}>
            <Picture data={val}/>
          </ImageListItem>
        ))}
      </ImageList>
    );
  }