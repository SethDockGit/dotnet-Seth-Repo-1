import Typography from '@mui/material/Typography';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';


function PeopleTable({people}){

    const mapPeopleToRows = () => {
        return people.map(function(val, index){
            return(
                <TableRow key={index}>
                    <TableCell>{val.firstName}</TableCell>
                  <TableCell align={'right'}>{val.lastName}</TableCell>
                  <TableCell align={'right'}>{val.age}</TableCell>
                  <TableCell align={'right'}>
                    <Link to={`/people/${val.id}`}>
                      <Button align={'right'} size="small" variant="contained">View</Button>
                    </Link>
                  </TableCell>
                </TableRow>
            )})
    }

    return(

        <>
            <Typography variant="h3" gutterBottom>The People</Typography>
            <TableContainer component={Paper}>
              <Table sx={{maxWidth: 400}}>
                <TableHead>
                  <TableRow>
                    <TableCell>
                      <Typography variant={'overline'}>First Name</Typography>
                    </TableCell>
                    <TableCell align={'right'}>
                      <Typography variant={'overline'}>Last Name</Typography>
                    </TableCell>
                    <TableCell align={'right'}>
                      <Typography variant={'overline'}>Age</Typography>
                    </TableCell>
                    <TableCell align={'right'}>
                      <Typography variant={'overline'}>View</Typography>
                    </TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                {
                    mapPeopleToRows()
                }
                </TableBody>
              </Table>
            </TableContainer>
        </>
    )
}

export default PeopleTable;