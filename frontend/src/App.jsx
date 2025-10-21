import './App.css'
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import Home from "./pages/Home.jsx";
import Doctors from "./pages/Doctors.jsx";
import Login from "./pages/Login.jsx";
import SidePanel from "./components/SidePanel.jsx";

const router = createBrowserRouter([
    {path: "/", element: <Home /> },
    {path: "/doctors", element: <Doctors /> },
    {path: "/login", element: <Login /> },
]);

export default function App() {
    
    return (
        <>
            <RouterProvider router={router} />
            <SidePanel />
        </>
    )
}
