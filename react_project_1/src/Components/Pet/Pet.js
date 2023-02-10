import { PetsContext } from "../../Contexts/PetsContext";
import { useContext } from "react";
import { useParams } from "react-router-dom";
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';

export default function Pet(){

    const {pets} = useContext(PetsContext);
    const {id} = useParams();
    const pet = pets.find(p => p.Id == id);

    return (
        <Card sx={{ maxWidth: 275 }}>
            <CardContent>
                <Typography variant="h5" component="div">
                {pet.Name}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                Age {pet.Age}
                </Typography>
                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                Species: {pet.Species}
                </Typography>
            </CardContent>
        </Card>
    );
}