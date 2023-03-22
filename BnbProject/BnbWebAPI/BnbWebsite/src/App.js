import { createBrowserRouter } from "react-router-dom";
import { RouterProvider } from 'react-router-dom';
import Layout from './Components/Layout/Layout';
import CreateListing from './Components/CreateListing/CreateListing';
import EditListing from './Components/EditListing/EditListing';
import Listing from './Components/Listing/Listing';
import Login from './Components/Login/Login';
import MyStuff from './Components/MyStuff/MyStuff';
import ViewListings from "./Components/ViewListings/ViewListings";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { useState } from "react";
import CreateAccount from "./Components/CreateAccount/CreateAccount";



function App() {

const api = `https://localhost:44305`;

const router = createBrowserRouter([{

  path: "/",
  element: <Layout/>,
  children: [
    {
      path: "/listings",
      element: <ViewListings/>
    },
    {
      path: "/listings/:id",
      element: <Listing/>
    },
    {
      path: "/listings/create",
      element: <CreateListing/>
    },
    {
      path: "/listings/edit/:id",
      element: <EditListing/>
    },
    {
      path: "/mystuff",
      element: <MyStuff/>
    },
    {
      path: "/user/login",
      element: <Login/>
    },
    {
      path: "/user/create",
      element: <CreateAccount/>
    }
  ]
}]);




  return (
    <div className="App">
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <RouterProvider router={router}/>
          </LocalizationProvider>
    </div>
  );
}

export default App;
