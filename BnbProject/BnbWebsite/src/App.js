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
import CreateAccount from "./Components/CreateAccount/CreateAccount";
import { UserContext } from "./Contexts/UserContext/UserContext";
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
      path: "/user/login",
      element: <Login/>
    },
    {
      path: "/user/create",
      element: <CreateAccount/>
    }
  ]
}]);


const Provider = props => {

  const [user, setUser] = useState(null);
  const [isLoggedIn, setIsLoggedIn] = useState();

  return (
    <UserContext.Provider
      value={{ user, setUser, isLoggedIn, setIsLoggedIn}}>
        {props.children}
      </UserContext.Provider>
  )
};


  return (
    <div className="App">
      <Provider>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <RouterProvider router={router}/>
        </LocalizationProvider>
      </Provider>
    </div>
  );
}

export default App;
