import { Outlet } from "react-router-dom"
import NavBar from "../components/NavBar"
import Footer from "../components/Footer"

const Layout = () => {
    return (
        <>
            <NavBar />
            <div id='main'>
                <Outlet/>
            </div>
            <Footer/>
        </>
    )
}

export default Layout