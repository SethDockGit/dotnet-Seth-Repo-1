import { Typography } from "@mui/material";


export default function Error({
    message, 
    bool
}){


    return (
        <div>
        {
        bool &&   
            <div style={{margin:'auto'}}>
                <Typography color="red" variant="caption">{message}</Typography>
            </div>    
        }
        </div>
    )
}