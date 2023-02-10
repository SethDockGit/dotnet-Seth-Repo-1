import MyAppBar from "../MyAppBar/MyAppBar";
import { Outlet } from "react-router-dom";


export default function Layout(){

    return(
    <>
        <MyAppBar/>
        <Outlet/>
    </>
    )
}