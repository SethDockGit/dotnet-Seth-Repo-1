import { Typography } from "@mui/material";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import dayjs from "dayjs";
import Rating from '@mui/material/Rating';
import Grid from '@mui/material/Unstable_Grid2';

export default function ReviewCard({stay}){

    return(
        stay.review != null &&
        <Grid item s={4}>
            <Card sx={{ width:300, margin:3}}>
                <CardContent>
                  <Typography sx={{ fontSize: 14 }}>
                    {stay.review.username}
                  </Typography>
                  <Typography variant="body1" color="text.secondary">
                    {dayjs(stay.startDate).format('MM/DD/YYYY')} - {dayjs(stay.endDate).format('MM/DD/YYYY')}
                  </Typography>
                      <Rating
                      name="user rating"
                      value={stay.review.rating}
                      disabled
                      />
                  <Typography variant="body2">
                    "{stay.review.text}"
                  </Typography>
                </CardContent>
            </Card>
        </Grid>
    )

}