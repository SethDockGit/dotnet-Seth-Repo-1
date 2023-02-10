import { RouterProvider } from 'react-router-dom';
import { createBrowserRouter } from 'react-router-dom';
import Layout from './Components/Layout/Layout';
import AddPet from './Components/AddPet/AddPet';
import PetTable from './Components/PetTable/PetTable';
import { useState } from 'react';
import { PetsContext } from './Contexts/PetsContext';
import Pet from './Components/Pet/Pet';
import Home from './Components/Home/Home';

function App() {

  const initialPets = [
    {
      Name: "Snuggles",
      Age: 10,
      Species: "LittleGuy",
      Id: 1,
    },
    {
      Name: "Georgie",
      Age: 5,
      Species: "Tabby",
      Id: 2,
    },
    {
      Name: "Davey",
      Age:16,
      Species: "Husky",
      Id: 3,
    }];

    const [pets, setPets] = useState(initialPets)
    const router = createBrowserRouter([{

        path: "/",
        element: <Layout/>,
        children: [
          {
            path: "/home",
            element: <Home/>,
          },
          {
            path: "/pets/add",
            element: <AddPet/>,
          },
          {
            path: "/pets",
            element: <PetTable/>,
          },
          {
            path: "/pets/:id",
            element: <Pet/>
          }
        ]
      }]);

  return (
    <div className="App">
      <PetsContext.Provider value={{pets, setPets}}>
        <RouterProvider router={router}/> 
      </PetsContext.Provider>
    </div>
  );
}

export default App;
