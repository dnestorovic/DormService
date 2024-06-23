import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { LogOutUser } from '../../models/User';
import IdentityService from '../../services/IdentityService';
import { getRole } from '../../Utils/TokenUtil';

const SideMenu = () => {
    const [showMenu, setShowMenu] = useState<boolean>(false);

    const navigate = useNavigate();

    const handleLogout = () => {
        const username = localStorage.getItem("username");
        const refreshToken = localStorage.getItem("refresh-token");

        const userState: LogOutUser = {
            userName: username || undefined,
            refreshToken: refreshToken || undefined
        }

        IdentityService.logout(userState)
        .then(() => {
            localStorage.clear();
            navigate('/login');
        })
        .catch(() => alert("Something went wrong!"));

    }

    return (
        <div className="actions-menu" onMouseEnter={() => setShowMenu(true)} onMouseLeave={() => setShowMenu(false)}>
            <div className='pattern'>
                <div className={['title', showMenu ? "show-title" : "hide-title"].join(' ')}><span>Dorm Service</span></div>
                <ul>
                    <Link to="/login">
                        <li className='navigation-item navigation-login'>
                            <div className='icon' />
                            <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Login</div>
                        </li>
                    </Link>
                    <Link to="/payments">
                        <li className='navigation-item navigation-payments'>
                            <div className='icon' />
                            <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Payments</div>
                        </li>
                    </Link>
                    { getRole() === "Student" && <Link to="/canteen">
                        <li className='navigation-item navigation-canteen'>
                            <div className='icon' />
                            <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Canteen</div>
                        </li>
                    </Link>
                    }
                    <Link to="/laundry">
                        <li className='navigation-item navigation-laundry'>
                            <div className='icon' />
                            <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Laundry</div>
                        </li>
                    </Link>
                    { getRole() === "Student" && <Link to="/documentation">
                        <li className='navigation-item navigation-documentation'>
                            <div className='icon' />
                            <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Documentation</div>
                        </li>
                    </Link>
                    }
                    <li className='navigation-item navigation-logout' onClick={handleLogout}>
                        <div className='icon' />
                        <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Logout</div>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default SideMenu;