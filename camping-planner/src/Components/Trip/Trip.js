import { useParams } from "react-router-dom";
import { useContext } from "react";
import { TripsContext } from "../../Contexts/TripsContext";

export default function Trip(){

    const {trips} = useContext(TripsContext)
    const {id} = useParams();
    const trip = trips.find(t => t.id == id);

    return (

        <div>{trip.location}</div>
    )
}