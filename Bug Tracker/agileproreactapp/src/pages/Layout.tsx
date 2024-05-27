import { Outlet } from "react-router-dom"
import NavBar from "../components/NavBar"
import Footer from "../components/Footer"
import { observer } from "mobx-react-lite"
import { useStore } from "../stores/store";
import { useEffect } from "react";
import LoadingComponent from "../components/common/Loading/LoadingComponent";

function Layout() {
    const { commonStore, userStore } = useStore();

    useEffect(() => {
        if (commonStore.token) {
            userStore.getCurrentUser().finally(() => commonStore.setAppLoaded() );
        } else {
            commonStore.setAppLoaded();
        }
    }, [commonStore, userStore]);

    if (!commonStore.appLoaded) {
        return <LoadingComponent text="Loading app..." />;
    }

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

export default observer(Layout)