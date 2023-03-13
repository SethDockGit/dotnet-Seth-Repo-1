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



function App() {

//document.body.style = 'background: peachpuff;'

//set initials for context here. 

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

  return (
    <div className="App">
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <RouterProvider router={router}/>
      </LocalizationProvider>
    </div>
  );
}

export default App;
