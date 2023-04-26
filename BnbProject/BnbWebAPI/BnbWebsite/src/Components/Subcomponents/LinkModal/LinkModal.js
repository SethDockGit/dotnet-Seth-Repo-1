import Modal from '@mui/material/Modal';
import Box from "@mui/material/Box";
import { Typography } from "@mui/material";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";

export default function LinkModal({
  message, 
  messageTwo, 
  modalOpen, 
  modalClose}){


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
              <Link style={{ textDecoration: 'none' }} to={`/mystuff`}>
                <Button>{messageTwo}</Button>
              </Link>
          </Box>
      </Modal>
    )
}