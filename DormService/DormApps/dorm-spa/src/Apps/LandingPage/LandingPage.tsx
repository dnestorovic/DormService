import React, { useState } from 'react';
import { useMount } from 'react-use';
import Banner from './components/Banner';
import Login from './components/Login';

const LandingPage = () => {
    const [showBanner, setShowBanner] = useState<boolean>(false);

    useMount(() => {
        setShowBanner(true);
        setTimeout(() => {
            setShowBanner(false);
        }, 14050);
    });

    return showBanner ? <Banner /> : <Login />;
};

export default LandingPage;