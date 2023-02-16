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
import dayjs from 'dayjs';
import PastTrip from './Components/PastTrip/PastTrip';

function App() {

  document.body.style = 'background: oldlace;'

  const testCrew = [
    {
      id: 1,
      name: "Seth",
      phone: "768-230-5738",
      email: "seth@seth.com"
    }
  ]
  const testGear = [
    {
      id: 1,
      name: "pack",
      quantity: 4
    }
  ]
  const testTrips = [
    {
      id: 1,
      location: "BWCA",
      startDate: dayjs().add(7, 'day'),
      endDate: dayjs().add(14, 'day'),
      crew: testCrew,
      gear: testGear,
    },
    {
      id: 2,
      location: "YellowStone",
      startDate: dayjs().subtract(14, 'day'),
      endDate: dayjs().subtract(7, 'day'),
      crew: testCrew,
      gear: testGear,
    },
    {
      id: 3,
      location: "Jay Cooke",
      startDate: dayjs().subtract(14, 'day'),
      endDate: dayjs().subtract(7, 'day'),
      crew: testCrew,
      gear: testGear,
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
      path: "/trips/:id/past",
      element: <PastTrip/>
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
