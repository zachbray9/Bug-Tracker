import { useState } from "react";
import axios from "../api/axios";
import Cookies from "js-cookie";

const useAuth = () => {
    const [user, setUser] = useState(null);

    const checkAuthStatus = async() => {
        try {
            const userId = Cookies.get("AgileSessionId")
            const response = await axios.get("/api/Users/");

            if (response.status === 200) {
                setUser();
            }
        } catch(error) {

        }


        return user;
    }
}

export default useAuth;