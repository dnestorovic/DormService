import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { useMount } from 'react-use'
import { getRole } from '../../Utils/TokenUtil';
import LaudnryPage from './components/StudnetPage/LaudnryPage';
import AdminLaundryPage from './components/AdminPage/AdminLaundryPage';

export default function LaundryPageSwitcher() {

    const navigate = useNavigate();
    const [isAdmin, setIsAdmin] = useState<boolean>(false)

    useMount(() => {
        if (localStorage.getItem("username") === undefined) {
            navigate('/login');
            return;
        }

        setIsAdmin(() => getRole() === 'Administrator');
    });


    return isAdmin ? <AdminLaundryPage /> : <LaudnryPage />
}
