import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';


export default function People() {
  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Id goes here
        </Typography>
        <Typography variant="h5" component="div">
          Name goes here
        </Typography>
        <Typography sx={{ mb: 1.5 }} color="text.secondary">
          Age goes here
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small">Return to People Table</Button>
      </CardActions>
    </Card>
  );
}