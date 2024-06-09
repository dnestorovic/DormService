import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const SideMenu = () => {
    const [showMenu, setShowMenu] = useState<boolean>(false);

    return (
        <div className="actions-menu" onMouseEnter={() => setShowMenu(true)} onMouseLeave={() => setShowMenu(false)}>
            <div className='pattern'>
                <div className={['title', showMenu ? "show-title" : "hide-title"].join(' ')}><span>Dorm Service</span></div>
                <ul>
                    <li className='navigation-item navigation-login'>
                        <div className='icon' />
                        <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Login</div>
                    </li>
                    <li className='navigation-item navigation-canteen'>
                        <div className='icon' />
                        <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Canteen</div>
                    </li>
                    <li className='navigation-item navigation-payments'>
                        <div className='icon' />
                        <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Payments</div>
                    </li>
                    <li className='navigation-item navigation-laundry'>
                        <div className='icon' />
                        <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Laundry</div>
                    </li>
                    <li className='navigation-item navigation-documentation'>
                        <div className='icon' />
                        <div className={['label', showMenu ? "show-label" : "hide-label"].join(' ')}>Documentation</div>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default SideMenu;