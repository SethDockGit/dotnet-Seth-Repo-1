import ListingsCard from "../ListingsCard/ListingsCard";
import { Link } from "react-router-dom";
import { Button } from "@mui/material";

export default function MyListings({
    listings
}){

    const jsx = listings.map(function(val, index) {

        return(
            <div key={index}>
                <ListingsCard listing={val}/>
                <Link  style={{ textDecoration: 'none' }} to={`/listings/edit/${val.id}`}>
                    <Button variant="contained" sx={{":hover": {
                    bgcolor: "peachpuff"}, justifyContent:'right', backgroundColor:"lightsalmon", ml:3}}>Edit</Button>
                </Link>
            </div>
        )
    });

    return jsx;
}