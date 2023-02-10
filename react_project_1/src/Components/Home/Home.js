import { Box, Typography } from "@mui/material"

export default function Home(){

    return(
        <div style={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            }}> 
            <Box height={500} width={500}>
                <Typography variant="h3" style={{
                    marginTop:50,
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    }} >
                    Hey There!
                </Typography>
                <br/>
                <Typography variant="body1"style={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    }}>
                    I don't actually know what this site would practically be for. Is it for 
                    a user of the pet rescue website or the operator? Anyways, rescue an animal
                    and they will love you forever!!
                </Typography>
            </Box>    
        </div>
    )

}