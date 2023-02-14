import { createBrowserRouter } from 'react-router-dom';
import { RouterProvider } from 'react-router-dom';
import Layout from './Components/Layout/Layout';
import Home from './Components/Home/Home';
import TripsView from './Components/TripsView/TripsView';
import Trip from './Components/Trip/Trip';
import { TripsContext } from './Contexts/TripsContext';
import { useState } from 'react';
import CreateTrip from './Components/CreateTrip/CreateTrip';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

function App() {

  document.body.style = 'background: oldlace;'

  //lightcyan?
  //moccasin?
  const testTrips = [
    {
      id: 1,
      location: "BWCA",
      startDate: null,
      endDate: null,
      crew: null,
      gear: null,
    },
    {
      id: 2,
      location: "YellowStone",
      startDate: null,
      endDate: null,
      crew: null,
      gear: null,
    },
    {
      id: 3,
      location: "Jay Cooke",
      startDate: null,
      endDate: null,
      crew: null,
      gear: null,
    },
  ];

  
const [trips, setTrips] = useState(testTrips);
const router = createBrowserRouter([{


  path: "/",
  element: <Layout/>,
  children: [
    {
      path: "/home",
      element: <Home/>
    },
    {
      path: "/trips",
      element: <TripsView/>
    },
    {
      path: "/trips/:id",
      element: <Trip/>
    },
    {
      path: "/trips/create",
      element: <CreateTrip/>
    },
  ]
}]);

  return (
    <div className="App">
      <TripsContext.Provider value={{trips, setTrips }}>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <RouterProvider router={router}/>
        </LocalizationProvider>
      </TripsContext.Provider>
    </div>
  );
}

export default App;
