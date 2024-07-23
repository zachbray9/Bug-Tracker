import { Outlet } from "react-router-dom"
import NavBar from "../components/NavBar"
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
        return <LoadingComponent text="Authenticating..." />;
    }

    return (
        <>
            <NavBar />
            <div id='main'>
                <Outlet/>
            </div>     
        </>
    )
}

export default observer(Layout)