import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import Home from './Components/Home/Home';
import {
  createBrowserRouter,
  RouterProvider
} from "react-router-dom";
import People from './Components/People/People';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home/>
  },
  {
    path: "/people",
    element: <App/>
  },
  {
    path: "/people/:id",
    element: <People/>
  }
]);



const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

