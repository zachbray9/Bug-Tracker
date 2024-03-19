import { createBrowserRouter } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import Layout from "./pages/Layout";
import Error from "./pages/Error";

const router = createBrowserRouter([
    {
        path: '/',
        element: <Layout />,
        children: [
            { path: '', element: <Home /> },
            { path: 'login', element: <Login /> },
            { path: 'signup', element: <Signup /> }
        ],
        /*errorElement: <Error/>*/
    }
])

export default router;