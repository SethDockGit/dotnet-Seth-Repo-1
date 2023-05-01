import Modal from '@mui/material/Modal';
import Box from "@mui/material/Box";
import { Typography } from "@mui/material";
import Button from "@mui/material/Button";
import Grid from '@mui/material/Unstable_Grid2';

export default function WarningModal({
  message, 
  modalOpen, 
  modalClose, 
  handleClickDeleteListing
}){


const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    borderRadius:3,
    boxShadow: 24,
    p: 4,
  };



    return(

        <Modal
            open={modalOpen}
            onClose={modalClose}
        >
          <Box sx={style}>
          <Typography variant="h5">{message}</Typography>
          <Grid container sx={{m:'auto', justifyContent: 'center', display: 'flex'}}>
            <Grid item xs={4}>
              <Button variant="contained" sx={{":hover": {
              bgcolor: "gray"}, backgroundColor:'lightgray', m:'auto', justifyContent: 'center', display: 'flex'}} 
              onClick={modalClose}>Cancel</Button>
            </Grid>
            <Grid item xs={4}>
              <Button variant="contained" sx={{":hover": {
              bgcolor: "darkred"}, backgroundColor:'red', m:'auto', justifyContent: 'center', display: 'flex'}} 
              onClick={handleClickDeleteListing}>Confirm</Button>
            </Grid>
          </Grid>
          </Box>
      </Modal>
    )
}