import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Typography } from '@mui/material';
import { PetsContext } from '../../Contexts/PetsContext';
import { useContext } from 'react';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';

export default function PetTable(){

const {pets} = useContext(PetsContext);

const mapPetData = () => {

    return pets.map(function(val, index){
        return(
            <TableRow key={index}>
                <TableCell>
                    <Typography variant="overline">{val.Name}</Typography>
                </TableCell>
                <TableCell>
                    <Typography variant="overline">{val.Age}</Typography>
                </TableCell>
                <TableCell>
                    <Typography variant="overline">{val.Species}</Typography>
                </TableCell>
                <TableCell sx={{alignItems: 'right', justifyContent:'right', display:'flex'}}>
                    <Link to={`/pets/${val.Id}`}>
                        <Button variant="outlined">View</Button>  
                    </Link>
                </TableCell>
            </TableRow>
        )})};

return(

    <TableContainer sx={{ maxWidth: 600}} component={Paper}>
        <Table sx={{ maxWidth: 600}}>
            <TableHead>
                <TableRow>
                    <TableCell>
                        <Typography variant="overline">Name</Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant="overline">Age</Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant="overline">Species</Typography>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {mapPetData()}
            </TableBody>
        </Table>
    </TableContainer>
)
}