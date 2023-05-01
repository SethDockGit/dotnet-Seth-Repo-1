import FavoriteIcon from '@mui/icons-material/Favorite';
import IconButton from '@mui/material/IconButton';
import Grid from '@mui/material/Unstable_Grid2';

export default function FaveIcon({
    user,
    isLoggedIn,
    handleClickFavorite,
    handleClickUnFavorite,
    id,
    addToFavorites
}){


var isFavorite = false;

if(isLoggedIn && !!user){
    for(let i = 0; i < user.favorites.length; i++){
        if(user.favorites[i] == id || addToFavorites){
            isFavorite = true;
        }
    }
}

    return(
        isFavorite 
        ?   <Grid container sx={{justifyContent: 'center', display: 'flex'}}>
                <IconButton onClick={handleClickUnFavorite}>
                    <FavoriteIcon sx={{color:"pink", mb:2}}/>
                </IconButton>
            </Grid>
        :   <Grid container sx={{justifyContent: 'center', display: 'flex'}}>
                <IconButton onClick={handleClickFavorite}>
                    <FavoriteIcon sx={{mb:2}}/>
                </IconButton>
            </Grid>
    )
}