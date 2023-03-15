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
import { ListingsContext } from "./Contexts/ListingsContext";
import { useState } from "react";



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
      path: "/login",
      element: <Login/>
    },
  ]
}]);

const [listingsLoaded, setListingsLoaded] = useState(false);
const [listings, setListings] = useState();

const getListings = () => {
  debugger;
  fetch(`${api}/bnb/listings`)
  .then((response) => response.json())
  .then((data) => {

      setListings(data.listings);
      console.log(data);
  })
  .then(() => {
      setListingsLoaded(true);
  });
}

const stopRerender = () => {

    !listingsLoaded && getListings();
}

stopRerender();

  return (
    <div className="App">
      {listingsLoaded &&
        <ListingsContext.Provider value={{listings, setListings}}>
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <RouterProvider router={router}/>
          </LocalizationProvider>
        </ListingsContext.Provider> 
      }   
    </div>
  );
}

export default App;
