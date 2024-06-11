import { createBrowserRouter } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import Layout from "./pages/Layout";
import Dashboard from "./pages/Dashboard";
import ProjectBoard from "./pages/ProjectBoard";
import Profile from "./pages/Profile";
/*import Error from "./pages/Error";*/

const router = createBrowserRouter([
    {
        path: '/',
        element: <Layout />,
        children: [
            { path: '', element: <Home />},
            { path: 'login', element: <Login /> },
            { path: 'signup', element: <Signup /> },
            { path: 'dashboard', element: <Dashboard /> },
            { path: 'projectBoard', element: <ProjectBoard /> },
            { path: 'profile', element: <Profile /> }
        ],
        /*errorElement: <Error/>*/
    }
])

export default router;