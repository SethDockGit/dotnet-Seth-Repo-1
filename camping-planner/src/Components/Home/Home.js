import { Box, Typography } from "@mui/material"


export default function Home(){

    return(
        
            <div style={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            }}> 
            <Box height={500} width={600}>
                <Typography variant="h3" style={{
                    marginTop:50,
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    fontSize:36
                    }} >
                Welcome to CampPlan Beta
                </Typography>
                <br/>
                <Typography variant="body1"style={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    }}>
                    Use this website to organize camping trips with friends, keep up with dates, 
                    get access to everyone's contact info, and plan what gear you'll need for the trip.
                    It is currently in an early stage of development, using test data rather than persistent 
                    data, but test it out and see if it's something you might like to use once it's been fully
                    developed. Thanks! -Seth
                </Typography>
            </Box>    
        </div>
        
    )
}