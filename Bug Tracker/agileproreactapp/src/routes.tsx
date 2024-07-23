import { createBrowserRouter } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import Layout from "./pages/Layout";
import Dashboard from "./pages/Dashboard";
import ProjectBoard from "./pages/ProjectBoard";
import AccountSettings from "./pages/AccountSettings";
import RequireAuth from "./Router/RequireAuth";
/*import Error from "./pages/Error";*/

const router = createBrowserRouter([
    {
        path: '/',
        element: <Layout />,
        children: [
            {element: <RequireAuth />, children: [
                { path: 'dashboard', element: <Dashboard /> },
                { path: 'projectBoard', element: <ProjectBoard /> },
                { path: 'accountSettings', element: <AccountSettings /> }
            ] },
            { path: '', element: <Home />},
            { path: 'login', element: <Login /> },
            { path: 'signup', element: <Signup /> },
        ],
        /*errorElement: <Error/>*/
    }
])

export default router;